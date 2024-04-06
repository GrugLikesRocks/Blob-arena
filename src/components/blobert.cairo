use starknet::ContractAddress;

use blob_arena::components::{
    stats::{Stats, StatsTrait}, background::Background, armour::Armour, mask::Mask,
    jewelry::Jewelry, weapon::Weapon,
};

#[derive(Model, Copy, Drop, Print, Serde)]
struct Blobert {
    #[key]
    blobert_id: u128,
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
}
