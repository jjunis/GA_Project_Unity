using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [Header("UI Reference")]
    public TMP_InputField searchItemInputField;
    public Button linearSearchButton;
    public Button bindarySearchButton;
    public TMP_Text descriptionText;
    public GameObject parentObj;     //�̹����� ������ �θ� ������Ʈ
    public GameObject imagePrf;      //�̹��� ������

    public List<GameObject> itemImageObjs = new List<GameObject>();
    public List<Item> items = new List<Item>();

    private void Start()
    {
        if (linearSearchButton == null || bindarySearchButton == null) return;
        linearSearchButton.onClick.AddListener(SetItemByLinear);
        bindarySearchButton.onClick.AddListener(SetItemByBinary);

        //������ ����
        for (int i = 0; i < 100; i++)
        {
            items.Add(new Item($"Item_{i}", 1));

            GameObject temp = Instantiate(imagePrf);
            temp.transform.SetParent(parentObj.transform);  //�ڽ����� �־���
            TextMeshProUGUI text = temp.GetComponentInChildren<TextMeshProUGUI>();
            if (text != null)
            {
                text.text = $"Item_{i}";  //�ؽ�Ʈ �Ҵ�
            }
            itemImageObjs.Add(temp);  //����
        }
    }

    public void SetItemByLinear()
    {
        if (string.IsNullOrEmpty(searchItemInputField.text))
        {
            descriptionText.text = $"You have to set text in InputField!!";
            return;
        }
        descriptionText.text = "";

        GameObject temp = FindItemByLinear(searchItemInputField.text);
        foreach (var itemObj in itemImageObjs)
        {
            if (itemObj != temp)
            {
                Destroy(itemObj);  //�ش� �̹��� ����
            }
        }
    }

    public void SetItemByBinary()
    {
        if (string.IsNullOrEmpty(searchItemInputField.text))
        {
            descriptionText.text = $"You have to set text in InputField!!";
            return;
        }
        descriptionText.text = "";

        GameObject temp = FindItemByBinary(searchItemInputField.text);
        foreach (var itemObj in itemImageObjs)
        {
            if (itemObj != temp)
            {
                Destroy(itemObj);  //�ش� �̹��� ����
            }
        }
    }

    //���� Ž��
    public GameObject FindItemByLinear(string _itemName)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].itemName == _itemName)
                return itemImageObjs[i];
        }
        return null;
    }

    //���� Ž��
    public GameObject FindItemByBinary(string targetName)
    {
        int left = 0;
        int right = items.Count - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            int compare = items[mid].itemName.CompareTo(targetName);

            if (compare == 0)
            {
                return itemImageObjs[mid];
            }
            else if (compare < 0)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }
        return null; //�� ã��
    }
}