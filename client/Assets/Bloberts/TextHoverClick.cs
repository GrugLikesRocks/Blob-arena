using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextHoverClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TMP_Text textComponent;
    private string originalText;

    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
        if (textComponent != null)
        {
            originalText = textComponent.text; // Save the original text
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (textComponent != null)
        {
            textComponent.text = $"<{originalText}>"; // Add < > around the text on hover
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (textComponent != null)
        {
            textComponent.text = originalText; // Revert to the original text when the pointer exits
        }
    }
}
