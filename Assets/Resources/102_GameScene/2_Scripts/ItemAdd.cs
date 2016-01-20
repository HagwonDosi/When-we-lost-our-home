using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemAdd : MonoBehaviour {

    public ItemInfo ItmeName = null;
    public InvenData mData = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GetItem()
    {
        mData.AddItem(ItmeName.GetName());
    }
}
