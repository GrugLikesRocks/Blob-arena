using TMPro;
using UnityEngine;

public class Fumetto : MonoBehaviour
{
    public TMP_Text textComponent;

    string[] quotes = new string[]
    {
        "Brace thyself; the jelly is mightier than the sword.",
        "Float like a jellyfish, sting like I'm fabulous.",
        "Fear not, for I bring the squish before the squabble.",
        "Dost thou even drift, bro?",
        "Beware, for I am the very gel of valor.",
        "In a sea of knights, be the jellyfish: utterly unforgettable.",
        "Armor? Honey, I'm all natural and still uncatchable.",
        "Who needs a horse when you've got grace and tentacles?",
        "Not all heroes wear capes; some prefer a stylish drift.",
        "They whispered 'monster,' but I heard 'fashion icon.'",
        "In a galaxy far, far away, someone who cares might actually live. Until then, you’re stuck with me, buddy.",
        "Size matters not, unless we're talking about the last piece of pizza. Then, it's war.",
        "A path to the Dark Side, anger is. But honestly, so is standing in the way of my coffee in the morning.",
        "WEN Calc????? WEN Eternum",
        "Is this not what you came for, or do you need me to spell it out in glitter?",
        "Honey, the shade we throw today turns into the legend they can't stop talking about forever.",
        "When I snap these fingers, it’s not just drama I’m unleashing, it’s a whole spectacle. You ready?",
        "Fabulousness and flair, because why settle for anything less?",
        "The clock’s ticking on your little self-admiration party. Hope you enjoyed the spotlight!",
        "Give me all your $STRK"
    };


    private void Start()
    {
        int randomNumber = Random.Range(1, 11);
        textComponent.text = quotes[randomNumber];
    }
}
