using UnityEngine;
using System.Collections;

public class SetBoolAniTrigger : UITrigger
{
    public bool mSetBool;
    public string mTriggerName = "";

    [SerializeField]
    private Animator mAnimator = null;

    public override void Act()
    {
        mAnimator.SetBool(mTriggerName, mSetBool);
    }
}
