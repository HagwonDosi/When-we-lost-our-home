using UnityEngine;
using System.Collections;

/// <summary>
/// 플레이어의 배고픔을 표현할 이벤트를 말풍선으로 보내는 스크립트
/// </summary>
public class ShowHungerStatus : ShowPlayerStatus
{
    public PlayerStatus mStatus = null;

	// Use this for initialization
	new void Start ()
    {
        StartCoroutine(UpdateCurVal());
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
