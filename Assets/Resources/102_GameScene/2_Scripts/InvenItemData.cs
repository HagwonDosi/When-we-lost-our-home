using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InvenItemData : MonoBehaviour {

    public UISprite Itemname = null;
    public Inventory Yes = null;

    string Name;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    public string ItemNameReturn()//sprite이름 반환
    {
        Name = Itemname.spriteName;
        return Name;
    }

    public void OnPress()//yes no에 이름 저장
    {
        Name = Itemname.spriteName;
        Yes.GetName(Name);
    }
}
