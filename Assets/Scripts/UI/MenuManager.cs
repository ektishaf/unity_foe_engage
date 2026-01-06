//using UnityEngine;

//public class MenuManager : MonoBehaviour
//{
//    public static MenuManager I;

//    [Header("Panels")]
//    public GameObject mainMenuPanel;
//    public GameObject slotSelectPanel;
//    public GameObject slotMenuPanel;
//    public GameObject subMenuPanel;

//    [HideInInspector] public SlotData currentSlot;

//    void Awake()
//    {
//        I = this;
//        ShowMainMenu();
//    }

//    void HideAll()
//    {
//        mainMenuPanel.SetActive(false);
//        slotSelectPanel.SetActive(false);
//        slotMenuPanel.SetActive(false);
//        subMenuPanel.SetActive(false);
//    }

//    public void ShowMainMenu()
//    {
//        HideAll();
//        mainMenuPanel.SetActive(true);
//    }

//    public void ShowSlotSelect()
//    {
//        HideAll();
//        slotSelectPanel.SetActive(true);
//    }

//    public void SelectSlot(SlotData slot)
//    {
//        currentSlot = slot;
//        ShowSlotMenu();
//    }

//    public void ShowSlotMenu()
//    {
//        HideAll();
//        slotMenuPanel.SetActive(true);
//    }

//    public void ShowSubMenu(string title, System.Action onBuild)
//    {
//        //HideAll();
//        subMenuPanel.SetActive(true);
//        SubMenuBuilder.I.Build(title, onBuild);
//    }

//    public void BackToSlotMenu()
//    {
//        ShowSlotMenu();
//    }
//}
