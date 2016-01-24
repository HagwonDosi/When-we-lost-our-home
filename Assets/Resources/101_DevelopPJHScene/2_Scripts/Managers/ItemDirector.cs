using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StringTrigger
{
    public string mStr = "";
    public UITrigger mTrigger = null;
}

[System.Serializable]
public class StringObject
{
    public string mStr = "";
    public GameObject mObj = null;
}

public class ItemDirector : Singletone<ItemDirector>
{
    [SerializeField]
    private List<StringTrigger> mTriggerList = new List<StringTrigger>();
    [SerializeField]
    private List<StringObject> mItemList = new List<StringObject>();
    private string mSelectedItem = "";
    private Dictionary<string, UITrigger> mTriggerDic = new Dictionary<string, UITrigger>();
    private InvenData mPlayerInven = null;

    public string SelectedItem
    {
        set
        {
            mSelectedItem = value;
        }
        get
        {
            return mSelectedItem;
        }
    }

    void Start()
    {
        mPlayerInven = GameDirector.Instance.PlayerInven;

        foreach(var iter in mTriggerList)
        {
            mTriggerDic.Add(iter.mStr, iter.mTrigger);
        }
    }

    public void SelectedItemUse()
    {
        UITrigger trigger = null;

        if(mTriggerDic.TryGetValue(mSelectedItem, out trigger))
        {
            mSelectedItem = "";
            mPlayerInven.RemoveItem(mSelectedItem);
            trigger.Act();
        }
        else
        {
            Debug.LogWarning(name + ".ItemDirector.SelectedItemUse() " + "item " + mSelectedItem + " was not found");
        }
    }

    public void LoadItem(string fMapName)
    {

    }
}
