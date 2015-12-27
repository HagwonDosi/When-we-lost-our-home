using UnityEngine;
using System.Collections;

/*
 * 이 스크립트는 말풍선 오브젝트에 붙여서 말풍선에 관한 전체적인 것을 조율하게 하는 스크립트
 * TweenScale을 필요로 함
*/
public class SpeechBubbleControl : MonoBehaviour
{
    public ShowTextSlowly mText = null;
    public GameObject mSubject = null;
    public Camera mCamera = null;
    public UISprite mBackgroundSpr = null;

    private TweenScale mScale = null;

	// Use this for initialization
	void Start ()
    {
        mScale = GetComponent<TweenScale>();
        mScale.enabled = false;

        StartCoroutine(KeepBubblePlace());
	}
	
    IEnumerator KeepBubblePlace()
    {
        Vector2 OriSubjectPos = mCamera.WorldToScreenPoint(mSubject.transform.position);
        Vector2 oriPos = transform.position;

        while(true)
        {
            Vector2 curSubPos = mCamera.WorldToScreenPoint(mSubject.transform.position);
            Vector2 subDif = curSubPos - OriSubjectPos;

            transform.position = oriPos + subDif;

            yield return null;
        }
    }

    public bool ShowText(string fStr, float fSec)
    {
        float mSec = (mText.mSecPerLetter * fStr.Length) + fSec + 0.1f;

        mScale.enabled = true;
        mScale.from = new Vector3(0, 0, 0);
        mScale.to = Vector3.one;
        mScale.duration = 0.1f;
        mScale.ResetToBeginning();

        StartCoroutine(ReserveShowText(fStr));

        return true;
    }

    void SetBackgroundSprite()
    {
        Vector2 oriPos = mBackgroundSpr.transform.localPosition;
        var left = mBackgroundSpr.GetComponent<UIWidget>().leftAnchor;
    }

    IEnumerator ReserveShowText(string fStr)
    {
        yield return new WaitForSeconds(0.1f);

        mText.setNewString(fStr);
    }

    IEnumerator ReserveHideBubble(float fSec)
    {
        yield return new WaitForSeconds(fSec);

        mScale.enabled = true;
        mScale.from = Vector3.one;
        mScale.to = Vector3.zero;
        mScale.duration = 0.1f;
        mScale.ResetToBeginning();
    }
}
