using UnityEngine;
using System.Collections;

/// <summary>
/// 외부 건물에 넣어서 작동시킬 트리거
/// </summary>
public class EntranceDoorTrigger : UITrigger
{
    [SerializeField]
    private EntranceTrigger mETrigger = null;
    /// <summary>
    /// 외부 건물의 Animator 객체
    /// </summary>
    private Animator mAnimator = null;

    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    public override void Act()
    {
        mETrigger.DoorOpening();
        mAnimator.SetBool("Opened", true);
    }

    void DoorOpened()
    {
        mETrigger.StartWalkingAgain();
    }
}
