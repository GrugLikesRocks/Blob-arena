using Dojo;
using Dojo.Starknet;
using Dojo.Torii;
using UnityEngine;

public class Healths : ModelInstance
{
    [ModelField("combat_id")]
    public FieldElement combatId;

    [ModelField("a")]
    public byte a;
    [ModelField("b")]
    public byte b;

    private void Start()
    {
        //DojoEntitiesStatic.healthsList.Add(this);
        Debug.Log("Healths combat id: " + combatId.Hex());
        Debug.Log("Healths a: " + a);

    }

    private void Update()
    {
        
    }

    public override void OnUpdate(Model model)
    {
    }
}
