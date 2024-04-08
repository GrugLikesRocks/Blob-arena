using Dojo;
using Dojo.Starknet;
using Dojo.Torii;

public class LastRound : ModelInstance
{
    [ModelField("combat_id")]
    public FieldElement combatId;

    [ModelField("health_a")]
    public byte healthA;
    [ModelField("health_b")]
    public byte healthB;

    [ModelField("move_a")]
    public FieldElement moveA;
    [ModelField("move_b")]
    public FieldElement moveB;

    [ModelField("damage_a")]
    public byte damageA;
    [ModelField("damage_b")]
    public byte damageB;


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
