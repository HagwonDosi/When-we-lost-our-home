using UnityEngine;
using System.Collections;

public class EndBattleTrigger : UITrigger
{
    public Fightable mFightable = null;

    public override void Act()
    {
        Debug.Log("end fight res " + BattleDirector.Instance.EndFight(mFightable));
    }
}
