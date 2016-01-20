using UnityEngine;
using System.Collections;

/// <summary>
/// 플레이어의 손이 Item태그랑 접촉했을 때 실행할 스크립트
/// </summary>
public class ItemTrigger : UITrigger
{
    private CollisionTrigger mCTrigger = null;
    private InvenData mPlayerInven = null;

    void Start()
    {
        mCTrigger = GetComponent<CollisionTrigger>();
        mPlayerInven = GameDirector.Instance.PlayerInven;
    }

    public override void Act()
    {
        ItemControl iCon = mCTrigger.ColliderObject.GetComponent<ItemControl>();

        mPlayerInven.AddItem(iCon.ItemInfo);

        if(!iCon.IsInfinite)
        {
            Destroy(iCon.gameObject);
        }
    }
}
