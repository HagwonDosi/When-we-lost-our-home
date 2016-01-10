using UnityEngine;
using System.Collections;

public class FloorControl : MonoBehaviour
{
    #region Variables
    public GameObject[] mWalls = new GameObject[2];
    public GameObject[] mFloors = null;

    #endregion

    #region getter/setter
    public float FloorWidth
    {
        get
        {
            return Mathf.Abs(mWalls[0].transform.localPosition.x - mWalls[1].transform.localPosition.x);
        }
    }

    public float LeftLimit
    {
        get
        {
            return (mWalls[0].transform.localPosition.x < mWalls[1].transform.localPosition.x) ? mWalls[0].transform.localPosition.x : mWalls[1].transform.localPosition.x;
        }
    }

    public float RightLimit
    {
        get
        {
            return (mWalls[0].transform.localPosition.x > mWalls[1].transform.localPosition.x) ? mWalls[0].transform.localPosition.x : mWalls[1].transform.localPosition.x;
        }
    }
    #endregion

    #region VirtualFunction
    // Use this for initialization
    void Start ()
    {
        StartCoroutine(CheckWallMoving());
	}
    #endregion

    #region CustomFunction

    IEnumerator CheckWallMoving()
    {
        while(true)
        {
            float curWidth = Mathf.Abs(mWalls[0].transform.localPosition.x - mWalls[1].transform.localPosition.x);
            float floorWidth = mFloors[0].transform.localScale.x;

            for (int i = 0; i < mFloors.Length; i++)
            {
                float rate = curWidth / floorWidth;

                Vector3 oriScale = mFloors[i].transform.localScale;
                oriScale.x = oriScale.x * rate;
                mFloors[i].transform.localScale = oriScale;

                Vector3 oriPos = mFloors[i].transform.localPosition;
                oriPos.x = (mWalls[0].transform.localPosition.x + mWalls[1].transform.localPosition.x) / 2;
                mFloors[i].transform.localPosition = oriPos;

            }

            yield return null;
        }
    }

    public void setLimit(float left, float right)
    {
        GameObject leftWall = null;
        GameObject rightWall = null;

        if (mWalls[0].transform.localPosition.x < mWalls[1].transform.localPosition.x)
        {
            leftWall = mWalls[0];
            rightWall = mWalls[1];
        }
        else
        {
            leftWall = mWalls[1];
            rightWall = mWalls[0];
        }

        leftWall.transform.localPosition = new Vector3(left, leftWall.transform.localPosition.y, leftWall.transform.localPosition.z);
        rightWall.transform.localPosition = new Vector3(right, rightWall.transform.localPosition.y, rightWall.transform.localPosition.z);
    }

    public bool IsWall(GameObject fObj)
    {
        return (fObj == mWalls[0] || fObj == mWalls[1]);
    }

    #endregion
}
