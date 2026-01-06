//using UnityEngine;

//public class SlotMenuUI : MonoBehaviour
//{
//    public void OnSetup()
//    {
//        MenuManager.I.ShowSubMenu("Setup", () =>
//        {
//            SubMenuBuilder.I.AddButton("Body", () => Debug.Log("Body"));
//            SubMenuBuilder.I.AddButton("L Arm", () => Debug.Log("L Arm"));
//            SubMenuBuilder.I.AddButton("R Arm", () => Debug.Log("R Arm"));
//            SubMenuBuilder.I.AddButton("Legs", () => Debug.Log("Legs"));
//            SubMenuBuilder.I.AddButton("Backpack", () => Debug.Log("Backpack"));
//        });
//    }

//    public void OnWeapon()
//    {
//        MenuManager.I.ShowSubMenu("Weapon", () =>
//        {
//            SubMenuBuilder.I.AddButton("L Hand", () => Equip("L Hand"));
//            SubMenuBuilder.I.AddButton("R Hand", () => Equip("R Hand"));
//            SubMenuBuilder.I.AddButton("L Shoulder", () => Equip("L Shoulder"));
//            SubMenuBuilder.I.AddButton("R Shoulder", () => Equip("R Shoulder"));
//        });
//    }

//    public void OnBodySettings()
//    {
//        MenuManager.I.ShowSubMenu("Body Settings", () =>
//        {
//            SubMenuBuilder.I.AddButton("Machine Color", () => Debug.Log("Color"));
//            SubMenuBuilder.I.AddButton("Machine Name", () => Debug.Log("Rename"));
//        });
//    }

//    public void OnPilot()
//    {
//        MenuManager.I.ShowSubMenu("Pilot", () =>
//        {
//            SubMenuBuilder.I.AddButton("Computer", () => Debug.Log("Computer"));
//            SubMenuBuilder.I.AddButton("Battle Skill", () => Debug.Log("Battle Skill"));
//        });
//    }

//    void Equip(string slot)
//    {
//        Debug.Log($"Equip {slot} for {MenuManager.I.currentSlot.robot.machineName}");
//    }
//}
