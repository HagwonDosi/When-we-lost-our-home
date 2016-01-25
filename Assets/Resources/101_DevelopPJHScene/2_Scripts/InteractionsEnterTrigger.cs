using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionsEnterTrigger : UITrigger
{
    [SerializeField]
    private List<InteractionTrigger> mTriggers = null;
    [SerializeField]
    private CollisionTrigger mCTrigger = null;

    public override void Act()
    {
        InteractionControl con = mCTrigger.ColliderObject.GetComponent<InteractionControl>();

        foreach(var iter in mTriggers)
        {
            iter.InteractionTriggerEnter(con);
        }
    }
}
