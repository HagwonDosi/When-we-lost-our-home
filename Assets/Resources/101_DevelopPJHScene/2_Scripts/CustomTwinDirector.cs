using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomTwinDirector : MonoBehaviour
{
    #region Variables
    private static CustomTwinDirector mInstance = null;
    private List<TweenValue> mTwins = new List<TweenValue>();
    #endregion

    public static CustomTwinDirector Instance
    {
        get
        {
            if(mInstance == null)
            {
                mInstance = FindObjectOfType<CustomTwinDirector>();
            }
            if(mInstance == null)
            {
                mInstance = new GameObject("CustomTwinDirector").AddComponent<CustomTwinDirector>();
            }

            return mInstance;
        }
    }

    #region VirtualFunctions
	
	// Update is called once per frame
	void Update ()
    {
        UpdateTwins();
	}
    #endregion

    #region CustomFunctions
    private void UpdateTwins()
    {
        foreach(var iter in mTwins.ToArray())
        {
            iter.UpdateVal();
        }
    }

    public void AddTwinValue(TweenValue val)
    {
        mTwins.Add(val);
    }

    public bool RemoveVal(TweenValue val)
    {
        return mTwins.Remove(val);
    }
    #endregion
}
