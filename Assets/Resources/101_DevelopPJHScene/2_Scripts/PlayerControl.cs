using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    #region Variables
    public StickControl mController;
    public float mMaxSpeed = 0.1f;
    public Animator mAnimator = null;
    public float mSpeed = 0;

    private bool mFacingRight = true;
    private GameObject gun = null;
    private Rigidbody mRB = null;
    #endregion

    #region VirtualFunctions
    // Use this for initialization
    void Start ()
    {
        mRB = GameDirector.CustomGetComponent<Rigidbody>(gameObject);
        StartCoroutine(UpdateSpeed());
	}
    #endregion

    #region CustomFunctions
    IEnumerator UpdateSpeed()
    {
        while(true)
        {
            /*if(mController.StickVector.Equals(Vector3.zero))
            {
                Ani.SetBool("Player_Run", false);
                Ani.SetBool("Player_Gun_Run", false);
                if(mSpeed < 0 )
                {
                    mSpeed += mRetardationSpeed;
                    if (mSpeed > 0)
                        mSpeed = 0;
                }
                else if(mSpeed > 0 )
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
                    Ani.SetBool("Player_Run", true);
                    if (Ani.GetBool("Player_Monster") == true)
                    {
                        Ani.SetBool("Player_Gun_Run", true);
                        mSpeed -= mController.StickVector.x / 2;
                    }
                    mSpeed += mController.StickVector.x;

                    if (mSpeed < -mMaxSpeed && Player_Right == true)
                    {
                        Flip();
                        mSpeed = -mMaxSpeed;
                    }
                    else if (mSpeed > mMaxSpeed && Player_Right != true)
                    {
                        Flip();
                        mSpeed = mMaxSpeed;
                    }
                }
            }

            transform.localPosition = new Vector3(transform.localPosition.x + mSpeed, transform.localPosition.y, transform.localPosition.z);*/

            if(Mathf.Abs(mRB.velocity.x) <= mMaxSpeed)
            {
                Vector3 force = Vector3.right * mController.StickVector.x * Time.deltaTime * 62.5f * mSpeed;
                mRB.AddForce(force);

                if(force.x < 0 && mFacingRight)
                {
                    Flip();
                }
                else if (force.x > 0 && !mFacingRight)
                {
                    Flip();
                }
                
                if(Mathf.Round(mRB.velocity.x) != 0)
                {
                    mAnimator.SetBool("Player_Run", true);
                }
                else
                {
                    mAnimator.SetBool("Player_Run", false);
                }
            }
            yield return null;
        }
    }

    public void Flip()
    {
        mFacingRight = !mFacingRight;
        Vector3 Rote = transform.localEulerAngles;

        Rote.y *= -1;

        transform.localEulerAngles = Rote;
    }
    #endregion
}
