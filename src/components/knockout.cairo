use blob_arena::components::{
    blobert::{Blobert, Health}, combat::MatchResult, utils::{AB, Status, Winner}
};


#[derive(Model, Copy, Drop, Print, Serde)]
struct Knockout {
    #[key]
    combat_id: u128,
    bert_a: u128,
    bert_b: u128,
}


#[derive(Model, Copy, Drop, Print, Serde)]
struct Healths {
    #[key]
    combat_id: u128,
    a: u8,
    b: u8,
    round_number: u32,
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
