using UnityEngine;
using System.Collections;

/// <summary>
/// 빛의 공전을 구현
/// </summary>
public class LightRevolution : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Light mLight = null;
    private GameTimer mTimer = null;
    [SerializeField]
    /// <summary>
    /// 일출 시의 빛 색
    /// </summary>
    private Color mBaseColor = new Color(75, 75, 75);
    [SerializeField]
    /// <summary>
    /// 일몰 이후의 빛 색
    /// </summary>
    private Color mTargetColor;
    #endregion

    #region VirtualFunctions
    // Use this for initialization
    void Start ()
    {
        mTimer = GameTimer.Instance;
        StartCoroutine(CheckTimerAndRevolve());
	}
    #endregion

    #region CustomFunctions
    /// <summary>
    /// 시간을 확인하고 시간에 따라서 빛을 공전시킨다.
    /// </summary>
    private IEnumerator CheckTimerAndRevolve()
    {
        //이전에 체크한 시간
        float befHour = mTimer.Hour;
        //오늘 초기화 했는지
        bool initialized = false;
        int i = 0;

        while(true)
        {
            if((int)mTimer.Hour == 6f && !initialized)
            {
                initialized = true;
                befHour = mTimer.Hour;
                MorningCame();
            }
            if(initialized && (int)mTimer.Hour >= 6f && (int)mTimer.Hour <= 18f)
            {
                float dif = mTimer.Hour - befHour;

                if(dif >= 0.032f)
                {
                    //Debug.Log("TimeDif " + (mTimer.Hour - befHour));
                    befHour = mTimer.Hour;
                    ChangeOneTime(dif / 0.032f);
                }
            }
            if((int)mTimer.Hour > 18f)
            {
                mLight.enabled = false;
                initialized = false;
            }
            

            yield return null;
        }
    }

    /// <summary>
    /// 한 번 변화(게임내 2분)
    /// </summary>
    private void ChangeOneTime(float rate)
    {
        Vector3 eul = mLight.transform.localEulerAngles;

        eul.x = 20.5f;
        eul.y += 0.5f * rate;

        mLight.transform.localEulerAngles = eul;

        Color curCol = mLight.color;

        float val = 0.0008f * rate;

        if (mTimer.Hour <= 12f)
        {
            curCol.r += val;
            curCol.b += val;
            curCol.g += val;
        }
        else
        {
            curCol.r -= val;
            curCol.b -= val;
            curCol.g -= val;
        }

        mLight.color = curCol;
    }

    /// <summary>
    /// 아침이 왔을 때 한 번 초기화
    /// </summary>
    private void MorningCame()
    {
        mLight.enabled = true;
        mLight.color = mBaseColor;

        mLight.transform.localEulerAngles = new Vector3(20.5f, -90f);
    }
    #endregion
}
