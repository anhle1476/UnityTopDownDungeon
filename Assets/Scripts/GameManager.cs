using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        // if the scene is load again, there are multiple GameManager object -> destroy the new object
        if (GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        SceneManager.sceneLoaded += LoadState;
        // preserve the GameManager through scence loads
        DontDestroyOnLoad(gameObject);
    }

    // Resources
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> playerPrices;
    public List<int> xpTable;

    // References
    public Player player;
    // public Weapon weapon
    public FloatingTextManager floatingTextManager;

    // Logic
    public int pesos;
    public int experience;

    public void ShowText(FloatingText.TextInfoDTO textInfo)
    {
        floatingTextManager.Show(textInfo);
    }

    /// <summary>
    /// Save state:
    /// 
    /// INT preferedSkin
    /// INT pesos
    /// INT experinence
    /// INT weapon
    /// </summary>
    public void SaveState()
    {
        string s = $"0|{pesos}|{experience}|0";

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene scene, LoadSceneMode mode)
    {
        if (!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        pesos = int.Parse(data[1]);
        experience = int.Parse(data[2]);
    }


}
