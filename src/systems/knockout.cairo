use blob_arena::{
    components::{
        blobert::{Blobert, BlobertTrait, Health}, combat::{Move, Round, RoundTrait}, world::World,
        knockout::{Knockout,}
    },
    systems::{blobert::{WBlobertTrait}, combat::{Outcome, calculate_damage, get_outcome}}
};
use dojo::world::{IWorldDispatcherTrait};

#[derive(Copy, Drop, Serde)]
struct KnockoutGame {
    world: World,
    combat_id: u128,
    bert_a: u128,
    bert_b: u128,
}

#[generate_trait]
impl KnockoutImpl of KnockoutTrait {
    fn create(world: World, combat_id: u128) -> KnockoutGame {
        let model: Knockout = get!(world, combat_id, Knockout);
        KnockoutGame { world, combat_id, bert_a: model.bert_a, bert_b: model.bert_b, }
    }
    fn get_bloberts(self: KnockoutGame) -> (Blobert, Blobert) {
        (self.world.get_blobert(self.bert_a), self.world.get_blobert(self.bert_b))
    }
    fn get_round(self: KnockoutGame) -> Round {
        get!(self.world, self.combat_id, Round)
    }

    fn run_round(self: KnockoutGame, move_a: Move, move_b: Move) {
        let (bert_a, bert_b) = self.get_bloberts();
        let mut round = self.get_round();
        let outcome = get_outcome(move_a, move_b);
        let (damage_a, damage_b) = calculate_damage(bert_a.stats(), bert_b.stats(), outcome);
        round.apply_damage(damage_a, damage_b);

        set!(self.world, (round,))
    }
    fn commit_move(self: KnockoutGame) {}
}
