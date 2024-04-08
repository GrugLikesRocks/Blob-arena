using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // every state has one of this this needs the
    // the last opened
    // the current opened 
  
    [SerializeField] Menu[] menus;
    [SerializeField] Menu[] nonReturnableMenus;

    public  Menu previousMenu;
    public Menu currentlyOpened;

    public int gamePhase = 1; 
 
    public void OpenMenu(Menu menu)
    {

        if (currentlyOpened == menu)  // if the currently opened menu is the same as the one being called to open again this means its actually a close call
        {
            if (previousMenu != null)
            {
                previousMenu.Open();
                currentlyOpened = previousMenu;
            }
            else
            {
                currentlyOpened = null;
            }

            menu.Close();
            
        }
        else
        {
            if (currentlyOpened != null)  // if there is something opened currenlty
            {
               
                currentlyOpened.Close(); //close the one open now

                if (!nonReturnableMenus.Contains(currentlyOpened))  //chekc if the one that we want to open is in the non savable
                {
                    previousMenu = currentlyOpened; // if it is not we should save it 
                }
                currentlyOpened = menu;
                currentlyOpened.Open();
            }
            else
            {
                menu.Open();
                currentlyOpened = menu;
            }
        }
    }



    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.Space))
        {
            OpenMenu(menus[4]);
        }


        if (Input.GetKeyDown(KeyCode.M))
        {
            OpenMenu(menus[6]);
        }
    }
    public void CloseGame()
    {
        Application.Quit();
    }

}
