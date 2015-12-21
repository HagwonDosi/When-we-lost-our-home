using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public StickControl mController;
    public float mMaxSpeed = 0.1f;
    public float mRetardationSpeed = 0.01f;
    public GameObject Gun = null;

    private GameObject gun = null;
    private float mSpeed = 0;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(UpdateSpeed());
	}
	
    IEnumerator UpdateSpeed()
    {
        while(true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                
            }
            if(mController.StickVector.Equals(Vector3.zero))
            {
                if(mSpeed < 0)
                {
                    mSpeed += mRetardationSpeed;

                    if (mSpeed > 0)
                        mSpeed = 0;
                }
                else if(mSpeed > 0)
                {
                    mSpeed -= mRetardationSpeed;

                    if (mSpeed < 0)
                        mSpeed = 0;
                }
            }
            else
            {
                if (mSpeed >= -mMaxSpeed && mSpeed <= mMaxSpeed)
                {
                    mSpeed += mController.StickVector.x;

                    if (mSpeed < -mMaxSpeed)
                        mSpeed = -mMaxSpeed;
                    else if (mSpeed > mMaxSpeed)
                        mSpeed = mMaxSpeed;
                }
            }

            transform.localPosition = new Vector3(transform.localPosition.x + mSpeed, transform.localPosition.y, transform.localPosition.z);

            yield return null;
        }
    }
}
