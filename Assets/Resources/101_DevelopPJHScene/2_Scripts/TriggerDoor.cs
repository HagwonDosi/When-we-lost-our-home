using UnityEngine;
using System.Collections;

public class TriggerDoor : UITrigger
{
    public CollisionTrigger mCTrigger = null;

    public override void Act()
    {
        if(CheckTime())
        {
            Animator animator = mCTrigger.ColliderObject.GetComponent<InteractionControl>().mSubject.GetComponent<Animator>();

            animator.SetBool("Opened", !animator.GetBool("Opened"));

            setTime();
        }
        
    }
}
