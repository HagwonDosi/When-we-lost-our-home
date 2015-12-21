using UnityEngine;
using System.Collections;

public class HPBarControl : BarControl
{
    public PlayerStatus mStatus = null;

	// Use this for initialization
	void Start ()
    {
        mMaxValue = 100f;
        mCurValue = mStatus.HP;

        StartCoroutine(UpdateCurVal());

        base.Start();
	}
	
    IEnumerator UpdateCurVal()
    {
        while(true)
        {
            mCurValue = mStatus.HP;

            yield return null;
        }
    }
}
