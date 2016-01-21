using UnityEngine;
using System.Collections;

public class EnemyAttackedTrigger : UITrigger
{
    public Animator mAnimator = null;
    public EnemyControl mECon = null;

    public override void Act()
    {
        mECon.Target = true;
    }
}
