using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InvenData : MonoBehaviour
{
    private List<string> items = new List<string>(); // 아이템 리스트

    private int Count_Item; // 인벤안의 아이템의 갯수

    public int BagLevel; // 가방의 레벨

    public List<string> Items 
    {
        get
        {
            return items;
        }
    }

	// Use this for initialization
	void Start ()
    {
        BagLevel = 1;
    }

    /// <summary>
    /// 아이템을 인벤토리 안에 넣습니다 꽉차면 안들어갑니다
    /// </summary>
    /// <param name="item">아이템의 스트링</param>
    public void AddItem(string item)
    {
        if (BagLevel * 9 <= Count_Item)
        {
            return;
            Debug.Log("Up");
        }
        Debug.Log(item);
        Count_Item += 1;
        items.Add(item);
        Debug.Log(Count_Item);
    }

    /// <summary>
    /// 아이템을 인벤토리에서 뺍니다
    /// </summary>
    /// <param name="item">아이템의 스트링</param>
    public void RemoveItem(string item)
    {
        Debug.Log("remove");
        Debug.Log(item);
        items.Remove(item);
        Count_Item -= 1;
    }

    /// <summary>
    /// 인벤토리가 얼마나 있는지 확인합니다
    /// </summary>
    public void CheckFullInven()
    {
    }

    /// <summary>
    /// 아이템이 들어갔는지 확인합니다
    /// </summary>
    public void DebugItems()
    {
        Debug.Log("Debug Items");
        foreach(var iter in items)
        {
            Debug.Log(iter);
        }
    }

    /// <summary>
    /// 가방의 질을 높입니다
    /// </summary>
    /// <param name="Bag">먹은 가방의 래밸</param>
    public void BagUpgrade(int Bag)
    {
        if(Bag > BagLevel)
        {
            BagLevel = Bag;
        }
    }

    public int BageSize
    {
        get
        {
            return BagLevel * 9;
        }
    }

    /// <summary>
    /// 리스트를 넘겨준다
    /// </summary>
    /// <param name="o_list">저장할 리스트</param>
    public List<string> GetList()
    {
        return items;
    }

    public void SetList(List<string> trade)
    {
        foreach (var iter in trade)
            items.Add(iter);
    }

}
