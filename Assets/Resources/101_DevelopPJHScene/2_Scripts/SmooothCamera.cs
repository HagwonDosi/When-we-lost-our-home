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

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(UpdateCo());
	}
	
	IEnumerator UpdateCo()
    {
        while(true)
        {
            Vector3 pos = Vector3.Lerp(transform.position, mObject.position, Time.deltaTime * mSpeed);

            pos += mOffset;

            transform.position = new Vector3(pos.x, pos.y, transform.position.z);

            yield return null;
        }
    }
}
