using UnityEngine;
using System.Collections;

public class EnemyAnimationEvent : MonoBehaviour
{
    public Collider mCollider = null;
    public Animator mAnimator = null;

    void Mon_Attack()
    {
        mCollider.enabled = true;
    }

    void Mon_Attack_End()
    {
        mCollider.enabled = false;
    }

    void AttackEnd()
    {
        mAnimator.SetBool("Monster_Attack_chek", false);
    }
}
