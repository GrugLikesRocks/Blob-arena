using Dojo;
using Dojo.Starknet;
using Dojo.Torii;
using System;

public enum MoveN
{
    NONE,
    BEAT,
    COUNTER,
    RUSH
}


public class TwoMoves : ModelInstance
{
    [ModelField("round_id")]
    public FieldElement roundId;

    [ModelField("a")]
    public MoveN b;
    [ModelField("b")]
    public MoveN a;

    private void Start()
    {

    }

    private void Update()
    {

    }

    public override void OnUpdate(Model model)
    {

    }
}

