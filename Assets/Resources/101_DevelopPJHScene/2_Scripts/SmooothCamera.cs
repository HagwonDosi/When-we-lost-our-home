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

    #region VirtualFunction
    // Use this for initialization
    void Start()
    {
        GoImmediately();
    }

    void Update()
    {
        MoveCamera();
    }
    #endregion

    #region CustomFunction
    public void GoImmediately()
    {
        transform.position = mOffset + new Vector3(mObject.transform.position.x, mObject.transform.position.y, mObject.transform.position.z - 1.1f);
    }

    private void MoveCamera()
    {
        Vector3 pos = Vector3.Lerp(transform.position, mObject.position + mOffset, Time.deltaTime * mSpeed);

        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }
    #endregion
}
