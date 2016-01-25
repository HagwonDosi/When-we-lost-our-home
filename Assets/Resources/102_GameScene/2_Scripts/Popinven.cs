using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Popinven : MonoBehaviour
{
    public GameObject inventory = null;     //인벤토리 프리팹 화면에 띄우기

    private List<string> Item_Info = new List<string>(); // 아이템 리스트

    public InvenData mData = null;  // InvenData가져오는 함수

    public UISprite[] Item_image = null; // 인벤토리 이미지

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
        Item_Info = mData.GetList();
    }

    public void OpenInven() //인벤토리를 만듭니다.
    {
        UIDirector.Instance.SetEnabledUILayer(0, false);
        Bage_Size = mData.BageSize;
        for (int i = 0; i < Bage_Size;)
        {
            Item_image[i].spriteName = "Gear";
            i++;
        }

        Item_Chek();
        inventory.transform.localPosition = new Vector3(0, 0, 0);
        StartCoroutine(SyncListItem());
    }

    int chek = 0; // 아래 MakeItem이 몇번 돌아갔는지 체크

    public void MakeItem()
    {

        foreach (var iter in Item_Info)
        {
            if (iter == "SmokedFood")
            {
                Item_image[chek].spriteName = "SmokedFood";
                Item_image[chek].GetComponent<ItemInfo>().Item_Name = iter;
            }
            if (iter == "CannedFood")
            {
                Item_image[chek].spriteName = "CannedFood";
                Item_image[chek].GetComponent<ItemInfo>().Item_Name = iter;
            }

            chek++;

        }
        chek = 0;
    }

    public void GetInfo()
    {

    }

    public void CloseInven() //인벤토리를 닫습니다.
    {
        Destroy(this.gameObject);
    }
}
