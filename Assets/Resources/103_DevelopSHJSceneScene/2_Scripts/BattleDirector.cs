using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class FightPair
{
    public Fightable mAttackObj;
    public Fightable mOpponentObj;

    public FightPair()
    {
        mAttackObj = null;
        mOpponentObj = null;
    }

    public FightPair(Fightable attack, Fightable oppo)
    {
        mAttackObj = attack;
        mOpponentObj = oppo;
    }
}

/// <summary>
/// 전투 행위, 캐릭터의 죽음을 관장하는 관리자
/// </summary>
public class BattleDirector : MonoBehaviour
{
    #region Variables
    static private BattleDirector mInstance = null;
    public Fightable mJustAttacked = null;

    private List<FightPair> mPairs = new List<FightPair>();
    #endregion

    #region get/setter
    static public BattleDirector Instance
    {
        get
        {
            if(mInstance == null)
            {
                mInstance = GameObject.FindObjectOfType<BattleDirector>();
            }

            //히어아키 상에 BattleDirector가 없다면
            if(mInstance == null)
            {
                GameObject obj = new GameObject("BattleDirector");
                mInstance = obj.AddComponent<BattleDirector>();
            }

            return mInstance;
        }
    }
    #endregion

    #region VirtualFunctions
    // Use this for initialization
    void Start ()
    {
	
	}
    #endregion

    #region CustomFunctions
    /// <summary>
    /// 전투를 관리하는 코루틴
    /// </summary>
    /// <returns>한 프레임마다 반복</returns>
    private IEnumerator FightCheck(FightPair pair)
    {
        float sec = 0f;
        while (true)
        {
            sec += Time.deltaTime;

            if (CheckDeath(pair.mOpponentObj))
            {
                Debug.Log("죽음");
                pair.mOpponentObj.mStatus.Dead();
                EndFight(pair.mAttackObj);
                break;
            }
            if(CheckDeath(pair.mAttackObj))
            {
                Debug.Log("죽음");
                pair.mAttackObj.mStatus.Dead();
                EndFight(pair.mAttackObj);
                break;
            }

            //만약 공격할 시간이 되면
            if (pair.mAttackObj.AttackTerm <= sec)
            {
                sec = 0f;

                pair.mAttackObj.ActAttackAni();

                Debug.Log("attack " + pair.mAttackObj + " oppo " + pair.mOpponentObj);
            }

            yield return null;
        }
    }

    /// <summary>
    /// 죽었는지 검사하라
    /// </summary>
    /// <param name="fOppo">검사할 대상</param>
    /// <returns>죽었으면 true, 살았으면 false</returns>
    private bool CheckDeath(Fightable fOppo)
    {
        Debug.Log("oppo " + fOppo);
        if(fOppo.mStatus.GetStatus("Health") == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartFight(FightPair pair)
    {
        Debug.Log(pair.mAttackObj.name + " Start Fight against " + pair.mOpponentObj.name);
        mPairs.Add(pair);
        StartCoroutine(FightCheck(pair));
    }

    public void StartFight(PlayerFightable player, Fightable oppo)
    {
        FightPair pair = new FightPair(player, oppo);
        if(!mPairs.Exists(x => x.mAttackObj == player))
        {
            mPairs.Add(pair);
        }
    }

    public bool EndFight(Fightable fAttack)
    {
        Debug.Log("End Fight");
        foreach (var iter in mPairs)
        {
            if (iter.mAttackObj == fAttack)
            {
                iter.mAttackObj.ActEndAni();
                iter.mOpponentObj.ActEndAni();
                mPairs.Remove(iter);
                StopAllCoroutines();
                return true;
            }
        }

        return false;
    }

    public void FindAndGiveDamage(Fightable fAttack)
    {
        foreach(var iter in mPairs)
        {
            if(iter.mAttackObj.name.Equals(fAttack.name))
            {
                fAttack.Attack(iter.mOpponentObj);
                return;
            }
        }
        Debug.LogWarning("coundn't find " + fAttack.name + " pair");
    }

    #endregion
}
