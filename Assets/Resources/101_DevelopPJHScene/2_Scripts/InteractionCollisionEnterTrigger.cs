using UnityEngine;
using System.Collections;

public class InteractionCollisionEnterTrigger : UITrigger
{
    public CollisionTrigger mCTrigger = null;
    public InteractionTrigger mITrigger = null;

    public override void Act()
    {
        Debug.Log(name + " Trigger Enter with " + mCTrigger.ColliderObject.gameObject);
        InteractionControl iCon = mCTrigger.ColliderObject.GetComponent<InteractionControl>();

        mITrigger.InteractionTriggerEnter(iCon);
    }
}
