use blob_arena::{
    components::{blobert::{Blobert, BlobertTrait}, world::World}, utils::{uuid, RandomTrait}
};
use dojo::world::{IWorldDispatcherTrait};
use starknet::{ContractAddress};


#[generate_trait]
impl BlobertWorldImpl of BlobertWorldTrait {
    fn mint_blobert(self: World, owner: ContractAddress) -> Blobert {
        let id = uuid(self);
        let mut random = RandomTrait::new();
        let seed = random.next();
        let blobert = BlobertTrait::new(id, owner, seed);
        set!(self, (blobert, blobert_stats));
        blobert
    }
    fn get_blobert(self: World, blobert_id: u128) -> Blobert {
        let blobert: Blobert = get!(self, (blobert_id), Blobert);
        assert(blobert.owner.is_non_zero(), 'Blobert not found');
        blobert
    }
    fn get_blobert_stats(self: World, blobert_id: u128) -> Blobert {
        let blobert: Blobert = get!(self, (blobert_id), BlobertStats);
        assert(blobert.owner.is_non_zero(), 'Blobert not found');
        blobert
    }
}
