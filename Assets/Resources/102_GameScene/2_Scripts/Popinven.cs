using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Popinven : MonoBehaviour
{
    public GameObject inventory = null;     //인벤토리 프리팹 화면에 띄우기

    private List<string> Item_Info = new List<string>(); // 아이템 리스트

    public InvenData mData = null;  // InvenData가져오는 함수

    int x; // 이미지의 x값
    int y; // 이미지의 y값

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenInven() //인벤토리를 만듭니다.
    {
        GameObject Inven = (GameObject)Instantiate(inventory);

        Inven.transform.parent
        = GameObject.Find("CC_Offset").transform;

        Inven.transform.localPosition =
            new Vector3(0, 0, 0);
        Inven.transform.localScale = Vector3.one;
    }

    public void MakeItem()
    {
        x = 0;
        y = 0;

        foreach (var iter in Item_Info)
        {
            if (x >= 3)
            {
                y += 1;
                x = 0;
            }

            if(iter == "SmokedFood")
            {
                 
            }
        }
    }

    public void CloseInven() //인벤토리를 닫습니다.
    {   
        Destroy(this.gameObject);
    }
}