using UnityEngine;
using System.Collections;

public class TriggerDoor : UITrigger
{
    public Animator mAnimator = null;

    public override void Act()
    {
        mAnimator.SetBool("Opened", !mAnimator.GetBool("Opened"));
    }
}
