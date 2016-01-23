using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * 이 스크립트는 SpeechBubbleControl을 통해서 플레이어의 상태를 출력해줄 스크립트이다.
*/
public class ShowPlayerStatus : MonoBehaviour
{
    [System.Serializable]
    public struct StatusWarnInfo
    {
        public int mMessageIndex;
        public float mDuration;
        public float mValue;
    }

    public ConversationFileControl mFileCon = null;
    public List<StatusWarnInfo> mStatuses = new List<StatusWarnInfo>();
    public int mSpeechBubbleIndex = 0;
    public float mPeriod = 3f;

    protected float mCurValue = 100f;

    private int mCurInfoIndex = 0;
    private bool mIsInforming = false;
    private float mBefMin = 100f;

	// Use this for initialization
	protected void Start ()
    {
        StartCoroutine(UpdateInform());
	}

    void DetermineToInform()
    {
        float maxVal = 0;
        float minVal = 0;
        int theIdx = 0;

        for(int i = 0; i < mStatuses.Count; i++)
        {
            if(mStatuses[i].mValue <= mBefMin)
            {
                if (maxVal < mStatuses[i].mValue)
                {
                    maxVal = mStatuses[i].mValue;
                    theIdx = i;
                }
                else if (minVal < mStatuses[i].mValue)
                {
                    minVal = mStatuses[i].mValue;
                }
            }
        }

        if (mCurValue < minVal)
            mBefMin = minVal;

        Debug.Log("max " + maxVal + " min " + minVal + " befMin " + mBefMin);
        if(mCurValue <= maxVal && mCurValue >= minVal)
        {
            mIsInforming = true;
            mCurInfoIndex = theIdx;
        }
    }

    void DisplayMsg()
    {
        Debug.Log("Display");
        mFileCon.ShowTextByIndex(mStatuses[mCurInfoIndex].mMessageIndex, mSpeechBubbleIndex, mStatuses[mCurInfoIndex].mDuration);
    }

    IEnumerator UpdateInform()
    {
        float curTime = 0;

        while(true)
        {
            curTime += Time.deltaTime;
            DetermineToInform();

            if(mIsInforming && curTime >= mPeriod)
            {
                curTime = 0;
                DisplayMsg();
            }

            yield return null;
        }
    }
}
