using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct StatMinMax
{
    public float mVal;
    public float mMin;
    public float mMax;

    public StatMinMax(float val, float min, float max)
    {
        mVal = val;
        mMax = max;
        mMin = min;
    }
}

public class Status : MonoBehaviour
{
    public UITrigger mDeathTrigger = null;

    private Dictionary<string, StatMinMax> mStatusList = new Dictionary<string, StatMinMax>();

    #region VirtualFunctions

    #endregion

    #region CustomFunctions
    public void Dead()
    {
        if(mDeathTrigger != null)
        {
            Debug.Log(name + " Dead");
            mDeathTrigger.Act();
        }
        else
        {
            Debug.LogWarning(name + ".Status doesn't has DeathTrigger");
        }
    }

    protected void AddStatus(string name, StatMinMax maxVal)
    {
        Debug.Log(name + " added");
        mStatusList.Add(name, maxVal);
        Debug.Log("count " + mStatusList.Count);
    }

    public float GetStatus(string name)
    {
        StatMinMax val;
        if (mStatusList.TryGetValue(name, out val))
        {
            return val.mVal;
        }
        else
        {
            Debug.LogWarning(gameObject.name + "Status.GetStatus() " + "Key " + name + " not found");
            return 0f;
        }
    }

    public void SetStatus(string name, float val)
    {
        StatMinMax getVal;
        if(mStatusList.TryGetValue(name, out getVal))
        {
            Debug.Log(name + " set val " + val);
            getVal.mVal = val;

            if (getVal.mVal < getVal.mMin)
                getVal.mVal = getVal.mMin;
            else if (getVal.mVal > getVal.mMax)
                getVal.mVal = getVal.mMax;

            mStatusList.Remove(name);
            mStatusList.Add(name, getVal);
        }
        else
        {
            Debug.LogWarning(gameObject.name + "Status.SetStatus() " + "Key not found");
        }
    }

    /// <summary>
    /// 특정 스테터스에 덧셈을 한다
    /// </summary>
    /// <param name="name">스테터스의 이름</param>
    /// <param name="val">스테터스에 더할 값</param>
    public void AddValStatus(string name, float val)
    {
        Debug.Log(name + " add val " + val);
        SetStatus(name, GetStatus(name) + val);
    }
    #endregion
}
