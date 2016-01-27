using UnityEngine;
using System.Collections;

public class PlayerFollowTrigger : UITrigger
{
    #region Variables
    [SerializeField]
    private EnemyControl mECon = null;

    #endregion

    public override void Act()
    {
        mECon.Target = true;
    }
}