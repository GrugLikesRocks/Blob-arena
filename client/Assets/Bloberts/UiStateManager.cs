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
            Debug.Log($"This is the amount of blobs loaded in healthsStorage {DojoEntitiesStatic.healthsList.Count}");
            Debug.Log($"This is the amount of blobs loaded in knockoutsList {DojoEntitiesStatic.knockoutsList.Count}");

            if (DojoEntitiesStatic.knockoutCurrentGame != null)
            {
                Debug.Log("knowouct current game not null");
            }
            else
            {
                Debug.Log("knowouct current game null");
            }

            if (DojoEntitiesStatic.healthsCurrentGame != null)
            {
                Debug.Log("healths current game not null");
            }
            else
            {
                Debug.Log("healths current game null");
            }

            if (DojoEntitiesStatic.twoHashesCurrentGame != null)
            {
                Debug.Log("twoHashes current game not null");
            }
            else
            {
                Debug.Log("twoHashes current game null");
            }


            if (DojoEntitiesStatic.twoMovesCurrentGame != null)
            {
                Debug.Log("twoMoves current game not null");
            }
            else
            {
                Debug.Log("twoMoves current game null");
            }

        }

        //chekc thoruhg all the saved knocouts if more than 0 then
        if (DojoEntitiesStatic.knockoutsList.Count > 0 && DojoEntitiesStatic.healthsList.Count > 0)
        {
            Debug.Log("There are knockouts in the list");
            // backwards loop allw sus to remove entries
            for (int i = DojoEntitiesStatic.knockoutsList.Count; i-- > 0;)
            {
                //check fi the player is in any of them
                if (DojoEntitiesStatic.knockoutsList[i].playerA.Hex() == DojoEntitiesStatic.currentAccount.Address.Hex() || DojoEntitiesStatic.knockoutsList[i].playerB.Hex() == DojoEntitiesStatic.currentAccount.Address.Hex())
                {
                    Debug.Log("The player is in one of the games");
                    //ok the player is in one of them

                    //check if the health is there

                    foreach (var item in DojoEntitiesStatic.healthsList)
                    {
                        Debug.Log("Checking the health");
                        Debug.Log("The health combat id is " + item.combatId.Hex());
                        Debug.Log("The knockout combat id is " + DojoEntitiesStatic.knockoutsList[i].combatId.Hex());
                        if (item.combatId.Hex() == DojoEntitiesStatic.knockoutsList[i].combatId.Hex())
                        {
                            Debug.Log("The health is in the list");
                            if (item.a <= 0 || item.b <= 0)
                            {
                                DojoEntitiesStatic.knockoutsList.RemoveAt(i);
                            }
                            else
                            {
                                Debug.Log("The health is not 0 WE FOUND IT");
                                DojoEntitiesStatic.knockoutCurrentGame = DojoEntitiesStatic.knockoutsList[i];
                                DojoEntitiesStatic.healthsCurrentGame = item;

                                break;
                            }
                        }
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
