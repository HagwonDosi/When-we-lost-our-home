using UnityEngine;
using System.Collections;

/// <summary>
/// 아이템 슬롯에도 쓸 수 있는 스크립트
/// </summary>
public class ItemInfo : MonoBehaviour
{
    public string Item_Name;

    public string GetName()
    {
        return Item_Name;
    }

    public void OnPress()
    {
        ItemDirector.Instance.SelectedItem = Item_Name;
    }
}