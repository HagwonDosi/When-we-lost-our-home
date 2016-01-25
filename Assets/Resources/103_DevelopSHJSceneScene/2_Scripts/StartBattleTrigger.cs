using UnityEngine;
using System.Collections;

public class StartBattleTrigger : UITrigger
{
    [SerializeField]
    private CollisionTrigger mCTrigger = null;
    [SerializeField]
    private Fightable mThisFight = null;
    [SerializeField]
    private EnemyControl mECon = null;

    public override void Act()
    {
        mECon.Target = false;
        Debug.Log("fight with " + mCTrigger.ColliderObject.name);
        BattleDirector.Instance.StartFight(new FightPair(mThisFight, mCTrigger.ColliderObject.GetComponent<Fightable>()));
    }
}
