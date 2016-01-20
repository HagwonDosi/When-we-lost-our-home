using UnityEngine;
using System.Collections;

public class UITrigger : MonoBehaviour
{
    protected float mTime = 0f;

	// Use this for initialization
	void Start ()
    {
        mTime = Time.time;
	}

    public virtual void Act() { }

    protected bool CheckTime()
    {
        if (Time.time - mTime > 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void setTime()
    {
        mTime = Time.time;
    }
}
