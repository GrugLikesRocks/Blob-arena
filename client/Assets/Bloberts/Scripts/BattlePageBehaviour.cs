using Dojo.Starknet;
using System;
using TMPro;
using UnityEngine;

public class BattlePageBehaviour : Menu
{
    public TMP_Text addressTextLeft;
    public TMP_Text addressTextRight;

    public GameObject commitButton;
    public GameObject revealButton;

    public TMP_Text hpTextLeft;
    public TMP_Text hpTextRight;

    public BlobertCardData blobertCardDataLeft;
    public BlobertCardData blobertCardDataRight;

    public int secretNumber;
    public BlobertUitls.Move lastMove;

    public int gameState = 0;
    //0 waiating for self to commit
    //1 waiting for other to commit
    //2 waiting for self to reveal



    private void OnEnable()
    {
        addressTextLeft.text = DojoEntitiesStatic.knockoutCurrentGame.playerA.Hex();
        addressTextRight.text = DojoEntitiesStatic.knockoutCurrentGame.playerB.Hex();

        blobertCardDataLeft.SetBlobertId(DojoEntitiesStatic.knockoutCurrentGame.blobertA);
        blobertCardDataRight.SetBlobertId(DojoEntitiesStatic.knockoutCurrentGame.blobertB);

        secretNumber = UnityEngine.Random.Range(0, 100000);
    }



    private void Update()
    {
        var zeroHash = new FieldElement("0");

        if (DojoEntitiesStatic.twoHashesCurrentGame != null)
        {
            if (DojoEntitiesStatic.twoHashesCurrentGame.a != 0 && DojoEntitiesStatic.twoHashesCurrentGame.b != 0 && gameState != 2)
            {
                //means both players have committed
                gameState = 2;

            }
        }
    }


    public async void CallToCommit(int action)
    {
        if (gameState != 0)
        {
            return;
        }

        try
        {
      
            var hash = BlobertUitls.PedersenFunction(secretNumber.ToString(), action.ToString());

            lastMove = (BlobertUitls.Move)action;

            var endpoint = new DojoCallsStatis.EndpointDojoCallStruct
            {
                account = DojoEntitiesStatic.currentAccount,
                addressOfSystem = DojoCallsStatis.knockoutAddress,
                functionName = "commit",
            };

            var structData = new DojoCallsStatis.CommitMoveStruct
            {
                combat_id = DojoEntitiesStatic.knockoutCurrentGame.combatId,
                hash = UInt64.Parse(hash)
            };

            var transaction = await DojoCallsStatis.CreateCommitTransaction(structData, endpoint);

            gameState = 1;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }


    public async void CallToReveal()
    {
        var endpoint = new DojoCallsStatis.EndpointDojoCallStruct
        {
            account = DojoEntitiesStatic.currentAccount,
            addressOfSystem = DojoCallsStatis.knockoutAddress,
            functionName = "reveal",
        };

        var structData = new DojoCallsStatis.RevealMoveStruct
        {
            combat_id = DojoEntitiesStatic.knockoutCurrentGame.combatId,
            move = lastMove,
            salt = (UInt64)secretNumber
        };

        var transaction = await DojoCallsStatis.CreateRevealTransaction(structData, endpoint);
    }
}
