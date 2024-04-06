#[starknet::interface]
trait IKnockoutActions<TContractState> {
    fn commit_move(self: @TContractState, combat_id: u128, hash: u64);
}


mod knockout_actions {
    use super::IKnockoutActions;
    use blob_arena::{components::{combat::Move, utils::{AB}}};
    #[abi(embed_v0)]
    impl KnockoutActionsImpl of IKnockoutActions<ContractState> {
        fn commit_move(self: @TContractState, combat_id: u128, hash: u64) {}
    }
}
