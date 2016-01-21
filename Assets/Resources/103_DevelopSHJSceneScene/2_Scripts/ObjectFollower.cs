using UnityEngine;
using System.Collections;

/// <summary>
/// 정해진 오브젝트를 따라가게 만드는 스크립트0
/// </summary>
public class ObjectFollower : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// 따라갈 목표
    /// </summary>
    public Transform mTarget = null;

    private Vector3 mTargetOrigin = Vector3.zero;
    private Vector3 mThisOrigin = Vector3.zero;
    #endregion

    // Use this for initialization
    void Start ()
    {
        mTargetOrigin = mTarget.position;
        mThisOrigin = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        SyncPosition();
	}

    #region CustomFunctions
    private void SyncPosition()
    {
        Vector3 targetDif = -mTargetOrigin + mTarget.position;

        transform.position = targetDif + mThisOrigin;
    }

    #endregion
}
