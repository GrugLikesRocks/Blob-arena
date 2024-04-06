use blob_arena::components::stats::{Stats, StatsTrait};

#[derive(Copy, Drop, Print, Serde, SerdeLen)]
enum Background {
    AvnuBlue,
    Blue,
    CryptsAndCaverns,
    FibrousFrame,
    Green,
    Holo,
    Orange,
    Purple,
    RealmsDark,
    Realms,
    Terraforms,
    Tulip,
}

impl BackgroundImpl of StatsTrait<Background> {
    fn stats(self: Background) -> Stats {
        match self {
            Background::AvnuBlue => Stats { attack: 0, defense: 0, speed: 0, strength: 0, },
            Background::Blue => Stats { attack: 0, defense: 0, speed: 0, strength: 0, },
            Background::CryptsAndCaverns => Stats { attack: 0, defense: 0, speed: 0, strength: 0, },
            Background::FibrousFrame => Stats { attack: 0, defense: 0, speed: 0, strength: 0, },
            Background::Green => Stats { attack: 0, defense: 0, speed: 0, strength: 0, },
            Background::Holo => Stats { attack: 0, defense: 0, speed: 0, strength: 0, },
            Background::Orange => Stats { attack: 0, defense: 0, speed: 0, strength: 0, },
            Background::Purple => Stats { attack: 0, defense: 0, speed: 0, strength: 0, },
            Background::RealmsDark => Stats { attack: 0, defense: 0, speed: 0, strength: 0, },
            Background::Realms => Stats { attack: 0, defense: 0, speed: 0, strength: 0, },
            Background::Terraforms => Stats { attack: 0, defense: 0, speed: 0, strength: 0, },
            Background::Tulip => Stats { attack: 0, defense: 0, speed: 0, strength: 0, },
        }
    }
    fn index(self: Background) -> u8 {
        match self {
            Background::AvnuBlue => 0,
            Background::Blue => 1,
            Background::CryptsAndCaverns => 2,
            Background::FibrousFrame => 3,
            Background::Green => 4,
            Background::Holo => 5,
            Background::Orange => 6,
            Background::Purple => 7,
            Background::RealmsDark => 8,
            Background::Realms => 9,
            Background::Terraforms => 10,
            Background::Tulip => 11,
        }
    }
}

impl U8IntoBackground of Into<u8, Background> {
    fn into(self: u8) -> Background {
        match self {
            0 => Background::AvnuBlue,
            1 => Background::Blue,
            2 => Background::CryptsAndCaverns,
            3 => Background::FibrousFrame,
            4 => Background::Green,
            5 => Background::Holo,
            6 => Background::Orange,
            7 => Background::Purple,
            8 => Background::RealmsDark,
            9 => Background::Realms,
            10 => Background::Terraforms,
            11 => Background::Tulip,
            _ => panic!("wrong background index")
        }
    }
}

impl SU8IntoBackground of Into<@u8, Background> {
    fn into(self: @u8) -> Background {
        match *self {
            0 => Background::AvnuBlue,
            1 => Background::Blue,
            2 => Background::CryptsAndCaverns,
            3 => Background::FibrousFrame,
            4 => Background::Green,
            5 => Background::Holo,
            6 => Background::Orange,
            7 => Background::Purple,
            8 => Background::RealmsDark,
            9 => Background::Realms,
            10 => Background::Terraforms,
            11 => Background::Tulip,
            _ => panic!("wrong background index")
        }
    }
}
