using UnityEngine;
using System.Collections;

public class ExternBuildingControl : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private string mBuildingName = "";
    private Animator mAnimator = null;

    public Transform mBuildingSpawnPlace = null;
    #endregion

    #region Capsules
    public string BuildingName
    {
        get
        {
            return mBuildingName;
        }
    }

    public Animator Animator
    {
        get
        {
            return mAnimator;
        }
    }
    #endregion

    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        mAnimator.SetBool("Opened", true);
    }
}
