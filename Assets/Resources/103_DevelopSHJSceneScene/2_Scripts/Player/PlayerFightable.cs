using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum PlayerWeaponType
{
    Knife,
    Gun
}

[System.Serializable]
public class WeaponAttackInfo
{
    public PlayerWeaponType mType;
    public float mAttackAbil = 0f;
    public float mAttackableDistance = 1f;
    public UITrigger mTrigger = null;
    public bool mNeededPreparation = false;
    public UITrigger mPreAniTrigger = null;
    public UITrigger mEndTrigger = null;
}

public class PlayerFightable : Fightable
{
    #region Variables
    public List<WeaponAttackInfo> mWeaponTriggers = null;
    public InteractionTrigger mITrigger = null;

    private UITrigger mPreAniTrigger = null;
    private bool mPreparationNeeded = false;
    private bool mPrepared = false;
    private Dictionary<PlayerWeaponType, WeaponAttackInfo> mWeaponDic = new Dictionary<PlayerWeaponType, WeaponAttackInfo>();
    #endregion

    // Use this for initialization
    void Start ()
    {
	    foreach(var iter in mWeaponTriggers)
        {
            mWeaponDic.Add(iter.mType, iter);
        }
        SetWeaponType(PlayerWeaponType.Gun);
	}

    public void SetWeaponType(PlayerWeaponType fType)
    {
        if(mWeaponDic.ContainsKey(fType))
        {
            WeaponAttackInfo info = null;
            mWeaponDic.TryGetValue(fType, out info);

            mAnimationTrigger = info.mTrigger;
            mAttackAbil = info.mAttackAbil;
            mAttackableDistance = info.mAttackableDistance;

            mPrepared = false;
            mPreparationNeeded = info.mNeededPreparation;
            mPreAniTrigger = info.mPreAniTrigger;

            mEndAniTrigger = info.mEndTrigger;
        }
    }

    public override void ActEndAni()
    {
        mPrepared = false;
        mITrigger.InteractionTriggerExit();

        base.ActEndAni();
    }

    public override void ActAttackAni()
    {
        if (mPreparationNeeded)
        {
            if (!mPrepared)
            {
                mPrepared = true;
                mPreAniTrigger.Act();
            }
            else
            {
                mAnimationTrigger.Act();
            }
        }
    }

    public override void Attack(Fightable fOther)
    {
        if(mPreparationNeeded)
        {
            if(mPrepared)
            {
                base.Attack(fOther);
            }
            else
            {
                mPrepared = true;
                mPreAniTrigger.Act();
            }
        }
        else
        {
            base.Attack(fOther);
        }
    }
}
