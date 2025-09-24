using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBinary : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    void Start()
    {
        // 인벤토리 초기 아이템 추가
        items.Add(new Item("Potion", 5));
        items.Add(new Item("High-Potion", 2));
        items.Add(new Item("Elixir", 1));
        items.Add(new Item("Sword", 1));

        // 탐색 전 정렬 (이름 기준)
        items.Sort((a, b) => a.itemName.CompareTo(b.itemName));

        // 탐색 테스트
        Item found = FindItem("Elixir");
        if (found != null)
            Debug.Log($"[이진 탐색] 찾은 아이템: {found.itemName}, 개수: {found.quantity}");
        else
            Debug.Log("[이진 탐색] 아이템을 찾을 수 없습니다.");
    }

    public Item FindItem(string targetName)
    {
        int left = 0;
        int right = items.Count - 1;

        while (left <= right)
        {
            int mid = (left + right) / 2;
            int compare = items[mid].itemName.CompareTo(targetName);

            if (compare == 0)
            {
                return items[mid]; //찾음
            }
            else if (compare < 0)
            {
                left = mid + 1; //오른쪽 탐색
            }
            else
            {
                right = mid - 1; //왼쪽 탐색
            }
        }
        return null; //못 찾음
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
