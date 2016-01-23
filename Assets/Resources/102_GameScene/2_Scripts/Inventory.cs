using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    //public GameObject Item = null;          //얻은 아이템
    public InvenData mData = null;          //InvenData
    //public InvenItemData Name = null;            //템이름

    string Name;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void GetName(string name)
    {
        Name = name;
        //ItemStatus();
    }

    void ItemStatus()
    {
        //아이템 정보 뛰우기
    }

    public void RemoveItem()//아이템 삭제
    {
        mData.RemoveItem(Name);
        Name = null;
    }


}
