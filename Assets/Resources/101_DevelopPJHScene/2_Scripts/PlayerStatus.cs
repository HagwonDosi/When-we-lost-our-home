using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour
{
    //플레이어가 위험 지역에 있는가
    public bool mIsPlayerInDanger = false;

    private float mHP = 100f;           // HP
    private float mHunger = 100f;       // 배고픔 수치
    private float mThirsty = 100f;      // 갈증 수치
    private GameTimer mTimer = null;

    public float HP
    {
        get
        {
            return mHP;
        }
    }
    public float Hunger
    {
        get
        {
            return mHunger;
        }
    }
    public float Thirsty
    {
        get
        {
            return mThirsty;
        }
    }

    public void AddHP(float fVal)
    {
        mHP += fVal;

        // 범위를 벗어났나 검사
        if (mHP < 0)
            mHP = 0;
        else if (mHP > 100f)
            mHP = 100f;
    }

	// Use this for initialization
	void Start ()
    {
        mTimer = GameTimer.Instance;

        StartCoroutine(UpdateStatus());
	}

    void OneHourPassed()
    {
        if(mIsPlayerInDanger)
        {
            mHunger -= 2f;
            mThirsty -= 2f;
        }
        else
        {
            mHunger -= 1.4f;
            mThirsty -= 1.4f;
        }
    }

    IEnumerator UpdateStatus()
    {
        int befDay = mTimer.Day;
        float befTime = mTimer.Hour;

        while(true)
        {
            //현재 시각과 이전 시각을 비교해서 1시간보다 크다면
            if(mTimer.getTimeGap(befDay, befTime) >= 1.0f)
            {
                befDay = mTimer.Day;
                befTime = mTimer.Hour;

                OneHourPassed();
            }

            yield return null;
        }
    }
}
