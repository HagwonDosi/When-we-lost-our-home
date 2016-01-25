using UnityEngine;
using System.Collections;

public class ItemTrigger : UITrigger
{
    private PlayerControl mPCon = null;
    private CollisionTrigger mCTrigger = null;
    private TweenRotation mTRot = null;
    private float mOriDeg = 0f;
    private float mOriZ = 0f;
    private ItemControl mICon = null;
    [SerializeField]
    private float mSpeed = 0.01f;

	// Use this for initialization
	void Start ()
    {
        mSpeed = GameDirector.Instance.mEventSpeed;
        mPCon = GameDirector.Instance.Player.GetComponent<PlayerControl>();
        mCTrigger = mPCon.GetComponent<CollisionTrigger>();
        mTRot = mPCon.GetComponent<TweenRotation>();
	}

    public override void Act()
    {
        UIDirector.Instance.SetEnabledUILayer(0, false);
        mPCon.mCheckAni = false;
        mPCon.mAnimator.SetBool("Player_Run", true);
        mOriZ = mPCon.transform.position.z;

        mTRot.enabled = true;
        mTRot.from = mPCon.transform.localEulerAngles;

        float target = 0f;
        if (Mathf.Round(mPCon.transform.localEulerAngles.y) == 90f)
        {
            mOriDeg = 90f;
            target = 0f;
        }
        else if (Mathf.Round(mPCon.transform.localEulerAngles.y) == 270)
        {
            mOriDeg = 270f;
            target = 360f;
        }

        Debug.Log("target " + target);
        mTRot.to = new Vector3(mPCon.transform.localEulerAngles.x, target, mPCon.transform.localEulerAngles.z);
        mTRot.duration = 0.3f;
        mTRot.ResetToBeginning();

        mICon = mCTrigger.ColliderObject.GetComponent<InteractionControl>().mSubject.GetComponent<ItemControl>();

        StartCoroutine(WalkToItem());
    }

    private IEnumerator WalkToItem()
    {
        yield return new WaitForSeconds(0.3f);

        float targetZ = mICon.transform.position.z - 0.1f;
        while(mPCon.transform.position.z <= targetZ)
        {
            Vector3 pos = mPCon.transform.position;
            pos.z += Time.deltaTime * 62.5f * mSpeed;
            mPCon.transform.position = pos;

            yield return null;
        }

        mPCon.mAnimator.SetBool("Player_Run", false);
        mPCon.mAnimator.SetBool("PlayerItem", true);

        yield return new WaitForSeconds(0.8f);

        mPCon.mAnimator.SetBool("Player_Run", true);

        mTRot.enabled = true;
        mTRot.from = mPCon.transform.localEulerAngles;

        float target = 180f;

        Debug.Log("target " + target);
        mTRot.to = new Vector3(mPCon.transform.localEulerAngles.x, target, mPCon.transform.localEulerAngles.z);
        mTRot.duration = 0.3f;
        mTRot.ResetToBeginning();

        yield return new WaitForSeconds(0.3f);
        
        while (mPCon.transform.position.z >= mOriZ)
        {
            Vector3 pos = mPCon.transform.position;
            pos.z -= Time.deltaTime * 62.5f * mSpeed;
            mPCon.transform.position = pos;

            yield return null;
        }

        mTRot.enabled = true;
        mTRot.from = mPCon.transform.localEulerAngles;
        
        mTRot.to = new Vector3(mPCon.transform.localEulerAngles.x, mOriDeg, mPCon.transform.localEulerAngles.z);
        mTRot.duration = 0.3f;
        mTRot.ResetToBeginning();

        yield return new WaitForSeconds(0.3f);

        UIDirector.Instance.SetEnabledUILayer(0, true);
        mPCon.mCheckAni = true;
        mPCon.mAnimator.SetBool("Player_Run", false);
    }
}
