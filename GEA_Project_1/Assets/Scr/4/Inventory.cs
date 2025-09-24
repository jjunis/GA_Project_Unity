using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // 인벤토리 아이템 리스트
    public List<Item> items = new List<Item>();

    void Start()
    {
        //예시 아이템 추가
        items.Add(new Item("Sword"));
        items.Add(new Item("Shield"));
        items.Add(new Item("Potion"));

        //아이템 찾기 테스트
        Item found = FindItem("Potion");

        if (found != null)
            Debug.Log("찾은 아이템: " + found.itemName);
        else
            Debug.Log("아이템을 찾을 수 없습니다.");
    }

    // 선형 탐색
    public Item FindItem(string _itemName)
    {
        foreach (var item in items)
        {
            if (item.itemName == _itemName)
                return item; // 발견 시 반환
        }
        return null; // 못 찾으면 null
    }
    void Update()
    {
        
    }
}
