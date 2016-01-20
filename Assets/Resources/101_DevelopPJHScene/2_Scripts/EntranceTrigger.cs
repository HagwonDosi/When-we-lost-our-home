using UnityEngine;
using System.Collections;

public class EntranceTrigger : UITrigger
{
    #region Variables
    public Animator mAnimator = null;
    public PlayerControl mPCon = null;
    public CollisionTrigger mCTrigger = null;
    public MapLoader mLoader = null;
    public SmooothCamera mCamera = null;

    private float mSpeed = 0.01f;
    private float mDegDif = 0f;
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
        GameObject player = GameDirector.Instance.mPlayer;
        TweenRotation tRot = player.GetComponent<TweenRotation>();
        tRot.enabled = true;
        tRot.from = player.transform.localEulerAngles;
        tRot.to = new Vector3(player.transform.localEulerAngles.x, player.transform.localEulerAngles.y - 90, player.transform.localEulerAngles.z);
        tRot.duration = 0.3f;
        tRot.ResetToBeginning();

        mDegDif = 0 - player.transform.localEulerAngles.y;
        
        yield return new WaitForSeconds(0.3f);

        Debug.Log(mAnimator.GetBool("Player_Run"));
        float targetZ = player.transform.localPosition.z + 0.5f;
        while(player.transform.localPosition.z <= targetZ)
        {
            Vector3 local = player.transform.localPosition;
            local.z += Time.deltaTime * 62.5f * mSpeed;
            player.transform.localPosition = local;

            yield return new WaitForFixedUpdate();
        }

        StopAllCoroutines();
        StartCoroutine(WalkInBuilding());
    }

    /// <summary>
    /// 건물 안으로 걸어들어간다
    /// </summary>
    /// <returns></returns>
    private IEnumerator WalkInBuilding()
    {
        mLoader.CurBuiding.GetComponent<BuildingControl>().SetPlayer();
        GameObject player = GameDirector.Instance.mPlayer;
        float targetZ = player.transform.localPosition.z + 0.5f;
        while (player.transform.localPosition.z <= targetZ)
        {
            Vector3 local = player.transform.localPosition;
            local.z += Time.deltaTime * 62.5f * mSpeed;
            player.transform.localPosition = local;

            yield return null;
        }

        TweenRotation tRot = player.GetComponent<TweenRotation>();
        tRot.enabled = true;
        tRot.from = player.transform.localEulerAngles;
        tRot.to = new Vector3(player.transform.localEulerAngles.x, player.transform.localEulerAngles.y + 90, player.transform.localEulerAngles.z);
        tRot.duration = 0.3f;
        tRot.ResetToBeginning();

        yield return new WaitForSeconds(0.3f);

        mAnimator.SetBool("Player_Run", false);
        mPCon.mCheckAni = true;
        StopAllCoroutines();
    }
}
