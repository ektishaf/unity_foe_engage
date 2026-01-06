using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SetupMenuView : MonoBehaviour
{
    public Transform slotList;
    public GameObject slotRowPrefab;
    public RectTransform cursor;

    List<RectTransform> rows = new();

    public void Build(List<SlotData> slots)
    {
        rows.Clear();

        foreach (Transform c in slotList)
            Destroy(c.gameObject);

        foreach (var slot in slots)
        {
            var row = Instantiate(slotRowPrefab, slotList);
            row.transform.Find("Text_PilotName").GetComponent<TextMeshProUGUI>().text = slot.pilotName;
            row.transform.Find("Text_MachineName").GetComponent<TextMeshProUGUI>().text = slot.machineName;
            rows.Add(row.GetComponent<RectTransform>());
        }

        MoveCursor(0);
    }

    public void MoveCursor(int index)
    {
        cursor.position = new Vector3(
            cursor.position.x,
            rows[index].position.y,
            cursor.position.z
        );
    }
}
