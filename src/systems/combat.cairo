use blob_arena::components::blobert::BlobertTrait;
use blob_arena::components::{
    blobert::Blobert, stats::Stats, combat::{Move, MatchResult, Outcome, AB},
};


fn calculate_win_damage(attacker: Stats, defender: Stats, winning_mode: Move) -> u8 {
    let (a_multiplier, b_multiplier): (u8, u8) = match winning_mode {
        Move::Beat => (attacker.strength, defender.strength),
        Move::Counter => (attacker.speed, defender.strength),
        Move::Rush => (attacker.speed, defender.speed),
    };
    attacker.attack * a_multiplier / (defender.defense * b_multiplier)
}

fn calculate_draw_damage(attacker: Stats, defender: Stats, mode: Move) -> u8 {
    match mode {
        Move::Beat => attacker.attack * attacker.strength - defender.defense,
        Move::Counter => 0,
        Move::Rush => attacker.attack * attacker.speed - defender.defense,
    }
}


fn calculate_damage(player_a: Stats, player_b: Stats, outcome: Outcome) -> (u8, u8) {
    match outcome.MatchResult {
        MatchResult::Draw => (
            calculate_draw_damage(player_a, player_b, outcome.move),
            calculate_draw_damage(player_b, player_a, outcome.move)
        ),
        MatchResult::Winner(winner) => {
            let (attacker, defender): (Stats, Stats) = match winner {
                AB::A => (player_a, player_b),
                AB::B => (player_b, player_a),
            };
            let damage = calculate_win_damage(attacker, defender, outcome.move);

            match winner {
                AB::A => (0, damage),
                AB::B => (damage, 0),
            }
        }
    }
}


fn get_outcome(move_a: Move, move_b: Move) -> Outcome {
    let MatchResult_u8: u8 = (move_a.into() - move_b.into() % 3);
    if MatchResult_u8 == 0 {
        return Outcome { MatchResult: MatchResult::Draw, move: move_a };
    };

    let winner: AB = match (MatchResult_u8 % 2).is_non_zero() {
        false => AB::B,
        true => AB::A,
    };
    let move = match winner {
        AB::A => move_a,
        AB::B => move_b,
    };

    return Outcome { MatchResult: MatchResult::Winner(winner), move };
}

