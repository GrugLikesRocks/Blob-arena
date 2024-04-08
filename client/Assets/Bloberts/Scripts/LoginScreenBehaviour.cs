using Dojo;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginScreenBehaviour : Menu
{
    // this is the way

    // firts anim starts
    // on end it calls the anim for the button
    // on end it calls the fade in
    // on end this should also call the interactive on

    // when the burner is done change the text to the the address and start the blobert anim with the button
    // once both are done the interactive on the button is on
    // on groundhit the camera should shake
    // the particle should play

    // bloberts should blob
    //till the new blobert is minted, once minter we go onto the next screen
    


    public RawImage rawImage;

    public Animator blobertAnimLeft; // Assign this in the inspector
    public Animator blobertAnimRight; // Assign this in the inspector
    public Animator mintBlobertAnim; // Assign this in the inspector

    public Transform cameraTransform;

    public TMP_Text addressText;

    public GameManager gameManager;


    public Menu mainMenu;
    public MenuManager menuManager;

    public WorldManager worldManager;

    public GameObject popUp;


    private bool burnerCreated = false;
  
    public void CreateBurner()
    {
        gameManager.CreateBurner();
    }

    private void Update()
    {
        if (!burnerCreated && DojoEntitiesStatic.currentAccount != null)
        {
            burnerCreated = true;

            blobertAnimLeft.SetTrigger("comeDown");
            blobertAnimRight.SetTrigger("comeDown");

            mintBlobertAnim.SetTrigger("comeUp");

            addressText.text = $"{DojoEntitiesStatic.currentAccount.Address.Hex().Substring(0,6)}...";

            worldManager.LoadData();

            popUp.SetActive(true);
        }

        if (burnerCreated && DojoEntitiesStatic.userBlobertData != null)
        {
            menuManager.OpenMenu(mainMenu);
        }
    }

    public void Shake(float duration, float magnitude)
    {
        StartCoroutine(FadeIn(rawImage, 2f));
        StartCoroutine(ShakeCoroutine(5f, 10f));
    }

    IEnumerator FadeIn(RawImage image, float duration)
    {
        float elapsedTime = 0.25f;
        Color c = image.color;


        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsedTime / duration);
            image.color = c;
            yield return null;
        }
    }

    IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        Vector3 originalPosition = cameraTransform.localPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            cameraTransform.localPosition = new Vector3(x, y, originalPosition.z);

            elapsedTime += Time.deltaTime;

            // Gradually reduce the magnitude over time
            magnitude = Mathf.Lerp(magnitude, 0, elapsedTime / duration);

            yield return null; // Wait until the next frame
        }

        // Reset the camera position
        cameraTransform.localPosition = originalPosition;
    }



    public async void MintBlobert()
    {
        var endpointData = new DojoCallsStatis.EndpointDojoCallStruct
        {
            account = DojoEntitiesStatic.currentAccount,
            addressOfSystem = DojoCallsStatis.blobertActionsAddress,
            functionName = "mint"
        };

        var structData = new DojoCallsStatis.MintBlobertStruct
        {
            owner = DojoEntitiesStatic.currentAccount.Address
        };

        var something = await DojoCallsStatis.MintBlobert(structData, endpointData);
    }


}
