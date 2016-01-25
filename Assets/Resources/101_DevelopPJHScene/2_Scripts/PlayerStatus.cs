using UnityEngine;
using System.Collections;

public class PlayerStatus : Status
{
    //플레이어가 위험 지역에 있는가
    public bool mIsPlayerInDanger = false;
    
    private GameTimer mTimer = null;

    public float HP
    {
        get
        {
            return base.GetStatus("Health");
        }
    }
    public float Hunger
    {
        get
        {
            return base.GetStatus("Hunger");
        }
    }
    public float Thirsty
    {
        get
        {
            return base.GetStatus("Thirst");
        }
    }

	// Use this for initialization
	void Start ()
    {
        AddStatus("Health", new StatMinMax(100f, 0f, 100f));
        AddStatus("Hunger", new StatMinMax(100f, 0f, 100f));
        AddStatus("Thirst", new StatMinMax(100f, 0f, 100f));
        mTimer = GameTimer.Instance;

        StartCoroutine(UpdateStatus());
    }

    public void AddHP(float fVal)
    {
        SetStatus("Health", GetStatus("Health") + fVal);

        // 범위를 벗어났나 검사
        if (GetStatus("Health") < 0)
            SetStatus("Health", 0);
        else if (GetStatus("Health") > 100f)
            SetStatus("Health", 100f);
    }

    void OneHourPassed()
    {
        if(mIsPlayerInDanger)
        {
            SetStatus("Hunger", GetStatus("Hunger") - 2f);
            SetStatus("Thirst", GetStatus("Thirst") - 2f);
        }
        else
        {
            SetStatus("Hunger", GetStatus("Hunger") - 1.4f);
            SetStatus("Thirst", GetStatus("Thirst") - 1.4f);
        }
    }

    IEnumerator UpdateStatus()
    {
        int befDay = mTimer.Day;
        float befTime = mTimer.Hour;

        while(true)
        {
           // if(GetStatus("Health") <= 0)
           // {
           //     Destroy(gameObject);
           // }
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
