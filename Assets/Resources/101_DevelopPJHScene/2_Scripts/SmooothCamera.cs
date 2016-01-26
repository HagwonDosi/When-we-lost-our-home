using UnityEngine;
using System.Collections;

/// <summary>
/// 카메라가 mObjec를 따라가게 하는 스크립트
/// </summary>
public class SmooothCamera : Singletone<SmooothCamera>
{
    #region Variables
    public Transform mObject = null;
    public float mSpeed = 10f;
    public Vector3 mOffset = Vector3.zero;
    /// <summary>
    /// Z좌표로 따라갈 것인지
    /// </summary>
    public bool mZTrack = true;
    public bool mUpdate = true;

    private Camera mCamera = null;
    private Camera mUICamera = null;
    private float mFieldOfView = 0f;
    private Vector3 mOriOffset = Vector3.zero;
    private Vector3 mOriEular = Vector3.zero;
    #endregion

    #region Capsules
    public Camera Camera3D
    {
        get
        {
            return mCamera;
        }
    }
    public Camera CameraUI
    {
        get
        {
            return mUICamera;
        }
    }
    public Vector3 OriOffset
    {
        get
        {
            return mOriOffset;
        }
    }
    public Vector3 OriEularAngles
    {
        get
        {
            return mOriEular;
        }
    }

    #endregion

    #region VirtualFunction
    // Use this for initialization
    void Start()
    {
        mUICamera = FindObjectOfType<UICamera>().camera;
        mCamera = GetComponent<Camera>();
        mFieldOfView = mCamera.fieldOfView;
        GoImmediately();
    }

    void Update()
    {
        if(mUpdate)
        {
            MoveCamera();
            SyncFieldOfView();
        }
    }
    #endregion

    #region CustomFunction
    private void SyncFieldOfView()
    {
        mCamera.fieldOfView = mFieldOfView;
    }

    public void GoImmediately()
    {
        transform.position = mOffset + mObject.position;
    }

    private void MoveCamera()
    {
        Vector3 pos = Vector3.Lerp(transform.position, mObject.position + mOffset, Time.deltaTime * mSpeed);

        if (!mZTrack)
            pos.z = transform.position.z;

        transform.position = pos;
    }

    public void InBuilding()
    {
        mOffset = new Vector3(0, 0.8f, -1.4f);
        mCamera.fieldOfView = 66f;
    }

    public void InBuildingSmooth()
    {
        StartCoroutine(InBuildingSmoothCor());
    }

    private IEnumerator InBuildingSmoothCor()
    {
        float sec = 0f;
        while(true)
        {
            sec += Time.deltaTime;
            float rate = sec / 2f;
            mOffset = new Vector3(0, 0.8f, CustomMath.TweenValue(-2.26f, -1.4f, rate));
            mFieldOfView = CustomMath.TweenValue(52f, 66f, rate);

            Debug.Log("filedOfView " + mCamera.fieldOfView);
            if(rate >= 1f)
            {
                StopAllCoroutines();
                break;
            }
            yield return null;
        }
    }

    public void InBuildingCamera()
    {
        mCamera.cullingMask = (1 << LayerMask.NameToLayer("Default"));
    }

    public void InBuildingCameraWall()
    {
        mCamera.cullingMask = (1 << LayerMask.NameToLayer("Default") | (1 << LayerMask.NameToLayer("ExternBuilding")));
    }

    public void OutBuilding()
    {
        mOffset = new Vector3(0, 0.8f, -2.26f);
        mFieldOfView = 52f;
    }

    public void OutBuildingSmooth()
    {
        StartCoroutine(CorOutBuildingSmooth());
    }

    private IEnumerator CorOutBuildingSmooth()
    {
        float sec = 0f;
        while (true)
        {
            sec += Time.deltaTime;
            float rate = sec / 0.3f;
            mOffset = new Vector3(0, 0.8f, CustomMath.TweenValue(-1.4f, -2.25f, rate));
            mFieldOfView = CustomMath.TweenValue(66, 52, rate);

            Debug.Log("Offset " + mOffset);
            if (rate >= 1f)
            {
                StopCoroutine(CorOutBuildingSmooth());
                break;
            }
            yield return null;
        }
    }

    public void OutBuildingCamera()
    {
        mCamera.cullingMask = (1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("ExternBuilding") | (1 << LayerMask.NameToLayer("ExternBuildingWall")));
    }

    public void CircularMovementTo(float fDeg, float fAngularSpeed, Transform fTra)
    {
        mUpdate = false;
        StartCoroutine(CorCircularMovementTo(fDeg, fAngularSpeed, fTra));
    }

    private IEnumerator CorCircularMovementTo(float fDeg, float fAngularSpeed, Transform fTra)
    {
        Vector2 oriPos = new Vector2(fTra.position.x, fTra.position.z);
        float xDif = transform.position.x - fTra.position.x;
        float zDif = transform.position.z - fTra.position.z;
        float rad = Mathf.Sqrt(xDif * xDif + zDif * zDif);
        Vector3 oriEular = transform.eulerAngles;
        Debug.Log("oriEular " + oriEular);

        float curDeg = Mathf.Atan2(zDif, xDif) * Mathf.Rad2Deg;

        if (curDeg < 0)
            curDeg += 360;
        float oriDeg = curDeg;

        while (true)
        {
            curDeg += fAngularSpeed;
            float x = Mathf.Cos(curDeg * Mathf.Deg2Rad) * rad;
            float y = Mathf.Sin(curDeg * Mathf.Deg2Rad) * rad;
            Vector2 circlePos = new Vector2(fTra.position.x, fTra.position.z) + new Vector2(x, y);
            transform.position = new Vector3(circlePos.x, transform.position.y, circlePos.y);
            Vector3 eul = oriEular;
            eul.y -= curDeg - oriDeg;
            transform.eulerAngles = eul;

            if (curDeg >= oriDeg + fDeg)
            {
                curDeg = oriDeg + fDeg;
                Debug.Log("end curDeg " + curDeg);

                x = Mathf.Cos(curDeg * Mathf.Deg2Rad) * rad;
                y = Mathf.Sin(curDeg * Mathf.Deg2Rad) * rad;
                circlePos = new Vector2(fTra.position.x, fTra.position.z) + new Vector2(x, y);
                transform.position = new Vector3(circlePos.x, transform.position.y, circlePos.y);
                eul = oriEular;
                eul.y -= curDeg - oriDeg;
                transform.eulerAngles = eul;

                break;
            }

            yield return null;
        }

        mOriOffset = mOffset;
        mOffset = transform.position - fTra.transform.position;
        mOriEular = oriEular;
        mUpdate = true;
    }
    #endregion
}
