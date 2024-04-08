using System.Collections.Generic;
using System.Linq;
using Dojo;
using Dojo.Starknet;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] WorldManager worldManager;
    [SerializeField] ChatManager chatManager;

    [SerializeField] WorldManagerData dojoConfig;
    [SerializeField] GameManagerData gameManagerData;

    private BurnerManager burnerManager;
    private Dictionary<Account, string> spawnedBurners = new();


    void Start()
    {
        var provider = new JsonRpcClient(dojoConfig.rpcUrl);
        var signer = new SigningKey(gameManagerData.masterPrivateKey);
        var account = new Account(provider, signer, new FieldElement(gameManagerData.masterAddress));

        burnerManager = new BurnerManager(provider, account);

        worldManager.synchronizationMaster.OnEntitySpawned.AddListener(InitEntity);
        foreach (var entity in worldManager.Entities())
        {
            InitEntity(entity);
        }
    }

    public async void CreateBurner()
    {
        var burner = await burnerManager.DeployBurner();
        spawnedBurners.Add(burner, null);
        DojoEntitiesStatic.currentAccount = burner;
    }

    private void InitEntity(GameObject entity)
    {
        //var capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        //// change color of capsule to a random color
        //capsule.GetComponent<Renderer>().material.color = Random.ColorHSV();
        //capsule.transform.parent = entity.transform;

        //foreach (var burner in spawnedBurners)
        //{
        //    if (burner.Value == null)
        //    {
        //        spawnedBurners[burner.Key] = entity.name;
        //        break;
        //    }
        //}
    }
}