using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiStateManager : MonoBehaviour
{
    public bool simpleLeaderboardModes = true;

    private void Awake()
    {
        UiReferencesStatic.uiStateManager = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log($"This is the amount of blobs loaded in all {DojoEntitiesStatic.allBlobertDict.Count}");
            Debug.Log($"This si the users blobert should be not null {DojoEntitiesStatic.userBlobertData}");
        
            Debug.Log($"This is the amount of blobs loaded in twoHashesDict {DojoEntitiesStatic.twoHashesDict.Count}");
            Debug.Log($"This is the amount of blobs loaded in twoMovesDict {DojoEntitiesStatic.twoMovesDict.Count}");
            Debug.Log($"This is the amount of blobs loaded in healthsStorage {DojoEntitiesStatic.healthsStorage.Count}");

            Debug.Log($"This is the amount of blobs loaded in knockoutCurrentGame {DojoEntitiesStatic.knockoutCurrentGame}");
            Debug.Log($"This is the amount of blobs loaded in healthsCurrentGame {DojoEntitiesStatic.healthsCurrentGame}");
            Debug.Log($"This is the amount of blobs loaded in twoHashesCurrentGame {DojoEntitiesStatic.twoHashesCurrentGame}");
            Debug.Log($"This is the amount of blobs loaded in twoMovesCurrentGame {DojoEntitiesStatic.twoMovesCurrentGame}");
        }

        //chekc thoruhg all the saved knocouts if more than 0 then
        if (DojoEntitiesStatic.knockoutsList.Count > 0)
        {
            // backwards loop allw sus to remove entries
            for (int i = DojoEntitiesStatic.knockoutsList.Count; i-- > 0;)
            {
                
                //check fi the player is in any of them
                if (DojoEntitiesStatic.knockoutsList[i].playerA.Hex() == DojoEntitiesStatic.currentAccount.Address.Hex() || DojoEntitiesStatic.knockoutsList[i].playerB.Hex() == DojoEntitiesStatic.currentAccount.Address.Hex())
                {
                   //ok the player is in one of them

                    //check if the health is there
                    if (DojoEntitiesStatic.healthsStorage.ContainsKey(DojoEntitiesStatic.knockoutsList[i].combatId))
                    {
                        // if it is check fi the game is ended
                        var healths = DojoEntitiesStatic.healthsStorage[DojoEntitiesStatic.knockoutsList[i].combatId];

                        if (healths.a <= 0 || healths.b <= 0)
                        {
                            DojoEntitiesStatic.knockoutsList.RemoveAt(i);
                        }
                        else
                        {
                            // if there are still healths the game hasnt ended and this is the current game
                            DojoEntitiesStatic.knockoutCurrentGame = DojoEntitiesStatic.knockoutsList[i];
                            DojoEntitiesStatic.healthsCurrentGame = healths;
                        }
                    }
                    else
                    {
                        // if the health is not there but the knockout is we wait for the next as it might be updating
                        continue;
                    }
                }
                else
                {
                    // if he is not we wait for the next
                    DojoEntitiesStatic.knockoutsList.RemoveAt(i);
                }
            }
        }
    }
}
