using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ColliderListClass
{
    public List<Collider> mColliderList = new List<Collider>();
}

public class UIDirector : MonoBehaviour
{
    #region Variables
    public List<ColliderListClass> mUILayers = new List<ColliderListClass>();

    private static UIDirector mInstance = null;
    private UIAnchor[] mAnchors = new UIAnchor[9];
    #endregion

    #region Capsule
    public static UIDirector Instance
    {
        get
        {
            if(mInstance == null)
            {
                mInstance = FindObjectOfType<UIDirector>();
            }

            if(mInstance == null)
            {
                mInstance = new GameObject("UIDirector").AddComponent<UIDirector>();
            }

            return mInstance;
        }
    }

    #endregion

    #region VirtualFunctions
    void Start()
    {
        var ancs = FindObjectsOfType<UIAnchor>();
        for (int i = 0; i < ancs.Length; i++)
        {
            mAnchors[(int)ancs[i].side] = ancs[i];
        }
    }

    #endregion

    #region CustomFunctions

    public GameObject GetUIAnchor(UIAnchor.Side fSide)
    {
        return mAnchors[(int)fSide].gameObject;
    }

    /// <summary>
    /// 해당 레벨의 UI의 enabled를 설정
    /// </summary>
    /// <param name="idx">설정할 UI 레벨</param>
    /// <param name="enable">설정할 bool 값</param>
    public void SetEnabledUILayer(int idx, bool enable)
    {
        if(idx >= 0 && idx < mUILayers.Count)
        {
            Debug.Log("Set UI Enable level " + (idx + 1) + " to " + enable);

            foreach(var iter in mUILayers[idx].mColliderList)
            {
                iter.enabled = enable;
            }
        }
    }

    public void AddNewLayer(Collider[] colliders)
    {
        ColliderListClass list = new ColliderListClass();

        for(int i = 0; i < colliders.Length; i++)
        {
            list.mColliderList.Add(colliders[i]);
        }

        mUILayers.Add(list);
    }

    #endregion
}
