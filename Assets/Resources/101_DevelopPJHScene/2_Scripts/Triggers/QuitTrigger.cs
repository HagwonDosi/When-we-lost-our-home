using UnityEngine;
using System.Collections;

public class QuitTrigger : UITrigger
{
    public PlayerControl mPCon = null;
    public Animator mAnimatr = null;
    public BuildingLoader mLoader = null;
    public SmooothCamera mCamera = null;

    private float mSpeed = 0.01f;
    private float mOriDeg = 0f;
    private BuildingControl mBuilCon = null;
    private TweenRotation mTRot = null;
    private GameObject mPlayer = null;

    void Start()
    {
        mSpeed = GameDirector.Instance.mEventSpeed;
    }

    public override void Act()
    {
        mBuilCon = BuildingLoader.Instance.CurBuiding.GetComponent<BuildingControl>();
        mPlayer = GameDirector.Instance.Player;
        mTRot = mPlayer.GetComponent<TweenRotation>();
        mPCon.mCheckAni = false;

        mAnimatr.SetBool("Player_Run", true);
        mTRot.enabled = true;
        mTRot.from = mPlayer.transform.localEulerAngles;

        float target = 180f;
        mOriDeg = 90f;

        if(Mathf.Abs(mPlayer.transform.localEulerAngles.y) == 270f)
        {
            mOriDeg = 270f;
        }
        

        mTRot.to = new Vector3(mPlayer.transform.localEulerAngles.x, target, mPlayer.transform.localEulerAngles.z);
        mTRot.duration = 0.6f;
        mTRot.ResetToBeginning();

        SmooothCamera.Instance.OutBuildingCamera();
        SmooothCamera.Instance.CircularMovementTo(180, 5f, mPCon.transform);
        UIDirector.Instance.SetEnabledUILayer(0, false);
        StartCoroutine(WalkFront1());
    }

    private IEnumerator WalkFront1()
    {
        yield return new WaitForSeconds(0.6f);

        float targetZ = BuildingLoader.Instance.CurEntrace.transform.position.z - 0.1f;
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
        SmooothCamera.Instance.CircularMovementTo(180, 5f, mPCon.transform);
        StartCoroutine(ReserveSmoothOutBuilding());
        Destroy(mLoader.CurBuiding);

        mTRot.enabled = true;
        mTRot.from = mPlayer.transform.localEulerAngles;
        mTRot.to = new Vector3(mPlayer.transform.localEulerAngles.x, mOriDeg, mPlayer.transform.localEulerAngles.z);
        mTRot.duration = 0.6f;
        mTRot.ResetToBeginning();

        yield return new WaitForSeconds(0.6f);
        
        UIDirector.Instance.SetEnabledUILayer(0, true);
        mPCon.mCheckAni = true;
        mAnimatr.SetFloat("Speed", 0f);
        StopAllCoroutines();
    }

    private IEnumerator ReserveSmoothOutBuilding()
    {
        yield return new WaitForSeconds(0.6f);

        SmooothCamera.Instance.OutBuildingSmooth();
    }
}
