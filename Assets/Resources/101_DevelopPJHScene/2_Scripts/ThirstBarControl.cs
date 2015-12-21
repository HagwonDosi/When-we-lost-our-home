using UnityEngine;
using System.Collections;

public class ThirstBarControl : BarControl
{
    public PlayerStatus mStatus = null;

	// Use this for initialization
	void Start ()
    {
        mMaxValue = 100f;
        mCurValue = mStatus.Thirsty;

        StartCoroutine(UpdateCurVal());

        base.Start();
	}

    IEnumerator UpdateCurVal()
    {
        while (true)
        {
            mCurValue = mStatus.Thirsty;

            yield return null;
        }
    }
}
