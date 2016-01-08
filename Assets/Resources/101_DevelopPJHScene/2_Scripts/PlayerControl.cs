using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public StickControl mController;
    public float mMaxSpeed = 0.1f;
    public float mRetardationSpeed = 0.01f;
    public Animator Ani = null;

    private bool Player_Right = true;
    private Enemy_State E_state;
    private float Pos_x;
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
            
            if(mController.StickVector.Equals(Vector3.zero))
            {
                Ani.SetBool("Player_Run", false);
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

            transform.localPosition = new Vector3(transform.localPosition.x + mSpeed, transform.localPosition.y, transform.localPosition.z);
            yield return null;
        }
    }

    public void Flip()
    {
        Player_Right = !Player_Right;
        Vector3 Rote = transform.localEulerAngles;

        Rote.y *= -1;

        transform.localEulerAngles = Rote;
    }
}
