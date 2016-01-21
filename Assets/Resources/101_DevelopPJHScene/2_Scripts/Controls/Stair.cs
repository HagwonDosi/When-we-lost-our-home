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

    private float mOriginalZ = 0f;
    private float mHeight = 0f;
    private bool mPlayer = false;
    private PlayerControl mPCon = null;
    #endregion

    #region VirtualFunctions
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region CustomFunctions

    /// <summary>
    /// 어떤 Object가 계단을 사용한다.
    /// </summary>
    /// <param name="fObj">계단을 사용할 오브젝트: TweenPosition, CapsuleCollider을 갖고 있을 것</param>
    public void UseStair(GameObject fObj)
    {
        Debug.Log("pre use stair");
        TweenPosition tPosition = fObj.GetComponent<TweenPosition>();
        CapsuleCollider collider = fObj.GetComponent<CapsuleCollider>();

        mHeight = (collider.height) * fObj.transform.localScale.y;

        if(fObj.tag.Equals("Player"))
        {
            mPCon = fObj.GetComponent<PlayerControl>();


        }


        mOriginalZ = fObj.transform.localPosition.z;
        //위와 아래 중에서 어디에 더 가까운지 거리로 확인
        float upYDif = Vector3.Distance(fObj.transform.position, mUp.position);
        float downYDif = Vector3.Distance(fObj.transform.position, mDown.position);

        if(upYDif < downYDif)
        {
            tPosition.enabled = true;
            tPosition.duration = 0.5f;
            tPosition.from = fObj.transform.localPosition;
            
            fObj.transform.position = mUp.transform.position;
            tPosition.to = new Vector3(fObj.transform.localPosition.x, fObj.transform.localPosition.y + mHeight / 2, fObj.transform.localPosition.z);
            fObj.transform.localPosition = tPosition.from;
            tPosition.ResetToBeginning();

            StartCoroutine(ReserveUseStair(tPosition, false, fObj.transform));
        }
        else
        {
            tPosition.enabled = true;
            tPosition.duration = 0.5f;
            tPosition.from = fObj.transform.localPosition;

            fObj.transform.position = mDown.transform.position;
            tPosition.to = new Vector3(fObj.transform.localPosition.x, fObj.transform.localPosition.y + mHeight / 2, fObj.transform.localPosition.z);
            fObj.transform.localPosition = tPosition.from;
            tPosition.ResetToBeginning();

            StartCoroutine(ReserveUseStair(tPosition, true, fObj.transform));
        }
    }
    
    /// <summary>
    /// 0.5초 후에 계단을 오르거나 내릴 스크립트
    /// </summary>
    /// <param name="tPosition">사용할 TweenPosition</param>
    /// <param name="up">true라면 위로 false라면 아래로</param>
    /// <returns></returns>
    private IEnumerator ReserveUseStair(TweenPosition tPosition, bool up, Transform fObj)
    {
        yield return new WaitForSeconds(0.5f);

        Debug.Log("use stair");
        tPosition.enabled = true;
        tPosition.duration = 1.0f;
        if(up)
        {
            tPosition.from = tPosition.to;
            fObj.position = mUp.position;
            tPosition.to = new Vector3(fObj.transform.localPosition.x, fObj.transform.localPosition.y + mHeight / 2, fObj.transform.localPosition.z);
            fObj.localPosition = tPosition.from;
        }
        else
        {

            tPosition.from = tPosition.to;
            fObj.position = mDown.position;
            tPosition.to = new Vector3(fObj.transform.localPosition.x, fObj.transform.localPosition.y + mHeight / 2, fObj.transform.localPosition.z);
            fObj.localPosition = tPosition.from;
        }

        tPosition.ResetToBeginning();

        StartCoroutine(ReserveGetBackRoad(tPosition));
    }

    private IEnumerator ReserveGetBackRoad(TweenPosition tPosition)
    {
        yield return new WaitForSeconds(1.0f);

        tPosition.enabled = true;
        tPosition.duration = 0.5f;
        tPosition.from = tPosition.to;
        tPosition.to = new Vector3(tPosition.from.x, tPosition.from.y, mOriginalZ);
        tPosition.ResetToBeginning();
    }

    #endregion
}
