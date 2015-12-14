using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Cube
{
    public Vector3 mCenter;
    public Vector3 mSize;

    public static Cube zero
    {
        get
        {
            Cube cube = new Cube();
            cube.mCenter = Vector3.zero;
            cube.mSize = Vector3.zero;

            return cube;
        }
    }
}

public class BoxFollower : MonoBehaviour
{
    public GameObject mFollowObject = null;
    public Cube mFollowCube = Cube.zero;
    public Cube mMaxCube = Cube.zero;

    private Vector3 mRectCenterPos = Vector3.zero;
    private Vector3 mThisOriginalPos = Vector3.zero;
    private Vector3 mChangedPos = Vector3.zero;

	// Use this for initialization
	void Start ()
    {
        if(mFollowObject)
        {
            mRectCenterPos = mFollowObject.transform.localPosition;
            mThisOriginalPos = transform.localPosition;

            StartCoroutine(UpdateFollowing());
        }
        else
        {
            Debug.LogError("BoxFollower 'mFollowObject' is null!");
        }
	}
	
    IEnumerator UpdateFollowing()
    {
        while(true)
        {
            Vector3 distance = mFollowObject.transform.localPosition - mRectCenterPos;
            Vector3 changePos = Vector3.zero;

            if(Mathf.Abs(distance.x) > mFollowCube.mSize.x)
            {
                float dis = Mathf.Abs(distance.x) - mFollowCube.mSize.x;

                if(distance.x < 0)
                {
                    changePos.x = -dis;
                }
                else
                {
                    changePos.x = dis;
                }
            }

            mChangedPos += changePos;
            mRectCenterPos += changePos;

            this.transform.localPosition = mThisOriginalPos + mChangedPos;

            yield return null;
        }
    }
}
