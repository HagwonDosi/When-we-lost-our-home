using UnityEngine;
using System.Collections;

public class AttackFightableTrigger : UITrigger
{
    public Fightable mFightable = null;

    public override void Act()
    {
        Debug.Log("AttackFightableTrigger");
        BattleDirector.Instance.FindAndGiveDamage(mFightable);
    }
}
