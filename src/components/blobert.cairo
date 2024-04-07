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
    background: u8,
    armour: u8,
    mask: u8,
    jewelry: u8,
    weapon: u8,
}

#[derive(Model, Copy, Drop, Print, Serde)]
struct Health {
    #[key]
    blobert_id: u128,
    #[key]
    combat_id: u128,
    health: u8,
}

#[derive(Copy, Drop, Print, Serde)]
struct Items {
    background: Background,
    armour: Armour,
    mask: Mask,
    jewelry: Jewelry,
    weapon: Weapon,
}


#[generate_trait]
impl BlobertImpl of BlobertTrait {
    fn new(id: u128, owner: ContractAddress, seed: u256) -> Blobert {
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

        return Blobert { id, owner, background, armour, jewelry, mask, weapon };
    }
    fn stats(self: @Blobert) -> Stats {
        let Items { background, armour, mask, jewelry, weapon, } = self.items();
        let (b_stats, a_stats, m_stats, j_stats, w_stats) = (
            background.stats(), armour.stats(), mask.stats(), jewelry.stats(), weapon.stats()
        );

        return (b_stats + j_stats) * (a_stats + m_stats + w_stats);
    }
    fn items(self: @Blobert) -> Items {
        Items {
            background: self.background.into(),
            armour: self.armour.into(),
            mask: self.mask.into(),
            jewelry: self.jewelry.into(),
            weapon: self.weapon.into(),
        }
    }
    fn check_owner(self: Blobert, player: ContractAddress) -> bool {
        return self.owner == player;
    }
    fn assert_owner(self: Blobert, player: ContractAddress) {
        assert(self.check_owner(player), 'Not Blobert Owner');
    }
}
