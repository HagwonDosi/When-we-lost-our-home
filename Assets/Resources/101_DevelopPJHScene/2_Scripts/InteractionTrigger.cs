using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct InteractionTriggerInfo
{
    public InteractionType mType;
    public UITrigger mTrigger;
    public string mSpriteName;
}

public class InteractionTrigger : MonoBehaviour
{
    public List<InteractionTriggerInfo> mTriggerInfo;

    private UISprite mSprite = null;
    private UITrigger mTrigger = null;
    private Dictionary<InteractionType, InteractionTriggerInfo> mTriggerInfoDic = new Dictionary<InteractionType, InteractionTriggerInfo>();

	// Use this for initialization
	void Start ()
    {
        mSprite = GetComponent<UISprite>();

        mSprite.alpha = 0;

	    foreach(var iter in mTriggerInfo)
        {
            mTriggerInfoDic.Add(iter.mType, iter);
        }
	}

    public void OnRelease()
    {
        if (mTrigger != null)
            mTrigger.Act();
        else
            Debug.LogWarning(gameObject.name + " InteractionTrigger OnPress " + "mTrigger is null");
    }

    public void InteractionTriggerEnter(InteractionControl fCon)
    {
        InteractionTriggerInfo getInfo;

        mTriggerInfoDic.TryGetValue(fCon.mType, out getInfo);

        mSprite.spriteName = getInfo.mSpriteName;
        mSprite.alpha = 1;

        mTrigger = getInfo.mTrigger;
    }

    public void InteractionTriggerExit()
    {
        mSprite.alpha = 0;
    }
}
