using UnityEngine;
using System.Collections;

public class QuitTrigger : UITrigger
{
    public PlayerControl mPCon = null;
    public Animator mAnimatr = null;
    public MapLoader mLoader = null;
    public SmooothCamera mCamera = null;

    private float mSpeed = 0.01f;
    private float mOriDeg = 0f;
    private BuildingControl mBuilCon = null;
    private TweenRotation mTRot = null;
    private GameObject mPlayer = null;

    public override void Act()
    {
        mBuilCon = FindObjectOfType<BuildingControl>();
        mPlayer = GameDirector.Instance.Player;
        mTRot = mPlayer.GetComponent<TweenRotation>();
        mPCon.mCheckAni = false;

        mAnimatr.SetBool("Player_Run", true);
        mTRot.enabled = true;
        mTRot.from = mPlayer.transform.localEulerAngles;

        float target = 180f;
        mOriDeg = 90f;

        if(Mathf.Abs(mPlayer.transform.localEulerAngles.y) == -90f)
        {
            mOriDeg = -90f;
            target = -180f;
        }

        mTRot.to = new Vector3(mPlayer.transform.localEulerAngles.x, target, mPlayer.transform.localEulerAngles.z);
        mTRot.duration = 0.3f;
        mTRot.ResetToBeginning();
        
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
        mTRot.to = new Vector3(mPlayer.transform.localEulerAngles.x, mOriDeg, mPlayer.transform.localEulerAngles.z);
        mTRot.duration = 0.3f;
        mTRot.ResetToBeginning();

        yield return new WaitForSeconds(0.3f);
        StopAllCoroutines();
        mPCon.mCheckAni = true;
        Destroy(mLoader.CurBuiding);
        mAnimatr.SetFloat("Speed", 0f);
    }
}
