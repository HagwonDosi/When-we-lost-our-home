using UnityEngine;
using System.Collections;

public class QuitTrigger : UITrigger
{
    public PlayerControl mPCon = null;
    public Animator mAnimatr = null;
    public SmooothCamera mCamera = null;

    private float mSpeed = 0.01f;
    private float mDegDif = 0f;
    private BuildingControl mBuilCon = null;
    private TweenRotation mTRot = null;
    private GameObject mPlayer = null;

    public override void Act()
    {
        mBuilCon = FindObjectOfType<BuildingControl>();
        mPlayer = GameDirector.Instance.mPlayer;
        mTRot = mPlayer.GetComponent<TweenRotation>();
        mPCon.mCheckAni = false;

        mAnimatr.SetFloat("Speed", 1f);
        mTRot.enabled = true;
        mTRot.from = mPlayer.transform.localEulerAngles;
        mTRot.to = new Vector3(mPlayer.transform.localEulerAngles.x, 180, mPlayer.transform.localEulerAngles.z);
        mTRot.duration = 0.3f;
        mTRot.ResetToBeginning();

        mDegDif = 180f - mPlayer.transform.localEulerAngles.y;
        StartCoroutine(WalkFront1());
    }

    private IEnumerator WalkFront1()
    {
        yield return new WaitForSeconds(0.3f);

        float targetZ = mPlayer.transform.localPosition.z - 0.5f;
        while (mPlayer.transform.localPosition.z >= targetZ)
        {
            Vector3 local = mPlayer.transform.localPosition;
            local.z -= Time.deltaTime * 62.5f * mSpeed;
            mPlayer.transform.localPosition = local;

            yield return new WaitForFixedUpdate();
        }

        StopAllCoroutines();
        StartCoroutine(WalkFront2());
    }

    private IEnumerator WalkFront2()
    {
        mBuilCon.PlayerExit();

        float targetZ = mPlayer.transform.localPosition.z - 0.5f;
        while (mPlayer.transform.localPosition.z >= targetZ)
        {
            Vector3 local = mPlayer.transform.localPosition;
            local.z -= Time.deltaTime * 62.5f * mSpeed;
            mPlayer.transform.localPosition = local;

            yield return new WaitForFixedUpdate();
        }

        mTRot.enabled = true;
        mTRot.from = mPlayer.transform.localEulerAngles;
        mTRot.to = new Vector3(mPlayer.transform.localEulerAngles.x, 180f - mDegDif, mPlayer.transform.localEulerAngles.z);
        mTRot.duration = 0.3f;
        mTRot.ResetToBeginning();

        yield return new WaitForSeconds(0.3f);
        StopAllCoroutines();
        mPCon.mCheckAni = true;

        mAnimatr.SetFloat("Speed", 0f);
    }
}
