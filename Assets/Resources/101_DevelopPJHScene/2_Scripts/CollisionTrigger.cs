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

	// Use this for initialization
	void Start ()
    {
	    foreach (var iter in mCollisionDatas)
        {
            mEnterTriggers.Add(iter.mCollisionTag, iter.mEnterTrigger);
        }
	}
	
    void OnTriggerEnter(Collider fOther)
    {
        if(mEnterTriggers.ContainsKey(fOther.tag))
        {
            UITrigger getTrigger = null;

            mEnterTriggers.TryGetValue(fOther.tag, out getTrigger);

            getTrigger.Act();
        }
    }
}
