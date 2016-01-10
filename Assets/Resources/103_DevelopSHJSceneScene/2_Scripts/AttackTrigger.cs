using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public struct Enemy_StateInfo
{
    public Enemy_State mState;
    public float mAttack;
}

public class AttackTrigger : UITrigger
{
    public Animator Ani = null;
    public List<Enemy_StateInfo> mEnemy_State;

    public Enemy_Target eEnemy;

    public override void Act()
    {
        foreach (var it in mEnemy_State)
        {

            if (it.mState != null)
            {
                it.mState.AddHP(-it.mAttack);
            }
            else
            {
                Debug.LogWarning(gameObject.name + ".AttackTrigger " + "mState is null");
            }
        }
    }
}
