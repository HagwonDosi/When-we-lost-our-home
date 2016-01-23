using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InteractionsExitTrigger : UITrigger
{
    [SerializeField]
    private List<InteractionTrigger> mTriggers = null;

    public override void Act()
    {
        foreach (var iter in mTriggers)
        {
            iter.InteractionTriggerExit();
        }
    }
}
