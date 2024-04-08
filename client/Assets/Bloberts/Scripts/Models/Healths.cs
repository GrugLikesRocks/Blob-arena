using Dojo;
using Dojo.Starknet;
using Dojo.Torii;

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
        DojoEntitiesStatic.healthsStorage.Add(combatId, this);
    }

    private void Update()
    {
        
    }

    public override void OnUpdate(Model model)
    {

    }
}
