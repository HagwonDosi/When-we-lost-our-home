using UnityEngine;
using System.Collections;

public class HPBarControl : BarControl
{
    public Status mStatus = null;

	// Use this for initialization
	void Start ()
    {
        mCurValue = mStatus.GetStatus("Health");

        StartCoroutine(UpdateCurVal());

        base.Start();
	}
	
    IEnumerator UpdateCurVal()
    {
        while(true)
        {
            mCurValue = mStatus.GetStatus("Health");

            yield return null;
        }
    }
}
