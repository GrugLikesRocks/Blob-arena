use blob_arena::components::combat::TwoHashesTrait;
use blob_arena::components::combat::TwoMovesTrait;
use blob_arena::{
    components::{
        blobert::{Blobert, BlobertTrait, Health}, combat::{Move, TwoHashes, RevealTrait, TwoMoves},
        world::World, knockout::{Knockout, Healths, HealthsTrait}, utils::{AB, Status},
    },
    systems::{blobert::{WBlobertTrait}, combat::{Outcome, calculate_damage, get_outcome}},
    utils::{uuid, pedersen},
};
use starknet::{ContractAddress, get_caller_address};
use dojo::world::{IWorldDispatcherTrait};

#[derive(Copy, Drop, Serde)]
struct KnockoutGame {
    world: World,
    combat_id: u128,
    player_a: ContractAddress,
    player_b: ContractAddress,
    blobert_a: u128,
    blobert_b: u128,
}

#[generate_trait]
impl KnockoutImpl of KnockoutTrait {
    fn new(
        world: World,
        player_a: ContractAddress,
        player_b: ContractAddress,
        blobert_a: u128,
        blobert_b: u128
    ) -> u128 {
        let combat_id = uuid(world);
        let knockout = Knockout { combat_id, player_a, player_b, blobert_a, blobert_b, };
        set!(world, (knockout,));
        combat_id
    }
    fn create(world: World, combat_id: u128) -> KnockoutGame {
        let model: Knockout = get!(world, combat_id, Knockout);
        KnockoutGame {
            world,
            combat_id,
            player_a: model.player_a,
            player_b: model.player_b,
            blobert_a: model.blobert_a,
            blobert_b: model.blobert_b,
        }
    }
    fn get_bloberts(self: KnockoutGame) -> (Blobert, Blobert) {
        (self.world.get_blobert(self.blobert_a), self.world.get_blobert(self.blobert_b))
    }
    fn get_healths(self: KnockoutGame) -> Healths {
        get!(self.world, self.combat_id, Healths)
    }
    // fn get_round(self: KnockoutGame) -> Round {
    //     get!(self.world, self.combat_id, Round)
    // }
    fn get_commitments(self: KnockoutGame) -> TwoHashes {
        get!(self.world, self.combat_id, TwoHashes)
    }
    fn get_moves(self: KnockoutGame) -> TwoMoves {
        get!(self.world, self.combat_id, TwoMoves)
    }

    fn get_caller_player(self: KnockoutGame) -> AB {
        let caller = get_caller_address();
        if caller == self.player_a {
            return AB::A;
        }
        if caller == self.player_b {
            return AB::B;
        };
        panic!("Player not part of combat");
        AB::A
    }
    fn run_round(self: KnockoutGame, move_a: Move, move_b: Move) -> Healths {
        let (blobert_a, blobert_b) = self.get_bloberts();
        let mut healths = self.get_healths();
        let outcome = get_outcome(move_a, move_b);
        let (damage_a, damage_b) = calculate_damage(blobert_a.stats(), blobert_b.stats(), outcome);
        healths.apply_damage(damage_a, damage_b);
        healths
    }
    fn commit_move(self: KnockoutGame, hash: u64) {
        let player = self.get_caller_player();
        let mut commitments = self.get_commitments();
        match player {
            AB::A => {
                assert(commitments.a == 0, 'Already committed');
                commitments.a = hash;
            },
            AB::B => {
                assert(commitments.b == 0, 'Already committed');
                commitments.b = hash;
            },
        };
        set!(self.world, (commitments,));
    }

    fn reveal_move<T, +Into<T, felt252>, +Drop<T>>(self: KnockoutGame, move: Move, salt: T) {
        let player = self.get_caller_player();
        let reveal = RevealTrait::create(move, salt);
        let mut commitments = self.get_commitments();
        let mut moves = self.get_moves();

        assert(!moves.check_set(player), 'Already revealed');
        assert(reveal.check_hash(commitments.get_hash(player)), 'Hash dose not atch');

        moves.set_move(player, move);
    }

    fn verfiy_outcome(self: KnockoutGame) {
        let mut moves = self.get_moves();
        let mut commitments = self.get_commitments();
        let (move_a, move_b) = moves.get_moves();

        let healths = self.run_round(move_a, move_b);
        moves.reset();
        commitments.reset();
        set!(self.world, (healths, commitments, moves));
    }
}
