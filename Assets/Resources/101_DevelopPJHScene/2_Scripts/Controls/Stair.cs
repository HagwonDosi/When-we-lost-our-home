using UnityEngine;
using System.Collections;

/// <summary>
/// 계단에 붙이는 기본 스크립트. 계단의 사용을 관장한다.
/// </summary>
public class Stair : MonoBehaviour
{
    #region Variables
    public Transform mDown = null;
    public Transform mUp = null;

    [SerializeField]
    private float mSpeed = 0.03f;
    private float mOriginalZ = 0f;
    private float mHeight = 0f;
    private bool mPlayer = false;
    private PlayerControl mPCon = null;
    private float mOriDeg;
    #endregion

    #region VirtualFunctions
    void Start()
    {
        mSpeed = GameDirector.Instance.mEventSpeed;
    }
    #endregion

    #region CustomFunctions

    /// <summary>
    /// 어떤 Object가 계단을 사용한다.
    /// </summary>
    /// <param name="fObj">계단을 사용할 오브젝트: TweenRotation, CapsuleCollider을 갖고 있을 것</param>
    public void UseStair(GameObject fObj)
    {
        Debug.Log("pre use stair");
        SpeechBubbleDirector.Instance.mSpeechBubbleShow = false;

        if(fObj.tag.Equals("Player"))
        {
            mPCon = fObj.GetComponent<PlayerControl>();

            mPCon.mCheckAni = false;
            mPCon.mAnimator.SetBool("Player_Run", true);
            mPCon.rigidbody.useGravity = false;
        }

        TweenRotation tRot = fObj.GetComponent<TweenRotation>();
        tRot.enabled = true;
        tRot.from = fObj.transform.localEulerAngles;

        float target = 0f;
        if (Mathf.Round(fObj.transform.localEulerAngles.y) == 90f)
        {
            mOriDeg = 90f;
            target = 0f;
        }
        else if (Mathf.Round(fObj.transform.localEulerAngles.y) == 270)
        {
            mOriDeg = 270f;
            target = 360f;
        }
        
        tRot.to = new Vector3(fObj.transform.localEulerAngles.x, target, fObj.transform.localEulerAngles.z);
        tRot.duration = 0.3f;
        tRot.ResetToBeginning();

        mOriginalZ = fObj.transform.localPosition.z;
        //위와 아래 중에서 어디에 더 가까운지 거리로 확인
        float upYDif = Vector3.Distance(fObj.transform.position, mUp.position);
        float downYDif = Vector3.Distance(fObj.transform.position, mDown.position);

        if(upYDif < downYDif)
        {
            StartCoroutine(ReserveUseStair(false, fObj.transform));
        }
        else
        {
            StartCoroutine(ReserveUseStair(true, fObj.transform));
        }
    }
    
    /// <summary>
    /// 뒤에 계단이 있는 곳으로 나가서 오르락, 내리락하는 스크립트
    /// </summary>
    /// <param name="up">true라면 위로 false라면 아래로</param>
    /// <returns></returns>
    private IEnumerator ReserveUseStair(bool up, Transform fObj)
    {
        yield return new WaitForSeconds(0.3f);

        float targetZ = mUp.transform.position.z;
        while(fObj.transform.position.z <= targetZ)
        {
            Vector3 curPos = fObj.position;
            curPos.z += mSpeed * Time.deltaTime * 62.5f;
            fObj.position = curPos;

            yield return null;
        }

        Vector3 oriPos = Vector3.zero;
        Vector3 targetPos = Vector3.zero;

        if (up)
        {
            Vector3 dif = fObj.position - mDown.position;
            Debug.Log("dif " + dif);

            oriPos = mDown.position + dif;
            targetPos = mUp.position + dif;
        }
        else
        {
            Vector3 dif = fObj.position - mUp.position;
            Debug.Log("dif " + dif);

            oriPos = mUp.position + dif;
            targetPos = mDown.position + dif;
        }

        TweenRotation tRot = fObj.GetComponent<TweenRotation>();
        tRot.enabled = true;
        tRot.from = fObj.transform.localEulerAngles;

        float target = 90f;
        if (oriPos.x >= targetPos.x)
        {
            target = -90f;
        }

        tRot.to = new Vector3(fObj.transform.localEulerAngles.x, target, fObj.transform.localEulerAngles.z);
        tRot.duration = 0.3f;
        tRot.ResetToBeginning();

        yield return new WaitForSeconds(0.3f);


        Debug.Log("use stair");
        SmooothCamera.Instance.enabled = false;

        float sec = 0f;
        float rate = 0f;
        while(true)
        {
            sec += Time.deltaTime;
            rate = sec / 2f;
            fObj.position = Vector3.Lerp(oriPos, targetPos, rate);
            
            if(rate >= 1f)
            {
                fObj.position = Vector3.Lerp(oriPos, targetPos, 1f);
                break;
            }
            yield return null;
        }

        float oriSpeed = SmooothCamera.Instance.mSpeed;
        SmooothCamera.Instance.enabled = true;
        SmooothCamera.Instance.mSpeed = 4f;
        
        tRot.enabled = true;
        tRot.from = fObj.transform.localEulerAngles;

        target = 180f;
        if (oriPos.x >= targetPos.x)
        {
            target = 180f;
        }

        tRot.to = new Vector3(fObj.transform.localEulerAngles.x, target, fObj.transform.localEulerAngles.z);
        tRot.duration = 0.3f;
        tRot.ResetToBeginning();

        yield return new WaitForSeconds(0.3f);

        while (fObj.transform.position.z >= mOriginalZ)
        {
            Vector3 curPos = fObj.position;
            curPos.z -= mSpeed * Time.deltaTime * 62.5f;
            fObj.position = curPos;

            yield return null;
        }

        SmooothCamera.Instance.mSpeed = oriSpeed;
        SpeechBubbleDirector.Instance.mSpeechBubbleShow = true;
        mPCon.rigidbody.useGravity = true;
        mPCon.mCheckAni = true;
        mPCon.mAnimator.SetBool("Player_Run", false);
        
        tRot.enabled = true;
        tRot.from = fObj.transform.localEulerAngles;
        tRot.to = new Vector3(fObj.transform.localEulerAngles.x, mOriDeg, fObj.transform.localEulerAngles.z);
        tRot.duration = 0.3f;
        tRot.ResetToBeginning();
    }

    #endregion
}
