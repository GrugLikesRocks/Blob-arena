using Dojo;
using Dojo.Starknet;
using Dojo.Torii;
using UnityEngine;

public class Knockout : ModelInstance
{
    [ModelField("combat_id")]
    public FieldElement combatId;

    [ModelField("player_a")]
    public FieldElement playerA;
    [ModelField("player_a")]
    public FieldElement playerB;

    [ModelField("blobert_a")]
    public FieldElement blobertA;
    [ModelField("blobert_b")]
    public FieldElement blobertB;


    private void Start()
    {
        DojoEntitiesStatic.knockoutsList.Add(this);
    }

    private void Update()
    {

    }

    public override void OnUpdate(Model model)
    {

    }
}
