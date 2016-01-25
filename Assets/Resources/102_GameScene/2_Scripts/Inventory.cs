using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public GameObject Item = null;          //얻은 아이템
    public InvenData mData = null;          //InvenData
    public ItemInfo Name = null;            //템이름

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}


    public void GetItem()
    {
        mData.AddItem(Name.GetName());
    }
}
