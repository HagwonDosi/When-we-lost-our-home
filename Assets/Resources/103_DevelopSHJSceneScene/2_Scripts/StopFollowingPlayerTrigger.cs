using UnityEngine;
using System.Collections;

public class StopFollowingPlayerTrigger : UITrigger
{
    [SerializeField]
    private EnemyControl mECon = null;

    public override void Act()
    {
        mECon.Target = false;
    }
}
