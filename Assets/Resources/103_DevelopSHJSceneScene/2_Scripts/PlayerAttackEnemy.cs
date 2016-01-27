using UnityEngine;
using System.Collections;

public class PlayerAttackEnemy : UITrigger
{
    private Fightable mFightable = null;
    private CollisionTrigger mCTrigger = null;

	// Use this for initialization
	void Start ()
    {
        mFightable = GameDirector.Instance.Player.GetComponent<Fightable>();
        mCTrigger = GetComponent<CollisionTrigger>();
	}

    public override void Act()
    {
        Fightable oppo = null;

        try
        {
            oppo = mCTrigger.ColliderObject.GetComponent<Fightable>();
        }
        catch (System.Exception ex)
        {
            Debug.LogWarning(gameObject.name + ".AttackTrigger.Act() " + ex.Message);
        }

        mFightable.Attack(oppo);
    }
}
