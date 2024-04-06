#[derive(Copy, Drop, Print, Serde, SerdeLen)]
struct Stats {
    attack: u8,
    defense: u8,
    speed: u8,
    strength: u8,
}
trait StatsTrait<T> {
    fn stats(self: T) -> Stats;
    fn index(self: T) -> u8;
}

impl u8IntoStats of Into<u8, Stats> {
    fn into(self: u8) -> Stats {
        Stats { attack: self, defense: self, speed: self, strength: self, }
    }
}


impl StatsAdd of Add<Stats> {
    fn add(lhs: Stats, rhs: Stats) -> Stats {
        return Stats {
            attack: lhs.attack + rhs.attack,
            defense: lhs.defense + rhs.defense,
            speed: lhs.speed + rhs.speed,
            strength: lhs.strength + rhs.strength,
        };
    }
}

impl StatsMul of Mul<Stats> {
    fn mul(lhs: Stats, rhs: Stats) -> Stats {
        return Stats {
            attack: lhs.attack * rhs.attack,
            defense: lhs.defense * rhs.defense,
            speed: lhs.speed * rhs.speed,
            strength: lhs.strength * rhs.strength,
        };
    }
}
