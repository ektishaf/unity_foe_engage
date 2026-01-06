using UnityEngine;

public class SetupMenuController : MonoBehaviour
{
    public SetupMenuView view;

    int selectedIndex = 0;

    void Start()
    {
        view.Build(MenuData.Slots);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = Mathf.Min(selectedIndex + 1, MenuData.Slots.Count - 1);
            view.MoveCursor(selectedIndex);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = Mathf.Max(selectedIndex - 1, 0);
            view.MoveCursor(selectedIndex);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log($"Slot {selectedIndex + 1} selected");
        }
    }
}
