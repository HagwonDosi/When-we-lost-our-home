using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    #region Variables
    [SerializeField]
    /// <summary>
    /// 시간계에서 하루가 현실세계의 몇 초인가
    /// </summary>
    private float mSecPerDay = 240f;
    private static GameTimer mInstance = null;
    private int mDay = 0;
    private float mSec = 0;
    #endregion

    #region Capsule
    public static GameTimer Instance
    {
        get
        {
            if (mInstance == null)
                mInstance = GameObject.FindObjectOfType<GameTimer>();

            return mInstance;
        }
    }

    public int Day
    {
        get
        {
            return mDay;
        }
    }

    public float Hour
    {
        get
        {
            return (mSec / mSecPerDay) * 24f;
        }
    }
    #endregion


    // Use this for initialization
    void Start ()
    {
        StartCoroutine(UpdateTime());
	}
	
    IEnumerator UpdateTime()
    {
        while(true)
        {
            mSec += Time.deltaTime;
            
            if(mSec >= mSecPerDay)
            {
                mSec -= mSecPerDay;
                mDay += 1;
            }

            yield return null;
        }
    }

    //시간의 차이를 시간 단위로 반환하는 함수
    public float getTimeGap(int compDay, float compHour)
    {
        return Mathf.Abs(Mathf.Abs(compDay - mDay) * 24 - Mathf.Abs(Hour - compHour));
    }
}
