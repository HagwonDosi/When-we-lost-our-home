using UnityEngine;
using System.Collections;

public class InteractionCollisionExitTrigger : UITrigger
{
    public CollisionTrigger mCTrigger = null;
    public InteractionTrigger mITrigger = null;

    public override void Act()
    {
        mITrigger.InteractionTriggerExit();
    }
}
