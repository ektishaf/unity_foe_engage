using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Enum for weapon slots
public enum WeaponSlot
{
    HandRight,
    HandLeft,
    ShoulderRight,
    ShoulderLeft
}

[System.Serializable]
public class RobotSlot
{
    public string slotName;
    public GameObject robotPrefab;   // Full robot prefab
    [HideInInspector] public GameObject previewInstance; // Instance in preview
    public Dictionary<WeaponSlot, GameObject> equippedWeapons = new Dictionary<WeaponSlot, GameObject>();
}

public class GarageManager : MonoBehaviour
{
    public static GarageManager Instance;

    [Header("Robot Slots")]
    public RobotSlot[] robotSlots = new RobotSlot[4];
    private int activeSlotIndex = 0;

    [Header("UI References")]
    public Button[] slotButtons;
    public Dropdown dropdownHandRight;
    public Dropdown dropdownHandLeft;
    public Dropdown dropdownShoulderRight;
    public Dropdown dropdownShoulderLeft;
    public Text textStats;

    [Header("Preview")]
    public Transform previewRoot;
    public float rotationSpeed = 20f;

    [Header("Weapon Prefabs")]
    public List<GameObject> handWeaponPrefabs;
    public List<GameObject> shoulderWeaponPrefabs;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Init();
    }

    public void Init()
    {
        // Instantiate robot previews
        for (int i = 0; i < robotSlots.Length; i++)
        {
            var slot = robotSlots[i];
            if (slot.robotPrefab != null)
            {
                slot.previewInstance = Instantiate(slot.robotPrefab, previewRoot);
                slot.previewInstance.SetActive(i == activeSlotIndex);
            }

            // Initialize weapon dictionary
            slot.equippedWeapons[WeaponSlot.HandRight] = null;
            slot.equippedWeapons[WeaponSlot.HandLeft] = null;
            slot.equippedWeapons[WeaponSlot.ShoulderRight] = null;
            slot.equippedWeapons[WeaponSlot.ShoulderLeft] = null;
        }

        // Setup UI callbacks
        for (int i = 0; i < slotButtons.Length; i++)
        {
            int index = i;
            slotButtons[i].onClick.AddListener(() => SwitchSlot(index));
        }

        dropdownHandRight.onValueChanged.AddListener((v) => EquipWeapon(WeaponSlot.HandRight, v));
        dropdownHandLeft.onValueChanged.AddListener((v) => EquipWeapon(WeaponSlot.HandLeft, v));
        dropdownShoulderRight.onValueChanged.AddListener((v) => EquipWeapon(WeaponSlot.ShoulderRight, v));
        dropdownShoulderLeft.onValueChanged.AddListener((v) => EquipWeapon(WeaponSlot.ShoulderLeft, v));

        UpdatePreview();
    }

    private void Update()
    {
        // Rotate active preview continuously
        if (robotSlots[activeSlotIndex].previewInstance != null)
        {
            robotSlots[activeSlotIndex].previewInstance.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    public void SwitchSlot(int index)
    {
        if (index == activeSlotIndex) return;

        // Hide previous
        robotSlots[activeSlotIndex].previewInstance.SetActive(false);

        activeSlotIndex = index;

        // Show new
        robotSlots[activeSlotIndex].previewInstance.SetActive(true);

        UpdatePreview();
    }

    public void EquipWeapon(WeaponSlot slot, int dropdownIndex)
    {
        RobotSlot activeSlot = robotSlots[activeSlotIndex];
        GameObject[] prefabs = slot == WeaponSlot.HandRight || slot == WeaponSlot.HandLeft ? handWeaponPrefabs.ToArray() : shoulderWeaponPrefabs.ToArray();

        // Remove previous weapon
        if (activeSlot.equippedWeapons[slot] != null)
            Destroy(activeSlot.equippedWeapons[slot]);

        if (dropdownIndex <= 0) // 0 = None
        {
            activeSlot.equippedWeapons[slot] = null;
        }
        else
        {
            // Instantiate weapon
            GameObject weaponPrefab = prefabs[dropdownIndex - 1];
            Transform mountPoint = GetMountPoint(activeSlot.previewInstance, slot);

            if (mountPoint != null)
            {
                GameObject weapon = Instantiate(weaponPrefab, mountPoint);
                weapon.transform.localPosition = Vector3.zero;
                weapon.transform.localRotation = Quaternion.identity;
                activeSlot.equippedWeapons[slot] = weapon;
            }
        }

        UpdatePreview();
    }

    private Transform GetMountPoint(GameObject robot, WeaponSlot slot)
    {
        switch (slot)
        {
            case WeaponSlot.HandRight: return robot.transform.Find("Mounts/RightHand");
            case WeaponSlot.HandLeft: return robot.transform.Find("Mounts/LeftHand");
            case WeaponSlot.ShoulderRight: return robot.transform.Find("Mounts/RightShoulder");
            case WeaponSlot.ShoulderLeft: return robot.transform.Find("Mounts/LeftShoulder");
            default: return null;
        }
    }

    private void UpdatePreview()
    {
        RobotSlot activeSlot = robotSlots[activeSlotIndex];

        // Update stats text
        string stats = $"Robot: {activeSlot.slotName}\n";
        stats += "Weapons:\n";
        foreach (var kvp in activeSlot.equippedWeapons)
        {
            stats += $"{kvp.Key}: {(kvp.Value != null ? kvp.Value.name.Replace("(Clone)", "") : "None")}\n";
        }
        textStats.text = stats;
    }

    // Call this when starting the battle
    public RobotSlot[] GetAllRobots()
    {
        return robotSlots;
    }
}
