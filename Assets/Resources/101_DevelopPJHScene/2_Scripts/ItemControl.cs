using UnityEngine;
using System.Collections;

public class ItemControl : MonoBehaviour
{
    #region Variables
    [SerializeField]
    /// <summary>
    /// 아이템 정보
    /// </summary>
    private string mItemInfo = "";
    [SerializeField]
    /// <summary>
    /// 계속해서 얻을 수 있는가
    /// </summary>
    private bool mInfinite = false;
    [SerializeField]
    private GameObject mInteractionCon = null;
    #endregion

    #region Capsules
    public string ItemInfo
    {
        get
        {
            return mItemInfo;
        }
    }

    public bool IsInfinite
    {
        get
        {
            return mInfinite;
        }
    }

    public GameObject Interaction
    {
        get
        {
            return mInteractionCon;
        }
    }
    #endregion
}
