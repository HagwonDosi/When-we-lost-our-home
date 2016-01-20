using UnityEngine;
using System.Collections;

public class StickControl : MonoBehaviour
{
    #region Variables
    public Camera mMainCamera = null;
    public GameObject mParent = null;
    public UISprite mStickSprite = null;
    public UISprite mBackground = null;
    public float mBackgroundRadius = 0;
    public float mSpeedRate = 1.0f;
    public bool mFreePositionTransparent = false;

    private bool mTouched = false;
    private Vector3 mTouchVector;
    private Matrix4x4 mOriMat;
    /// <summary>
    /// 원래 터치할 때 찍은 지점
    /// </summary>
    private Vector3 mOriginalLocal;
    #endregion

    public Vector3 StickVector
    {
        get
        {
            return transform.localPosition * mSpeedRate;
        }
    }

	// Use this for initialization
	void Start ()
    {
        if(mFreePositionTransparent)
        {
            mStickSprite.alpha = 0;
            mBackground.alpha = 0;
        }

        Matrix4x4 mat = transform.worldToLocalMatrix;
        mOriginalLocal = mat * transform.position;
	}

    #region CustomFunctions
    /*public void isTrigger()
    {
        Debug.Log("touched");

        Vector3 mouseVector = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        touchVector = transform.position - mouseVector;
    }*/

    public void onSceneTouched(Vector3 MousePos)
    {
        Debug.Log("scene touched");
        //isTouched를 true 대입
        mTouched = true;

        //만약에 자유롭게 움직이는 걸로 했으면 그 위치에 생성
        if(mFreePositionTransparent)
        {
            mParent.transform.position = MousePos;
            mStickSprite.alpha = 1;
            mBackground.alpha = 1;
        }

        //월드 좌표를 로컬 좌표로 바꿈 그리고 originalLocal에 저장
        mOriMat = transform.worldToLocalMatrix;
        mOriginalLocal = transform.localPosition;
        Debug.Log("originalLocal " + mOriginalLocal);
    }

    public void onSceneReleased()
    {
        //Debug.Log("stick released");

        mTouched = false;
        this.transform.localPosition = Vector3.zero;

        if(mFreePositionTransparent)
        {
            mStickSprite.alpha = 0;
            mBackground.alpha = 0;
        }
    }

    public void isReleased()
    {
        //Debug.Log("released");

        onSceneReleased();
    }

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //마우스 클릭
            Vector3 mouse = mMainCamera.ScreenToWorldPoint(Input.mousePosition);

            //화면 반쪽 반이면 onSceneTouched 호출
            if(mouse.x <= 0.0f)
                onSceneTouched(mouse);
        }
        if (Input.GetMouseButtonUp(0))
        {
            onSceneReleased();
        }

        //isTouched가 true라면
        if (mTouched)
        {
            //변수 선언
            bool isChecked = false;
            Matrix4x4 mat = transform.worldToLocalMatrix;
            ///현재 마우스 좌표
            Vector3 changedPos =  mOriMat.MultiplyPoint(mMainCamera.ScreenToWorldPoint(Input.mousePosition));
            Vector3 local = mat * changedPos;
            //Debug.Log("changedPos " + changedPos);
            //원래 마우스 터치한 지점에서 얼마나 멀어졌는가
            float x = changedPos.x;
            float y = changedPos.y;
            if (mFreePositionTransparent)
            {
                x = mBackground.transform.localPosition.x - local.x;
                y = mBackground.transform.localPosition.y - local.y;
            }

            float dis = Vector3.Distance(mOriginalLocal, changedPos);
            //Debug.Log("local " + local);

            //Debug.Log("dis " + dis);
            //Debug.Log("backgroundRad" + mBackgroundRadius);

            if (dis > mBackgroundRadius)
            {
                isChecked = true;
            }

            if (!isChecked)
            {
                transform.localPosition = changedPos;
            }
            else
            {
                transform.localPosition = Vector3.Normalize(changedPos) * mBackgroundRadius;
                Debug.Log("position " + transform.position);
            }
        }
    }
    #endregion
}
