use core::option::OptionTrait;
use starknet::ContractAddress;
use blob_arena::{constants::U64_MASK_U256, components::utils::{AB, Status, Winner}};

extern fn pedersen(a: felt252, b: felt252) -> felt252 implicits(Pedersen) nopanic;

#[derive(Copy, Drop, Print, Serde, SerdeLen, Introspect, PartialEq)]
enum Move {
    Beat,
    Counter,
    Rush,
}

#[derive(Model, Copy, Drop, Print, Serde, SerdeLen)]
struct RoundCommitments {
    #[key]
    round_id: u128,
    hash_a: u64,
    hash_b: u64,
}

#[derive(Copy, Drop, Serde)]
struct Reveal {
    move: Move,
    salt: u64,
}

#[generate_trait]
impl RevealImpl of RevealTrait {
    fn get_hash_u64(self: Reveal) -> u64 {
        (self.get_hash().into() & U64_MASK_U256).try_into().unwrap()
    }
    fn get_hash(self: Reveal) -> felt252 {
        pedersen(self.salt.into(), self.move.into())
    }
}

#[derive(Model, Copy, Drop, Print, Serde, SerdeLen)]
struct CombatHealth {
    #[key]
    combat_id: u128,
    player_a: ContractAddress,
    player_b: ContractAddress,
}

struct RoundCommitment {
    #[key]
    combat_id: u128,
    number: u32,
}


#[derive(Model, Copy, Drop, Print, Serde, SerdeLen)]
struct Round {
    #[key]
    combat_id: u128,
    number: u32,
    a_health: u8,
    b_health: u8,
}

#[generate_trait]
impl RoundImpl of RoundTrait {
    fn running(self: Round) -> bool {
        return self.a_health > 0 && self.b_health > 0;
    }
    fn apply_damage(ref self: Round, damage_a: u8, damage_b: u8) {
        self.a_health -= damage_a;
        self.b_health -= damage_b;
        self.number += 1;
    }
    fn status(self: Round) -> Status {
        if self.a_health > 0 && self.b_health > 0 {
            return Status::Running;
        }
        let winner: Winner = if self.a_health > 0 {
            Winner::A
        } else if self.b_health > 0 {
            Winner::B
        } else {
            Winner::Draw
        };
        return Status::Finished(winner);
    }
}

impl MoveIntoU8<T, +Into<u8, T>> of Into<Move, T> {
    fn into(self: Move) -> T {
        match self {
            Move::Beat => 0_u8,
            Move::Counter => 1_u8,
            Move::Rush => 2_u8,
        }.into()
    }
}

#[derive(Copy, Drop, Print, Serde)]
enum MatchResult {
    Winner: AB,
    Draw,
}

#[derive(Copy, Drop, Print, Serde)]
struct Outcome {
    MatchResult: MatchResult,
    move: Move
}

