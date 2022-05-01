using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Handle showing text for the game with a pool of <see cref="FloatingText"/> objects.
/// </summary>
public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private readonly ICollection<FloatingText> floatingTexts = new List<FloatingText>();

    /// <summary>
    /// Return the first in-active text object. If there is not, create a new one
    /// </summary>
    /// <returns></returns>
    private FloatingText GetFloatingText()
    {
        FloatingText floatingText = floatingTexts.FirstOrDefault(t => !t.active);

        if (floatingText == null)
        {
            floatingText = new FloatingText();
            floatingText.go = Instantiate(textPrefab);
            floatingText.go.transform.SetParent(textContainer.transform);
            floatingText.txt = floatingText.go.GetComponent<TextMeshProUGUI>();

            floatingTexts.Add(floatingText);
        } 

        return floatingText;
    }

    public void Show(FloatingText.TextInfoDTO textInfo)// string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText floatingText = GetFloatingText();

        floatingText.txt.text              = textInfo.Message;
        floatingText.txt.fontSize          = textInfo.FontSize;
        floatingText.txt.color             = textInfo.Color;

        // Transfer world space to screen space so the Text will show up in the camera
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(textInfo.Position);
        floatingText.motion                = textInfo.Motion;
        floatingText.duration              = textInfo.Duration;

        floatingText.Show();
    }

    private void Update()
    {
        foreach (var txt in floatingTexts)
            txt.UpdateFloatingText();
    }
}
