using Dojo;
using Dojo.Starknet;
using Dojo.Torii;
using UnityEngine;

public class Blobert : ModelInstance
{
    [ModelField("id")]
    public FieldElement blobertId;

    [ModelField("owner")]
    public FieldElement owner;

    [ModelField("stats")]
    public BlobertUitls.Stats stats;

    [ModelField("traits")]
    public BlobertUitls.Traits traits;


    private void Start()
    {
        if (owner.Hex() == DojoEntitiesStatic.currentAccount.Address.Hex())
        {
            DojoEntitiesStatic.userBlobertData = this;
        }


        Debug.Log("Blobert id: " + blobertId.Hex());
        Debug.Log("Blobert owner: " + owner.Hex());
        Debug.Log("Blobert stats speed: " + stats.speed);
        Debug.Log("Blobert stats attack: " + stats.attack);
        Debug.Log("Blobert stats defense: " + stats.defense);
        Debug.Log("Blobert stats strength: " + stats.strength);

        Debug.Log("Blobert traits mask: " + traits.mask);
        Debug.Log("Blobert traits background: " + traits.background);
        Debug.Log("Blobert traits armour: " + traits.armour);
        Debug.Log("Blobert traits jewelry: " + traits.jewelry);
        Debug.Log("Blobert traits strength: " + traits.weapon);

        DojoEntitiesStatic.allBlobertDict.Add(blobertId, this);
    }

    private void Update()
    {

    }

    public override void OnUpdate(Model model)
    {

    }
}
