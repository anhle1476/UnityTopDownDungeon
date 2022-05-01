using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMenu : MonoBehaviour
{
    // text fields
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI hitpointText;
    public TextMeshProUGUI pesosText;
    public TextMeshProUGUI xpText;

    // btn
    public Button weaponUpgradeBtn;
    private TextMeshProUGUI weaponUpgradeText;

    // Logic
    private int currentCharacterSelection = 0;
    public Image characterSelectionSprite;
    public Image weaponSprite;
    public RectTransform xpBar;

    protected virtual void Start()
    {
        weaponUpgradeText = weaponUpgradeBtn.GetComponentInChildren<TextMeshProUGUI>();
    }

    /// <summary>
    /// Handle character selection
    /// </summary>
    public void OnCharacterSelectionClick(bool right)
    {
        if (right)
        {
            currentCharacterSelection++;
        }
        else
        {
            currentCharacterSelection--;
        }

        // go to first if it's already the last sprite and vice versa
        if (currentCharacterSelection >= GameManager.instance.playerSprites.Count)
        {
            currentCharacterSelection = 0;
        }
        else if (currentCharacterSelection < 0)
        {
            currentCharacterSelection = GameManager.instance.playerSprites.Count - 1;
        }

        OnCharacterSelectionChanged();
    }

    /// <summary>
    /// Handle weapon upgrade click
    /// </summary>
    public void OnWeaponUpgradeClick()
    {
        if (GameManager.instance.TryUpgradeWeapon())
        {
            UpdateMenu();
        }
    }

    /// <summary>
    /// Update the character information
    /// </summary>
    public void UpdateMenu()
    {
        // Weapon
        int weaponLevel = GameManager.instance.weapon.weaponLevel;
        weaponSprite.sprite = GameManager.instance.weaponSprites[weaponLevel - 1];
        bool canUpgrade = GameManager.instance.CanUpgradeWeapon(out int nextWeaponPrice);
        weaponUpgradeBtn.interactable = canUpgrade;
        weaponUpgradeText.text = nextWeaponPrice == 0 ? "MAX" : nextWeaponPrice.ToString();

        // Meta
        hitpointText.text = GameManager.instance.player.hitPoint.ToString();
        pesosText.text = GameManager.instance.pesos.ToString();
        levelText.text = "NOT IMPLELEMTED";

        // xp Bar
        xpText.text = "NOT IMPLELEMTED";
        xpBar.localScale = new Vector3(0.3f, 0, 0);

    }

    /// <summary>
    /// On character selection change
    /// </summary>
    protected virtual void OnCharacterSelectionChanged()
    {
        Sprite selectedSprite = GameManager.instance.playerSprites[currentCharacterSelection];
        // update character in menu
        characterSelectionSprite.sprite = selectedSprite;
        // update character in game
        GameManager.instance.player.GetComponent<SpriteRenderer>().sprite = selectedSprite;
    }
}
