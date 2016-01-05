using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    public GameObject inventory = null;
    public InvenData mData = null;
    int a;
    int b;
	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void OpenInven() //인벤토리를 만듭니다.
    {
        GameObject Inven = (GameObject)Instantiate(inventory);

        Inven.transform.parent
        = GameObject.Find("CC_Offset").transform;

        Inven.transform.localPosition =
            new Vector3(0 , 0 , 0);
        Inven.transform.localScale = Vector3.one;
    }

    public void CloseInven() //인벤토리를 닫습니다.
    {
        Destroy(this.gameObject);
    }

    public void GetItem1()
    {
        mData.AddItem("CannedFood");
    }

    public void GetItem2()
    {
        mData.AddItem("SmokedFood");
    }


    public void LostItem1()
    {
        mData.RemoveItem("CannedFood");
    }

    public void LostItem2()
    {
        mData.RemoveItem("SmokedFood");
    }


}
