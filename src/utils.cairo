use dojo::world::{IWorldDispatcher, IWorldDispatcherTrait};

extern fn pedersen(a: felt252, b: felt252) -> felt252 implicits(Pedersen) nopanic;

fn uuid(world: IWorldDispatcher) -> u128 {
    IWorldDispatcherTrait::uuid(world).into()
}

