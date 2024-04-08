using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;
using TMPro;

public class CopyToClipboard : MonoBehaviour, IPointerDownHandler
{
    [DllImport("__Internal")]
    private static extern void CopyToClipboardImpl(string text);

    public void OnPointerDown(PointerEventData eventData)
    {
        var textToCopy = GetComponent<TMP_Text>().text;
#if UNITY_WEBGL && !UNITY_EDITOR
        CopyToClipboardImpl(textToCopy);
#else
        GUIUtility.systemCopyBuffer = textToCopy;
#endif
        Debug.Log($"Copied: {textToCopy}");
    }
}
