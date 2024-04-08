using Dojo.Starknet;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlobertCardData : MonoBehaviour
{
    [SerializeField] RawImage blobertPic;

    [SerializeField] TMP_Text swordText;
    [SerializeField] TMP_Text bicepText;
    [SerializeField] TMP_Text shieldText;
    [SerializeField] TMP_Text shoesText;

    public FieldElement blobertId;
    public Blobert blobert;

    public void SetBlobertId(FieldElement id)
    {
        blobertId = id;

        blobert = DojoEntitiesStatic.allBlobertDict[id.Hex()];
        //get the data and everything from here too

        SetBicepText(blobert.stats.strength.ToString());
        SetSwordText(blobert.stats.attack.ToString());
        SetShieldText(blobert.stats.defense.ToString());
        SetShoesText(blobert.stats.speed.ToString());
    }

    public void SetSwordText(string text)
    {
        swordText.text = text;
    }

    public void SetBicepText(string text)
    {
        bicepText.text = text;
    }

    public void SetShieldText(string text)
    {
        shieldText.text = text;
    }

    public void SetShoesText(string text)
    {
        shoesText.text = text;
    }

    public void SetBlobertPic(Texture2D texture)
    {
        blobertPic.texture = texture;
    }
}
