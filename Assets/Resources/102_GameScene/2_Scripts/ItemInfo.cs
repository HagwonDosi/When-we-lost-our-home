using UnityEngine;
using System.Collections;

public class ItemInfo : MonoBehaviour
{
    public string Item_Name;
    public Trade trade;
    public bool Player;
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public string GetName()
    {
        return Item_Name;
    }

    public void GiveItem()
    {
        if (Player == false)
            trade.SetNList(transform.tag);
        if (Player == true)
            trade.SetPList(transform.tag);
    }
}