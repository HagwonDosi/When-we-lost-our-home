using UnityEngine;
using System.Collections;

/// <summary>
/// Fightable의 AttackableDistance 멤버를 구 콜리더에 동기화한다.
/// </summary>
public class SyncColliderAttackableDistance : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private Fightable mFightableObj = null;
    private SphereCollider mCollider = null;

    #endregion

    #region VirtualFunctions
    // Use this for initialization
    void Start ()
    {
        mCollider = GameDirector.CustomGetComponent<SphereCollider>(gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    {
        SyncCollider();
	}
    #endregion

    #region CustomFunctions
    private void SyncCollider()
    {
        mCollider.radius = mFightableObj.mAttackableDistance;
    }

    #endregion
}
