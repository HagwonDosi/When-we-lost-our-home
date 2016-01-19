using UnityEngine;
using System.Collections;

public class DestroyTrigger : UITrigger
{
    public override void Act()
    {
        Destroy(gameObject);
    }
}
