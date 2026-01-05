using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WeaponItem
{
    public string weaponName;
    public GameObject prefab;
    public int cost;
    public WeaponSlot slotType; // HandRight, HandLeft, ShoulderRight, ShoulderLeft
}

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [Header("UI References")]
    public Transform contentParent; // For weapon buttons
    public Button weaponButtonPrefab;
    public Text goldText;

    [Header("Weapons For Sale")]
    public List<WeaponItem> shopWeapons;

    [Header("Player Inventory")]
    public List<WeaponItem> playerInventory = new List<WeaponItem>();
    public int playerGold = 500;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        RefreshShopUI();
        UpdateGoldUI();
    }

    void RefreshShopUI()
    {
        // Clear previous
        foreach (Transform child in contentParent) Destroy(child.gameObject);

        foreach (var weapon in shopWeapons)
        {
            Button btn = Instantiate(weaponButtonPrefab, contentParent);
            btn.GetComponentInChildren<Text>().text = $"{weapon.weaponName} ({weapon.cost}G)";

            btn.onClick.AddListener(() => TryBuyWeapon(weapon));
        }
    }

    void TryBuyWeapon(WeaponItem weapon)
    {
        if (playerGold < weapon.cost)
        {
            Debug.Log("Not enough gold!");
            return;
        }

        playerGold -= weapon.cost;
        playerInventory.Add(weapon);

        UpdateGoldUI();

        Debug.Log($"Bought {weapon.weaponName}!");
    }

    void UpdateGoldUI()
    {
        if (goldText != null) goldText.text = $"Gold: {playerGold}";
    }

    // Equip weapon to garage
    public void EquipToActiveRobot(WeaponItem weapon)
    {
        if (!playerInventory.Contains(weapon))
        {
            Debug.Log("Weapon not in inventory!");
            return;
        }

        GarageManager gm = GarageManager.Instance;
        if (gm == null) return;

        // Find dropdown index corresponding to this weapon
        int dropdownIndex = 0; // default "None"

        if (weapon.slotType == WeaponSlot.HandRight)
            dropdownIndex = gm.handWeaponPrefabs.IndexOf(weapon.prefab) + 1;
        else if (weapon.slotType == WeaponSlot.HandLeft)
            dropdownIndex = gm.handWeaponPrefabs.IndexOf(weapon.prefab) + 1;
        else if (weapon.slotType == WeaponSlot.ShoulderRight)
            dropdownIndex = gm.shoulderWeaponPrefabs.IndexOf(weapon.prefab) + 1;
        else if (weapon.slotType == WeaponSlot.ShoulderLeft)
            dropdownIndex = gm.shoulderWeaponPrefabs.IndexOf(weapon.prefab) + 1;

        gm.EquipWeapon(weapon.slotType, dropdownIndex);
    }

    // Unequip weapon from active robot
    public void UnequipFromActiveRobot(WeaponSlot slot)
    {
        GarageManager gm = GarageManager.Instance;
        if (gm == null) return;

        gm.EquipWeapon(slot, 0); // 0 = None
    }
}
