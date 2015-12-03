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
        isTouched = true;
        Vector3 oriPos = transform.localPosition;

        if(mFreePositionTransparent)
        {
            Parent.transform.position = MousePos;
            StickSprite.alpha = 1;
            Background.alpha = 1;
        }

        Matrix4x4 mat = transform.worldToLocalMatrix;
        originalLocal = mat * MousePos;

        Background.transform.localPosition -= oriPos - transform.localPosition;
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
            Vector3 mouse = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if(mouse.x <= 0.0f)
                onSceneTouched(mouse);
        }
        if (Input.GetMouseButtonUp(0))
        {
            onSceneReleased();
        }

        if (isTouched)
        {
            bool isChecked = false;
            Vector3 changedPos = touchVector + mainCamera.ScreenToWorldPoint(Input.mousePosition);  // 바뀐 좌표
            Matrix4x4 mat = transform.worldToLocalMatrix;
            Vector3 local = mat * changedPos;
            //Debug.Log(local);
            float x = originalLocal.x - local.x;
            float y = originalLocal.y - local.y;
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
