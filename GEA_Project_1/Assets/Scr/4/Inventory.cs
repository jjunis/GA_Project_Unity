using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // �κ��丮 ������ ����Ʈ
    public List<Item> items = new List<Item>();

    void Start()
    {
        //���� ������ �߰�
        items.Add(new Item("Sword"));
        items.Add(new Item("Shield"));
        items.Add(new Item("Potion"));

        //������ ã�� �׽�Ʈ
        Item found = FindItem("Potion");

        if (found != null)
            Debug.Log("ã�� ������: " + found.itemName);
        else
            Debug.Log("�������� ã�� �� �����ϴ�.");
    }

    // ���� Ž��
    public Item FindItem(string _itemName)
    {
        foreach (var item in items)
        {
            if (item.itemName == _itemName)
                return item; // �߰� �� ��ȯ
        }
        return null; // �� ã���� null
    }
    void Update()
    {
        
    }
}
