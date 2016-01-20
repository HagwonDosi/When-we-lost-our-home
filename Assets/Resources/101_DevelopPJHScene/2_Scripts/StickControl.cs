using UnityEngine;
using System.Collections;

public class StickControl : MonoBehaviour
{
    public Camera mainCamera = null;
    public GameObject Parent = null;
    public UISprite StickSprite = null;
    public UISprite Background = null;
    public float backgroundRadius = 0;
    public float speedRate = 1.0f;
    public bool mFreePositionTransparent = false;

    private bool isTouched = false;
    private Vector3 touchVector;
    private Vector3 originalLocal;

    public Vector3 StickVector
    {
        get
        {
            return transform.localPosition * speedRate;
        }
    }

	// Use this for initialization
	void Start ()
    {
        if(mFreePositionTransparent)
        {
            StickSprite.alpha = 0;
            Background.alpha = 0;
        }

        Matrix4x4 mat = transform.worldToLocalMatrix;
        originalLocal = mat * transform.position;
	}
	
    public void isTrigger()
    {
        Debug.Log("touched");

        Vector3 mouseVector = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        touchVector = transform.position - mouseVector;
    }

    public void onSceneTouched(Vector3 MousePos)
    {
        Debug.Log("scene touched");
        //isTouched를 true 대입
        isTouched = true;

        //만약에 자유롭게 움직이는 걸로 했으면 그 위치에 생성
        if(mFreePositionTransparent)
        {
            Parent.transform.position = MousePos;
            StickSprite.alpha = 1;
            Background.alpha = 1;
        }

        //월드 좌표를 로컬 좌표로 바꿈 그리고 originalLocal에 저장
        Matrix4x4 mat = transform.worldToLocalMatrix;
        originalLocal = mat * MousePos;
    }

    public void onSceneReleased()
    {
        //Debug.Log("stick released");

        isTouched = false;
        this.transform.localPosition = Vector3.zero;

        if(mFreePositionTransparent)
        {
            StickSprite.alpha = 0;
            Background.alpha = 0;
        }
    }

    public void isReleased()
    {
        //Debug.Log("released");

        onSceneReleased();
    }

	// Update is called once per frame
	void Update ()
    {
        
	}

    void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //마우스 클릭
            Vector3 mouse = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            //화면 반쪽 반이면 onSceneTouched 호출
            if(mouse.x <= 0.0f)
                onSceneTouched(mouse);
        }
        if (Input.GetMouseButtonUp(0))
        {
            onSceneReleased();
        }

        //isTouched가 true라면
        if (isTouched)
        {
            //변수 선언
            bool isChecked = false;
            Vector3 changedPos = touchVector + mainCamera.ScreenToWorldPoint(Input.mousePosition);  // 바뀐 좌표
            Matrix4x4 mat = transform.worldToLocalMatrix;
            Vector3 local = mat * changedPos;
            //Debug.Log(local);
            //원래 마우스 터치한 지접에서 얼마나 멀어졌는가
            float x = originalLocal.x - local.x;
            float y = originalLocal.y - local.y;
            if (!mFreePositionTransparent)
            {
                x = Background.transform.localPosition.x - local.x;
                y = Background.transform.localPosition.y - local.y;
            }

            float dis = Mathf.Sqrt((x * x) + (y * y));
            //Debug.Log("local " + local);

            //Debug.Log("dis " + dis);

            if (dis > backgroundRadius)
            {
                isChecked = true;
            }

            if (!isChecked)
                transform.position = changedPos;
            else
            {
                transform.localPosition = Vector3.Normalize(local - transform.parent.localPosition) * backgroundRadius;
            }
        }
    }
}
