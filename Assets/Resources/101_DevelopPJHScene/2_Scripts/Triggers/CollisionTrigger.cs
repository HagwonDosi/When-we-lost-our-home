using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct CollisionData
{
    public string mCollisionTag;
    public UITrigger mEnterTrigger;
    public UITrigger mStayTrigger;
    public UITrigger mExitTrigger;
}

public class CollisionTrigger : MonoBehaviour
{
    public List<CollisionData> mCollisionDatas = new List<CollisionData>();

    private Dictionary<string, UITrigger> mEnterTriggers = new Dictionary<string, UITrigger>();
    private Dictionary<string, UITrigger> mExitTriggers = new Dictionary<string, UITrigger>();
    private Dictionary<string, UITrigger> mStayTriggers = new Dictionary<string, UITrigger>();
    private GameObject mColliderObj = null; // 충돌한 Object

    public GameObject ColliderObject
    {
        get
        {
            return mColliderObj;
        }
    }

	// Use this for initialization
	void Start ()
    {
	    foreach (var iter in mCollisionDatas)
        {
            if(iter.mEnterTrigger != null)
                mEnterTriggers.Add(iter.mCollisionTag, iter.mEnterTrigger);
            if(iter.mExitTrigger != null)
                mExitTriggers.Add(iter.mCollisionTag, iter.mExitTrigger);
        }
	}
	
    void OnTriggerEnter(Collider fOther)
    {
        // default 태그의 트리거가 존재한다면
        if(mEnterTriggers.ContainsKey("default"))
        {
            UITrigger getTrigger = null;

            mEnterTriggers.TryGetValue("default", out getTrigger);

            mColliderObj = fOther.gameObject;
            getTrigger.Act();
        }

        if(mEnterTriggers.ContainsKey(fOther.tag))
        {
            UITrigger getTrigger = null;

            mEnterTriggers.TryGetValue(fOther.tag, out getTrigger);

            mColliderObj = fOther.gameObject;
            getTrigger.Act();
        }
    }

    void OnTriggerExit(Collider fOther)
    {
        Debug.Log(gameObject.name + " Trigger Exit With " + fOther.name);

        if (mExitTriggers.ContainsKey("default"))
        {
            UITrigger getTrigger = null;

            mExitTriggers.TryGetValue("default", out getTrigger);

            mColliderObj = null;
            getTrigger.Act();
        }

        if (mExitTriggers.ContainsKey(fOther.tag))
        {
            UITrigger getTrigger = null;

            mExitTriggers.TryGetValue(fOther.tag, out getTrigger);

            mColliderObj = null;
            getTrigger.Act();
        }
    }

    void OnTriggerStay(Collider fOther)
    {
        if (mStayTriggers.ContainsKey("default"))
        {
            UITrigger getTrigger = null;

            mExitTriggers.TryGetValue("default", out getTrigger);

            mColliderObj = fOther.gameObject;
            getTrigger.Act();
        }

        if (mStayTriggers.ContainsKey(fOther.tag))
        {
            UITrigger getTrigger = null;

            mExitTriggers.TryGetValue(fOther.tag, out getTrigger);

            mColliderObj = fOther.gameObject;
            getTrigger.Act();
        }
    }
}
