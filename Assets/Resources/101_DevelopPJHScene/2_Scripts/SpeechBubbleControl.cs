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
    private UIWidget mWidget = null;
    private bool isTalking = false;

	// Use this for initialization
	void Start ()
    {
        mScale = mBackgroundSpr.GetComponent<TweenScale>();
        mScale.enabled = false;
        mWidget = mBackgroundSpr.GetComponent<UIWidget>();

        StartCoroutine(KeepBubblePlace());
        StartCoroutine(KeepBubblePlace());
	}
	
    IEnumerator KeepBubblePlace()
    {
        Vector2 OriSubjectPos = mCamera.WorldToScreenPoint(mSubject.transform.position);
        Vector2 oriPos = transform.localPosition;

        while(true)
        {
            Vector2 curSubPos = mCamera.WorldToScreenPoint(mSubject.transform.position);
            Vector2 subDif = curSubPos - OriSubjectPos;

            transform.localPosition = oriPos + subDif;

            yield return null;
        }
    }

    public bool ShowText(string fStr, float fSec)
    {
        if(!isTalking)
        {
            isTalking = true;
            float sec = (mText.mSecPerLetter * fStr.Length) + fSec + 0.1f;

            mScale.enabled = true;
            mScale.from = new Vector3(0, 0, 0);
            mScale.to = Vector3.one;
            mScale.duration = 0.1f;
            mScale.ResetToBeginning();

            SetBackgroundSprite(fStr);

            StartCoroutine(ReserveShowText(fStr));
            StartCoroutine(ReserveHideBubble(sec));

            return true;
        }
        else
        {
            Debug.LogWarning(gameObject.name + ".ShowText " + "isTaking is true");

            return false;
        }
    }

    void SetBackgroundSprite(string fStr)
    {
        Vector2 size = mText.getFullSize(fStr);
        mWidget.width = (int)(size.x) + 100;
        mWidget.height = (int)(size.y) + 50;
        Vector2 oriPos = mBackgroundSpr.transform.localPosition;
        float left = 0;
        float right = 0;
        float width = mWidget.width;

        switch (mWidget.pivot)
        {
            case UIWidget.Pivot.BottomRight:
                left = mBackgroundSpr.transform.localPosition.x + width;
                right = mBackgroundSpr.transform.localPosition.x;

                break;
            case UIWidget.Pivot.BottomLeft:
                left = mBackgroundSpr.transform.localPosition.x;
                right = mBackgroundSpr.transform.localPosition.x + width;

                break;
        }

        Vector2 sceneSize = GameDirector.Instance.getPanelSize();

        if(sceneSize.x / 2 < right)
        {
            mWidget.pivot = UIWidget.Pivot.BottomRight;
        }
        else if( - sceneSize.x / 2 > left)
        {
            mWidget.pivot = UIWidget.Pivot.BottomLeft;
        }

        mBackgroundSpr.transform.localPosition = oriPos;
    }

    IEnumerator ReserveShowText(string fStr)
    {
        yield return new WaitForSeconds(0.2f);

        mText.setNewString(fStr);
    }

    IEnumerator ReserveHideBubble(float fSec)
    {
        yield return new WaitForSeconds(fSec);

        isTalking = false;
        mText.ClearText();
        mScale.enabled = true;
        mScale.from = Vector3.one;
        mScale.to = Vector3.zero;
        mScale.duration = 0.1f;
        mScale.ResetToBeginning();
    }
}
