using UnityEngine;
using System.Collections;

/// <summary>
/// 카메라가 mObjec를 따라가게 하는 스크립트
/// </summary>
public class SmooothCamera : MonoBehaviour
{
    public Transform mObject = null;
    public float mSpeed = 10f;
    public Vector3 mOffset = Vector3.zero;
    /// <summary>
    /// Z좌표로 따라갈 것인지
    /// </summary>
    public bool mZTrack = true;

    private Camera mCamera = null;
    private float mFieldOfView = 0f;

    #region VirtualFunction
    // Use this for initialization
    void Start()
    {
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
        new TweenValue(mOffset.z, -1.4f, 0.5f, ref mOffset.z, this);
        new TweenValue(mCamera.fieldOfView, 66f, 0.5f, ref mFieldOfView, this);
    }

    public void OutBuilding()
    {
        mOffset = new Vector3(0, 0.8f, -2.26f);
        mFieldOfView = 52f;
    }
    #endregion
}
