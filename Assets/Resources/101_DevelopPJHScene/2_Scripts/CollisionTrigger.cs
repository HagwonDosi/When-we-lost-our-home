using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct CollisionData
{
    public string mCollisionTag;
    public UITrigger mEnterTrigger;
    public UITrigger mExitTrigger;
}

public class CollisionTrigger : MonoBehaviour
{
    public List<CollisionData> mCollisionDatas = new List<CollisionData>();

    private Dictionary<string, UITrigger> mEnterTriggers = new Dictionary<string, UITrigger>();
    private Dictionary<string, UITrigger> mExitTriggers = new Dictionary<string, UITrigger>();
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
            mEnterTriggers.Add(iter.mCollisionTag, iter.mEnterTrigger);
            mExitTriggers.Add(iter.mCollisionTag, iter.mExitTrigger);
        }
	}
	
    void OnTriggerEnter(Collider fOther)
    {
        Debug.Log("Trigger Enter " + fOther.tag);
        mColliderObj = fOther.gameObject;

        // default 태그의 트리거가 존재한다면
        if(mEnterTriggers.ContainsKey("default"))
        {
            UITrigger getTrigger = null;

            mEnterTriggers.TryGetValue("default", out getTrigger);

            getTrigger.Act();
        }

        if(mEnterTriggers.ContainsKey(fOther.tag))
        {
            UITrigger getTrigger = null;

            mEnterTriggers.TryGetValue(fOther.tag, out getTrigger);

            getTrigger.Act();
        }
    }

    void OnTriggerExit(Collider fOther)
    {
        mColliderObj = null;
        
        if (mExitTriggers.ContainsKey("default"))
        {
            UITrigger getTrigger = null;

            mExitTriggers.TryGetValue("default", out getTrigger);

            getTrigger.Act();
        }

        if (mExitTriggers.ContainsKey(fOther.tag))
        {
            UITrigger getTrigger = null;

            mExitTriggers.TryGetValue(fOther.tag, out getTrigger);

            getTrigger.Act();
        }
    }
}
