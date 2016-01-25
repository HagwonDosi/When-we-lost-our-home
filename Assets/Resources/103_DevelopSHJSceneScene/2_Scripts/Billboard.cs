using UnityEngine;
using System.Collections;

public class Billboard : MonoBehaviour
{
    public Camera mCamera;

    void Update()
    {
        transform.LookAt(transform.position + mCamera.transform.rotation * Vector3.forward,
            mCamera.transform.rotation * Vector3.up);
    }
}
