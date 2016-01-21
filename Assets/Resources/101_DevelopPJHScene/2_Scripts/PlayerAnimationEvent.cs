using UnityEngine;
using System.Collections;

public class PlayerAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private Collider mRightHand = null;
    private Animator mAnimator = null;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    void GameOver()
    {
        Application.LoadLevel(4);
    }

    void RightHandColliderOn()
    {
        mRightHand.enabled = true;
    }

    void RightHandColliderOff()
    {
        mRightHand.enabled = false;
    }

    void EndRooting()
    {
        mAnimator.SetBool("PlayerItem", false);
    }
}
