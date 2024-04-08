use blob_arena::components::combat::TwoHashesTrait;
use blob_arena::components::combat::TwoMovesTrait;
use blob_arena::{
    components::{
        blobert::{Blobert, BlobertTrait}, combat::{Move, TwoHashes, RevealTrait, TwoMoves},
        world::World, knockout::{Knockout, Healths, HealthsTrait, RoundTrait}, utils::{AB, Status},
    },
    systems::{blobert::{BlobertWorldTrait}, combat::{Outcome, calculate_damage, get_outcome}},
    utils::{uuid},
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
impl KnockoutGameImpl of KnockoutGameTrait {
    fn new(
        world: World,
        player_a: ContractAddress,
        player_b: ContractAddress,
        blobert_a: u128,
        blobert_b: u128
    ) -> u128 {
        let combat_id = uuid(world);
        let _ = (world.get_blobert(blobert_a), world.get_blobert(blobert_b));
        let knockout = Knockout { combat_id, player_a, player_b, blobert_a, blobert_b };
        let healths = Healths { combat_id, a: 255, b: 255 };
        set!(world, (knockout, healths));
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
    fn commit_move(self: KnockoutGame, hash: u64) {
        self.assert_running();
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

        if moves.check_done() {
            self.verify_round(ref commitments, ref moves);
        } else {
            set!(self.world, (moves,));
        }
    }

    fn verify_round(self: KnockoutGame, ref commitments: TwoHashes, ref moves: TwoMoves) {
        let (blobert_a, blobert_b) = self.get_bloberts();
        let mut healths = self.get_healths();
        let (move_a, move_b) = moves.moves();

        let outcome = get_outcome(move_a, move_b);
        let (damage_a, damage_b) = calculate_damage(blobert_a.stats, blobert_b.stats, outcome);
        let round = RoundTrait::create(self.combat_id, healths, moves, damage_a, damage_b);
        moves.reset();
        commitments.reset();
        healths.apply_damage(damage_a, damage_b);
        set!(self.world, (healths, commitments, moves, round));
    }

    fn get_status(self: KnockoutGame) -> Status {
        let healths = self.get_healths();
        healths.status()
    }
    fn assert_running(self: KnockoutGame) {
        let status = self.get_status();
        assert(status == Status::Running, 'Game not running');
    }
}
