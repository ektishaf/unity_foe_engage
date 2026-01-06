//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class SlotSelectUI : MonoBehaviour
//{
//    public Transform content;
//    public GameObject slotRowPrefab;

//    public List<SlotData> slots;

//    void OnEnable()
//    {
//        Refresh();
//    }

//    void Refresh()
//    {
//        foreach (Transform c in content)
//            Destroy(c.gameObject);

//        foreach (var slot in slots)
//        {
//            GameObject row = Instantiate(slotRowPrefab, content);
//            row.transform.Find("Text_PilotName").GetComponent<TextMeshProUGUI>().text = slot.pilotName;
//            row.transform.Find("Text_MachineName").GetComponent<TextMeshProUGUI>().text = slot.robot.machineName;

//            row.transform.Find("Button_Select")
//                .GetComponent<Button>()
//                .onClick.AddListener(() => MenuManager.I.SelectSlot(slot));
//        }
//    }
//}
