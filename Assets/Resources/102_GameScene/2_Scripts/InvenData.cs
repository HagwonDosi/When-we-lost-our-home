using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InvenData : MonoBehaviour
{
    private List<string> items = new List<string>();

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
    }

    public void AddItem(string item)
    {
        Debug.Log(item);
        items.Add(item);
    }

    public void RemoveItem(string item)
    {
        Debug.Log("remove");
        Debug.Log(item);
        items.Remove(item);
    }

    public void DebugItems()
    {
        Debug.Log("Debug Items");
        foreach(var iter in items)
        {
            Debug.Log(iter);
        }
    }
}
