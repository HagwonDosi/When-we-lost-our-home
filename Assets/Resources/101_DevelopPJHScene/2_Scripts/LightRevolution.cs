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
                if(mTimer.Hour - befHour >= 0.016f)
                {
                    befHour = mTimer.Hour;

                    ChangeOneTime();
                }
            }
            if((int)mTimer.Hour > 18f)
            {
                initialized = false;
            }
            

            yield return null;
        }
    }

    /// <summary>
    /// 한 번 변화(게임내 1분)
    /// </summary>
    private void ChangeOneTime()
    {
        Vector3 eul = mLight.transform.localEulerAngles;

        eul.x = 20.5f;
        eul.y += 0.25f;

        mLight.transform.localEulerAngles = eul;

        Color curCol = mLight.color;

        curCol.r -= 0.1f;
        curCol.b -= 0.1f;
        curCol.g -= 0.1f;

        mLight.color = curCol;
    }

    /// <summary>
    /// 아침이 왔을 때 한 번 초기화
    /// </summary>
    private void MorningCame()
    {
        Debug.Log("oriColor " + mLight.color);
        Debug.Log("baseScolor " + mBaseColor);
        mLight.color = mBaseColor;

        mLight.transform.localEulerAngles = new Vector3(20.5f, -90f);
    }
    #endregion
}
