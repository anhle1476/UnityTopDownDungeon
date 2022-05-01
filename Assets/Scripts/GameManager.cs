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
    [SerializeField]
    public List<Sprite> playerSprites;
    [SerializeField]
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References
    public Player player;
    public Weapon weapon;
    public FloatingTextManager floatingTextManager;

    // Logic
    public int pesos;
    public int experience;

    /// <summary>
    /// Show floating text
    /// </summary>
    /// <param name="textInfo"></param>
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


    public bool TryUpgradeWeapon()
    {
        if (CanUpgradeWeapon(out int nextWeaponPrice))
        {
            pesos -= nextWeaponPrice;
            weapon.UpgradeWeapon();
            return true;
        }
        return false;
    }

    public bool CanUpgradeWeapon(out int nextWeaponPrice)
    {
        nextWeaponPrice = 0;
        if (weaponSprites.Count <= weapon.weaponLevel)
            return false;
        // next weapon lv N will be have their price stored in index N-1 (0 based index)
        nextWeaponPrice = weaponPrices[weapon.weaponLevel];
        return pesos >= nextWeaponPrice;
    }

}
