using UnityEngine;
using System.Collections;
/*
 * ShowPlayerStatus를 상속받는 스크립트이며 업데이트하며 배고픔 수치를 해당 스크립트에 대입해준다
*/
public class ShowHungerStatus : ShowPlayerStatus
{
    public PlayerStatus mStatus = null;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(UpdateCurVal());

        base.Start();
	}

    IEnumerator UpdateCurVal()
    {
        while(true)
        {
            mCurValue = mStatus.Hunger;

            yield return null;
        }
    }
}
