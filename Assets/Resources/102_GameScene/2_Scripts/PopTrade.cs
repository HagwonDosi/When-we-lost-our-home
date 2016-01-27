using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopTrade : MonoBehaviour
{
    public GameObject inventory = null;     //인벤토리 프리팹 화면에 띄우기

    private List<string> Item_Info_P = new List<string>(); // Player 아이템 리스트
    private List<string> Item_Info_N = new List<string>(); // NPC 아이템 리스트

    public InvenData mData_P = null;  // Player InvenData가져오는 함수
    public InvenData mData_N = null;  // NPC InvenData가져오는 함수

    public UISprite[] Item_image_P = null; // 인벤토리 이미지
    public UISprite[] Item_image_N = null; // 인벤토리 이미지

    int chek_P = 0; // 아래 Player MakeItem이 몇번 돌아갔는지 체크
    int chek_N = 0; // 아래 NPC MakeItem이 몇번 돌아갔는지 체크

    int Bage_Size = 0;

    // Use this for initialization
    void Start()
    {

    }   

    private IEnumerator SyncListItem()
    {
        while (true)
        {
            MakeItem();

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Item_Chek()
    {
        Item_Info_P  = mData_P.GetList();
        Item_Info_N = mData_N.GetList();
    }

    public void OpenInven() //인벤토리를 만듭니다.
    {
        //UIDirector.Instance.SetEnabledUILayer(0, false);
        Bage_Size = mData_P.BageSize;
        for (int i = 0; i < Bage_Size;)
        {
            Item_image_P[i].spriteName = "Gear";
            i++;
        }

        Item_Chek();
        inventory.transform.localPosition = new Vector3(0, 0, 0);
        StartCoroutine(SyncListItem());
    }

        
    public void MakeItem()  
    {

        foreach (var iter in Item_Info_P)
        {
            if (iter == "SmokedFood")   
            {
                Item_image_P[chek_P].spriteName = "SmokedFood";
                Item_image_P[chek_P].GetComponent<ItemInfo>().Item_Name = iter;
            }
            if (iter == "CannedFood")
            {
                Item_image_P[chek_P].spriteName = "CannedFood";
                Item_image_P[chek_P].GetComponent<ItemInfo>().Item_Name = iter;
            }
            chek_P++;
        }
        chek_P = 0;

        foreach (var iter in Item_Info_N)
        {
            if (iter == "SmokedFood")
            {
                Item_image_N[chek_N].spriteName = "SmokedFood";
                Item_image_N[chek_N].GetComponent<ItemInfo>().Item_Name = iter;
            }
            if (iter == "CannedFood")
            {
                Item_image_N[chek_N].spriteName = "CannedFood";
                Item_image_N[chek_N].GetComponent<ItemInfo>().Item_Name = iter;
            }
            chek_N++;
        }
        chek_N = 0;
    }

    public void GetInfo()
    {

    }

    public void CloseInven() //인벤토리를 닫습니다.
    {
        Destroy(this.gameObject);
    }
}
