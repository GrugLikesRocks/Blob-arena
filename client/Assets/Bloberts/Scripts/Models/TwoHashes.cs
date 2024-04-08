using Dojo;
using Dojo.Starknet;
using Dojo.Torii;
using System;
using UnityEngine;

public class TwoHashes : ModelInstance
{
    [ModelField("id")]
    public FieldElement roundId;

    [ModelField("a")]
    public UInt64 b;
    [ModelField("b")]
    public UInt64 a;

    private void Start()
    {
        Debug.Log("this is a call from the two hashes model");
        Debug.Log("this is the round id: " + roundId);
        Debug.Log("this is the a: " + a);
        Debug.Log("this is the b: " + b);
    }

    private void Update()
    {

    }

    public override void OnUpdate(Model model)
    {

    }
}
