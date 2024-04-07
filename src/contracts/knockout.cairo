use blob_arena::{components::{combat::Move, utils::{Status}}};
use starknet::{ContractAddress};

#[starknet::interface]
trait IKnockoutActions<TContractState> {
    fn new(
        self: @TContractState,
        player_a: ContractAddress,
        player_b: ContractAddress,
        blobert_a: u128,
        blobert_b: u128
    ) -> u128;
    fn commit(self: @TContractState, combat_id: u128, hash: u64);
    fn reveal(self: @TContractState, combat_id: u128, move: Move, salt: u64);
    fn verify(self: @TContractState, combat_id: u128);
    fn fetch_status(self: @TContractState, combat_id: u128) -> Status;
}

#[dojo::contract]
mod knockout_actions {
    use super::IKnockoutActions;
    use starknet::{ContractAddress};

    use blob_arena::{
        components::{combat::Move, utils::{AB, Status}},
        systems::{knockout::{KnockoutGame, KnockoutGameTrait}}
    };
    #[abi(embed_v0)]
    impl KnockoutActionsImpl of IKnockoutActions<ContractState> {
        fn new(
            self: @ContractState,
            player_a: ContractAddress,
            player_b: ContractAddress,
            blobert_a: u128,
            blobert_b: u128
        ) -> u128 {
            KnockoutGameTrait::new(
                self.world_dispatcher.read(), player_a, player_b, blobert_a, blobert_b,
            )
        }
        fn commit(self: @ContractState, combat_id: u128, hash: u64) {
            self.get_game(combat_id).commit_move(hash);
        }
        fn reveal(self: @ContractState, combat_id: u128, move: Move, salt: u64) {
            self.get_game(combat_id).reveal_move(move, salt);
        }
        fn verify(self: @ContractState, combat_id: u128) {
            self.get_game(combat_id).verify_round();
        }
        fn fetch_status(self: @ContractState, combat_id: u128) -> Status {
            self.get_game(combat_id).get_status()
        }
    }

    #[generate_trait]
    impl KnockoutInternalImpl of KnockoutInternalTrait {
        #[inline(always)]
        fn get_game(self: @ContractState, combat_id: u128) -> KnockoutGame {
            KnockoutGameTrait::create(self.world_dispatcher.read(), combat_id)
        }
    }
}
