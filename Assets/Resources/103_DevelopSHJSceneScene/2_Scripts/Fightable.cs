using UnityEngine;
using System.Collections;

/// <summary>
/// 이 스크립트는 모든 싸울 수 있는 객체에 붙이면 된다.
/// </summary>
public class Fightable : MonoBehaviour
{
    #region Variables
    public Status mStatus = null;
    /// <summary>
    /// 공격 사정 거리
    /// </summary>
    public float mAttackableDistance = 1f;

    [SerializeField]
    /// <summary>
    /// 공격시에 실행할 애니메이션 트리거
    /// </summary>
    protected UITrigger mAnimationTrigger = null;
    [SerializeField]
    /// <summary>
    /// 전투가 끝났을 때 실행할 애니메이션 트리거
    /// </summary>
    protected UITrigger mEndAniTrigger = null;
    [SerializeField]
    /// <summary>
    /// 공격받았을 때 실행할 트리거
    /// </summary>
    protected UITrigger mAttackedTrigger = null;
    [SerializeField]
    /// <summary>
    /// 실수로 표현되는 공격력
    /// </summary>
    protected float mAttackAbil = 0f;

    [SerializeField]
    /// <summary>
    /// 공격 사이의 시간
    /// </summary>
    private float mAttackTerm = 3f;
    #endregion

    #region get/setter
    /// <summary>
    /// [get]공격 사이의 시간
    /// </summary>
    public float AttackTerm
    {
        get
        {
            return mAttackTerm;
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
    /// 적을 넉백시킨다.
    /// </summary>
    /// <param name="fDis">넉백당할 거리</param>
    private void KnuckBack(Vector3 fDis)
    {

    }

    public virtual void ActAttackAni()
    {
        if (mAnimationTrigger != null)
        {
            mAnimationTrigger.Act();
        }
        else
        {
            Debug.LogWarning(gameObject.name + "Fightable.Attack() Doesn't have AnimationTrigger");
        }
    }

    public virtual void ActEndAni()
    {
        if(mEndAniTrigger != null)
        {
            mEndAniTrigger.Act();
        }
        else
        {
            Debug.LogWarning("EndAniTrigger is null");
        }
    }

    public virtual void GotDamaged()
    {
        if (mAttackedTrigger != null)
        {
            mAttackedTrigger.Act();
        }
        else
        {
            Debug.LogWarning("AttackedTrigger is null");
        }
    }

    /// <summary>
    /// 이 Fightable 스크립트가 fOther을 공격한다
    /// </summary>
    /// <param name="fOther">공격할 Fightable 객체</param>
    public virtual void Attack(Fightable fOther)
    {
        fOther.GotDamaged();
        fOther.mStatus.AddValStatus("Health", -mAttackAbil);
    }

    public void SetAttackabelDistance(float val)
    {
        Debug.Log("AttackableDistance Changed to " + val);
        mAttackableDistance = val;
    }

    #endregion
}
