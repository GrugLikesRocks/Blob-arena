use blob_arena::components::{
    blobert::{Blobert, Health}, combat::{MatchResult, Move, TwoMoves, TwoMovesTrait},
    utils::{AB, Status, Winner}
};
use starknet::{ContractAddress};


#[derive(Model, Copy, Drop, Print, Serde)]
struct Knockout {
    #[key]
    combat_id: u128,
    player_a: ContractAddress,
    player_b: ContractAddress,
    blobert_a: u128,
    blobert_b: u128,
}


#[derive(Model, Copy, Drop, Print, Serde)]
struct Healths {
    #[key]
    combat_id: u128,
    a: u8,
    b: u8,
}

#[derive(Model, Copy, Drop, Print, Serde, SerdeLen)]
struct LastRound {
    #[key]
    combat_id: u128,
    health_a: u8,
    health_b: u8,
    move_a: Move,
    move_b: Move,
    damage_a: u8,
    damage_b: u8,
}


#[generate_trait]
impl RoundImpl of RoundTrait {
    fn create(
        combat_id: u128, healths: Healths, moves: TwoMoves, damage_a: u8, damage_b: u8
    ) -> LastRound {
        let (move_a, move_b) = moves.moves();
        LastRound {
            combat_id, health_a: healths.a, health_b: healths.b, move_a, move_b, damage_a, damage_b
        }
    }
}

#[generate_trait]
impl HealthsImpl of HealthsTrait {
    fn running(self: Healths) -> bool {
        return self.a > 0 && self.b > 0;
    }
    fn apply_damage(ref self: Healths, damage_a: u8, damage_b: u8) {
        self.a -= damage_a;
        self.b -= damage_b;
    }
    fn status(self: Healths) -> Status {
        if self.a > 0 && self.b > 0 {
            return Status::Running;
        }
        let winner: Winner = if self.a > 0 {
            Winner::A
        } else if self.b > 0 {
            Winner::B
        } else {
            Winner::Draw
        };
        return Status::Finished(winner);
    }
}
