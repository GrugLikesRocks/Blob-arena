use blob_arena::{components::{blobert::{Blobert}, world::World}};
use dojo::world::{IWorldDispatcher, IWorldDispatcherTrait};
use starknet::{ContractAddress};

use origami::random::


#[generate_trait]
impl BlobertImpl of WBlobertTrait {
    fn formulate(seed: u128){
        
    }

    fn get_blobert(self: IWorldDispatcher, bert_id: u128) -> Blobert {
        get!(self, (bert_id), Blobert)
    }
    fn check_owner(self: Blobert, player: ContractAddress) -> bool {
        return self.owner == player;
    }
    fn assert_owner(self: Blobert, player: ContractAddress) {
        assert(self.check_owner(player), 'Not Blobert Owner');
    }
}
