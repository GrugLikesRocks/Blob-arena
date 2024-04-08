using Dojo.Starknet;
using TMPro;
using UnityEngine;

public class CreateLobbyBehavior : Menu
{
    public MenuManager menuManager;
    public Menu battlePhase;
    //se the data for the blobert and have an input field take waya the button

    public BlobertCardData blobertCardData;
    public TMP_InputField inputFieldOtherPlayer;
    public TMP_InputField inputFieldOtherBlob;

    public TMP_Text idText;
    public TMP_Text addressText;

    private void OnEnable()
    {
        var blobert = DojoEntitiesStatic.userBlobertData;

        addressText.text = DojoEntitiesStatic.currentAccount.Address.Hex();

        blobertCardData.SetBicepText(blobert.stats.strength.ToString());
        blobertCardData.SetShoesText(blobert.stats.speed.ToString());
        blobertCardData.SetSwordText(blobert.stats.attack.ToString());
        blobertCardData.SetShieldText(blobert.stats.defense.ToString());

        idText.text =  $"ID: {BlobertUitls.HexToBigInt(blobert.blobertId.Hex())}";
    }

    public async void CreateLobby()
    {
        // if input field is empty return
        if (string.IsNullOrEmpty(inputFieldOtherPlayer.text))
        {
            return;
        }

        // if input field is empty return
        if (string.IsNullOrEmpty(inputFieldOtherBlob.text))
        {
            return;
        }

        var endpoint = new DojoCallsStatis.EndpointDojoCallStruct
        {
            account = DojoEntitiesStatic.currentAccount,
            functionName = "new",
            addressOfSystem = DojoCallsStatis.knockoutAddress
        };

        Debug.Log("Creating new game");
        Debug.Log("Player A: " + DojoEntitiesStatic.currentAccount.Address.Hex());
        Debug.Log("Player B: " + new FieldElement(inputFieldOtherPlayer.text).Hex());
        Debug.Log("Blobert A: " + DojoEntitiesStatic.userBlobertData.blobertId.Hex());
        Debug.Log("Blobert B: " + new FieldElement(inputFieldOtherBlob.text).Hex());

        var dataStruct = new DojoCallsStatis.CreateNewGameStruct
        {
            player_a = DojoEntitiesStatic.currentAccount.Address,
            player_b = new FieldElement(inputFieldOtherPlayer.text),
            blobert_aID = DojoEntitiesStatic.userBlobertData.blobertId,
            blobert_bID = new FieldElement(inputFieldOtherBlob.text)
        };

        var transaction = await DojoCallsStatis.CreateNewGame(dataStruct, endpoint);

    }


    void Update()
    {
        if (DojoEntitiesStatic.healthsCurrentGame != null && DojoEntitiesStatic.knockoutCurrentGame != null)
        {
            Debug.Log("we move to the next phase");
            menuManager.OpenMenu(battlePhase);
        }
    }
}
