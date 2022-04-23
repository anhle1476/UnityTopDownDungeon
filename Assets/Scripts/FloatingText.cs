using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText
{
    #region DTO

    /// <summary>
    /// DTO contains the necessary information to initiate a FloatingText object
    /// </summary>
    public class TextInfoDTO
    {
        public string Message { get; set; }
        public int FontSize { get; set; } = 22;
        public Color Color { get; set; } = Color.white;
        public Vector3 Position { get; set; }
        public Vector3 Motion { get; set; } = Vector3.zero;
        public float Duration { get; set; } = 0.5f;
    }

    #endregion

    public bool active;
    public GameObject go;
    public TextMeshProUGUI txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    /// <summary>
    /// Show, enable the text and update <see cref="lastShown"/>
    /// </summary>
    public void Show()
    {
        active = true;
        lastShown = Time.time;
        UpdateGameObjectActive();
    }

    /// <summary>
    /// Hide and disable the text
    /// </summary>
    public void Hide()
    {
        active = false;
        UpdateGameObjectActive();
    }

    /// <summary>
    ///  Update the text's GameObject with the current active state
    /// </summary>
    private void UpdateGameObjectActive()
    {
        go.SetActive(active);
    }

    /// <summary>
    /// Update the text, will be called on each frame
    /// </summary>
    public void UpdateFloatingText()
    {
        if (!active)
            return;

        if (Time.time - lastShown > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime;
    }
}
