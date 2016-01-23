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

    private Camera mCamera = null;
    private Camera mUICamera = null;
    private float mFieldOfView = 0f;
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
        MoveCamera();
        SyncFieldOfView();
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
        mCamera.cullingMask = (1 << LayerMask.NameToLayer("Default"));
        StartCoroutine(InBuildingSmoothCor());
    }

    private IEnumerator InBuildingSmoothCor()
    {
        float sec = 0f;
        while(true)
        {
            sec += Time.deltaTime;
            float rate = sec / 1f;
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

    public void OutBuilding()
    {
        mCamera.cullingMask = (1 << LayerMask.NameToLayer("Default")) | (1 << LayerMask.NameToLayer("ExternBuilding"));
        mOffset = new Vector3(0, 0.8f, -2.26f);
        mFieldOfView = 52f;
    }
    #endregion
}
