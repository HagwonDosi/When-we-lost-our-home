using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// 플레이어의 인터액션 버튼을 통한 UITrigger
/// </summary>
public class AttackTrigger : UITrigger
{
    public CollisionTrigger mCTrigger = null;
    public PlayerFightable mPlayer = null;

    public override void Act()
    {
        Debug.Log("AttackTrigger enter");
        
        mPlayer.ActAttackAni();
    }
}
