use blob_arena::{components::{blobert::{Blobert, Health}, world::World}};
use dojo::world::{IWorldDispatcher, IWorldDispatcherTrait};

#[generate_trait]
impl BlobertImpl of WBlobertTrait {
    fn get_blobert(self: IWorldDispatcher, bert_id: u128) -> Blobert {
        get!(self, (bert_id), Blobert)
    }
    fn get_health(self: IWorldDispatcher, bert_id: u128, combat_id: u128) -> Health {
        get!(self, (bert_id, combat_id), Health)
    }
}
