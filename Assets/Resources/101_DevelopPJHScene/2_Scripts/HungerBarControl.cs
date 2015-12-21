using UnityEngine;
using System.Collections;

public class HungerBarControl : BarControl
{
    public PlayerStatus mStatus = null;

	// Use this for initialization
	void Start ()
    {
        mMaxValue = 100f;
        mCurValue = mStatus.Hunger;

        StartCoroutine(UpdateCurVal());

        base.Start();
	}

    IEnumerator UpdateCurVal()
    {
        while (true)
        {
            mCurValue = mStatus.Hunger;

            yield return null;
        }
    }
}
