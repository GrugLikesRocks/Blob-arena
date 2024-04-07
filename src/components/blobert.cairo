use core::traits::TryInto;
use starknet::ContractAddress;

use blob_arena::components::{
    stats::{Stats, StatsTrait}, background::{Background, BACKGROUND_COUNT},
    armour::{Armour, ARMOUR_COUNT}, mask::{Mask, MASK_COUNT}, jewelry::{Jewelry, JEWELRY_COUNT},
    weapon::{Weapon, WEAPON_COUNT},
};

#[derive(Model, Copy, Drop, Print, Serde)]
struct Blobert {
    #[key]
    id: u128,
    owner: ContractAddress,
    traits: Traits,
    stats: Stats,
// owner: ContractAddress,
// background:Background,
// armour:Armour,
// mask:Mask,
// jewelry:Jewelry,
// weapon:Weapon,
// attack: u8,
// defense: u8,
// speed: u8,
// strength: u8,
}

#[derive(Model, Copy, Drop, Print, Serde)]
struct Health {
    #[key]
    blobert_id: u128,
    #[key]
    combat_id: u128,
    health: u8,
}

#[derive(Copy, Drop, Print, Serde, Introspect)]
struct Traits {
    background: Background,
    armour: Armour,
    mask: Mask,
    jewelry: Jewelry,
    weapon: Weapon,
}

// #[generate_trait]
// impl BlobertStatsImpl of BlobertStatsTrait {
//     fn to_blobert(self: Stats, blobert_id: u128) -> BlobertStats {
//         BlobertStats {
//             blobert_id,
//             attack: self.attack,
//             defense: self.defense,
//             speed: self.speed,
//             strength: self.strength,
//         }
//     }
// }

// impl StatsIntoBlobertStats of Into<BlobertStats, Stats> {
//     fn into(self: BlobertStats) -> Stats {
//         Stats {
//             attack: self.attack, defense: self.defense, speed: self.speed, strength: self.strength,
//         }
//     }
// }

fn calculate_stats(traits: Traits) -> Stats {
    let Traits { background, armour, mask, jewelry, weapon, } = traits;
    let (b_stats, a_stats, m_stats, j_stats, mut w_stats) = (
        background.stats(), armour.stats(), mask.stats(), jewelry.stats(), weapon.stats()
    );
    let s_mod = Stats { attack: 1, defense: 2, speed: 2, strength: 2 };

    return (b_stats + j_stats + w_stats + a_stats + m_stats) / s_mod;
}

fn generate_traits(seed: u256) -> Traits {
    let background_count: u256 = BACKGROUND_COUNT.into();
    let armour_count: u256 = ARMOUR_COUNT.into();
    let jewelry_count: u256 = JEWELRY_COUNT.into();
    let weapon_count: u256 = WEAPON_COUNT.into();
    let mut mask_count: u256 = MASK_COUNT.into();
    let mut m_seed = seed;
    let background: u8 = (m_seed % background_count).try_into().unwrap();
    m_seed /= 0x100;
    let armour: u8 = (m_seed % armour_count).try_into().unwrap();
    m_seed /= 0x100;
    // only allow the mask to be one of the first 8 masks 
    // where the armour is sheep wool or kigurumi
    if armour == 0 || armour == 1 {
        mask_count = 8;
    };

    let jewelry: u8 = (m_seed % jewelry_count).try_into().unwrap();
    m_seed /= 0x100;

    let mask: u8 = (m_seed % mask_count).try_into().unwrap();
    m_seed /= 0x100;

    let weapon: u8 = (m_seed % weapon_count).try_into().unwrap();
    Traits {
        background: background.into(),
        armour: armour.into(),
        mask: mask.into(),
        jewelry: jewelry.into(),
        weapon: weapon.into(),
    }
}
#[generate_trait]
impl BlobertImpl of BlobertTrait {
    fn new(id: u128, owner: ContractAddress, seed: u256) -> Blobert {
        let traits = generate_traits(seed);
        let stats = calculate_stats(traits);
        return Blobert { id, owner, traits, stats };
    }
    fn check_owner(self: Blobert, player: ContractAddress) -> bool {
        return self.owner == player;
    }
    fn assert_owner(self: Blobert, player: ContractAddress) {
        assert(self.check_owner(player), 'Not Blobert Owner');
    }
}
