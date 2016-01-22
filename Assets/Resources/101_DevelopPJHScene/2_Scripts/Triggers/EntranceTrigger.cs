using UnityEngine;
using System.Collections;

public class EntranceTrigger : UITrigger
{
    #region Variables
    public Animator mAnimator = null;
    public PlayerControl mPCon = null;
    public CollisionTrigger mCTrigger = null;
    public BuildingLoader mLoader = null;
    public SmooothCamera mCamera = null;

    private float mSpeed = 0.01f;
    private float mOriDeg = 0f;
    #endregion

    public override void Act()
    {
        Entrance ent = null;
        try
        {
            ent = mCTrigger.ColliderObject.GetComponent<Entrance>();
        }
        catch
        {
            Debug.LogWarning(gameObject.name + ".EntranceTrigger.Act() " + "error get ent");
            return;
        }

        mLoader.LoadPrefabMap(ent.mEntranceTo);
        mLoader.AddExitInCurBuiding(ent);
        mPCon.mCheckAni = false;
        mAnimator.SetBool("Player_Run", true);
        StartCoroutine(WalkBackOutSide());
    }

    /// <summary>
    /// 바깥에서 걸어들어간다.
    /// </summary>
    /// <returns></returns>
    private IEnumerator WalkBackOutSide()
    {
        GameObject player = GameDirector.Instance.Player;
        TweenRotation tRot = player.GetComponent<TweenRotation>();
        tRot.enabled = true;
        tRot.from = player.transform.localEulerAngles;

        float target = 0f;
        if (Mathf.Round(player.transform.localEulerAngles.y) == 90f)
        {
            mOriDeg = 90f;
            target = 0f;
        }
        else if (Mathf.Round(player.transform.localEulerAngles.y) == 270)
        {
            mOriDeg = -90f;
            target = 360f;
        }

        Debug.Log("target " + target);
        tRot.to = new Vector3(player.transform.localEulerAngles.x, target, player.transform.localEulerAngles.z);
        tRot.duration = 0.3f;
        tRot.ResetToBeginning();
        
        
        yield return new WaitForSeconds(0.3f);
        
        while(true)
        {
            Vector3 local = player.transform.position;
            local.z += Time.deltaTime * 62.5f * mSpeed;
            player.transform.position = local;

            yield return new WaitForFixedUpdate();
        }
    }

    public void DoorOpening()
    {
        StopAllCoroutines();
        mAnimator.SetBool("Player_Run", false);
    }

    /// <summary>
    /// 문이 열리고 다시 걷기 시작한다.
    /// </summary>
    public void StartWalkingAgain()
    {
        mAnimator.SetBool("Player_Run", true);
        StartCoroutine(WalkInBuilding());
    }

    /// <summary>
    /// 건물 안으로 걸어들어간다
    /// </summary>
    /// <returns></returns>
    private IEnumerator WalkInBuilding()
    {
        mCamera.InBuilding();
        GameObject player = GameDirector.Instance.Player;
        float targetZ = mLoader.CurBuiding.GetComponent<BuildingControl>().mExit.transform.position.z;
        while (player.transform.position.z <= targetZ)
        {
            Vector3 local = player.transform.position;
            local.z += Time.deltaTime * 62.5f * mSpeed;
            player.transform.position = local;

            yield return null;
        }

        TweenRotation tRot = player.GetComponent<TweenRotation>();
        tRot.enabled = true;
        tRot.from = player.transform.localEulerAngles;
        tRot.to = new Vector3(player.transform.localEulerAngles.x, mOriDeg, player.transform.localEulerAngles.z);
        tRot.duration = 0.3f;
        tRot.ResetToBeginning();

        yield return new WaitForSeconds(0.3f);

        mAnimator.SetBool("Player_Run", false);
        mLoader.CurExternBuildingControl.Animator.SetBool("Opened", false);
        mPCon.mCheckAni = true;
        StopAllCoroutines();
    }
}
