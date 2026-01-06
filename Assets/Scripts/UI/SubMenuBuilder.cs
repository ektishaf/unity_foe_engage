//using UnityEngine;
//using UnityEngine.UI;
//using System;
//using TMPro;

//public class SubMenuBuilder : MonoBehaviour
//{
//    public static SubMenuBuilder I;

//    public TextMeshProUGUI titleText;
//    public Transform content;
//    public GameObject buttonPrefab;

//    void Awake()
//    {
//        I = this;
//    }

//    public void Build(string title, Action buildButtons)
//    {
//        titleText.text = title;

//        foreach (Transform c in content)
//            Destroy(c.gameObject);

//        buildButtons?.Invoke();
//    }

//    public void AddButton(string label, Action onClick)
//    {
//        GameObject btn = Instantiate(buttonPrefab, content);
//        btn.GetComponentInChildren<TextMeshProUGUI>().text = label;
//        btn.GetComponent<Button>().onClick.AddListener(() => onClick());
//    }
//}
