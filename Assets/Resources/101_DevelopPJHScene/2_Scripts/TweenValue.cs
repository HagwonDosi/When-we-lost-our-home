using UnityEngine;
using System.Collections;

public class  TweenValue
{
    private float mOriValue;
    private float mTargetValue;
    private float mDuration;
    private float mValue;
    private float mSec = 0f;

    public TweenValue(float ori, float target, float duration, ref float val, MonoBehaviour obj)
    {
        Debug.Log("Start Twin " + ori + " to " + target + " in " + duration);
        mOriValue = ori;
        mTargetValue = target;
        mDuration = duration;
        mValue = val;

        CustomTwinDirector.Instance.AddTwinValue(this);
    }

    public void UpdateVal()
    {
        mSec += Time.deltaTime;
        float rate = (mSec / mDuration);
        Debug.Log("update val" + mValue + " rate " + rate);
        float val = (1f - rate) * mOriValue + rate * mTargetValue;
        mValue = val;

        if (Mathf.Round(mValue) == Mathf.Round(mTargetValue))
        {
            CustomTwinDirector.Instance.RemoveVal(this);
        }
    }
}
