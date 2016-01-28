using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpeechBubbleDirector : Singletone<SpeechBubbleDirector>
{
    #region Variables
    public bool mSpeechBubbleShow = true;

    private List<SpeechBubbleControl> mControlList = new List<SpeechBubbleControl>();
    private Dictionary<string, ConversationFileControl> mConvDic = new Dictionary<string, ConversationFileControl>();
    private SortedDictionary<int, SpeechBubbleControl> mTouchedBubbles = new SortedDictionary<int, SpeechBubbleControl>();
    [SerializeField]
    private GameObject mSpeechBubble = null;
    [SerializeField]
    private int mCurDepth = 2;
    #endregion

    public List<SpeechBubbleControl> ControlList
    {
        get
        {
            return mControlList;
        }
    }

    #region VirtualFunctions
    void Start()
    {
        var cons = FindObjectsOfType<SpeechBubbleControl>();

        for (int i = 0; i < cons.Length; i++)
        {
            mControlList.Add(cons[i]);
        }

        var convs = FindObjectsOfType<ConversationFileControl>();

        for (int i = 0; i < convs.Length; i++)
        {
            mConvDic.Add(new string(convs[i].FileName.ToCharArray(), 0, convs[i].FileName.Length - 4), convs[i]);
        }
    }

    void LateUpdate()
    {
        TouchedBubblesDepthControl();
    }
    #endregion

    #region CustomFunctions
    private void TouchedBubblesDepthControl()
    {
        if (mTouchedBubbles.Count >= 1)
        {
            foreach (var iter in mControlList)
            {
                iter.SetToBasicDepth();
            }

            Debug.Log("start update");
            int upDepth = mCurDepth + 2;
            foreach (var iter in mTouchedBubbles)
            {
                iter.Value.SetDepth(upDepth);
                break;
            }

            mTouchedBubbles.Clear();
        }

    }

    public SpeechBubbleControl MakeSpeechBubble(Transform fObj, Vector2 fOffset)
    {
        GameObject bubble = Instantiate(mSpeechBubble) as GameObject;
        bubble.transform.parent = UIDirector.Instance.GetUIAnchor(UIAnchor.Side.Center).transform;

        Vector3 screen = SmooothCamera.Instance.Camera3D.WorldToScreenPoint(fObj.position);
        bubble.transform.localPosition = screen;
        bubble.transform.localScale = Vector3.one;

        mCurDepth += 2;
        SpeechBubbleControl con = bubble.GetComponent<SpeechBubbleControl>();
        con.mSubject = fObj.gameObject;
        con.mOffset = fOffset;
        con.BasicDepth = mCurDepth;
        con.SetDepth(mCurDepth);

        mControlList.Add(con);

        return con;
    }

    public bool RemoveSpeechBubble(SpeechBubbleControl fCon)
    {
        return mControlList.Remove(fCon);
    }

    public bool RemoveSpeechBubble(int fIdx)
    {
        if (fIdx >= 0 && fIdx < mControlList.Count)
        {
            Destroy(mControlList[fIdx].gameObject);
            mControlList.RemoveAt(fIdx);

            return true;
        }
        else
        {
            return false;
        }
    }

    public void SpeechBubbleTouched(SpeechBubbleControl fCon)
    {
        Debug.Log("depth " + fCon.BasicDepth);
        mTouchedBubbles.Add(fCon.CurDepth, fCon);
    }

    public string GetConversationString(string fConvName, int fIndex)
    {
        ConversationFileControl convCon = null;
        if (mConvDic.TryGetValue(fConvName, out convCon))
        {
            if (fIndex < 0 || fIndex >= convCon.ConvsList.Count)
            {
                Debug.LogWarning(name + ".SpeechBubbleDirector.GetConversationString() " + "fConvIdx is out of Range");
                return "";
            }

            return mControlList[0].CutString(convCon.ConvsList[fIndex]);
        }
        else
        {
            Debug.LogWarning(name + ".SpeechBubbleDirector.GetConversationString() " + "couldn't find ConvCon");
            return "";
        }
    }

    /// <summary>
    /// 지정하는 말풍선에 텍스트를 표시하는 스크립트
    /// </summary>
    /// <param name="fSpeechIdx">말풍선 인덱스</param>
    /// <param name="fConvName">ConverstaionControl 파일 이름</param>
    /// <param name="fConvIdx">ConverstionControl 인덱스</param>
    /// <param name="fDuration">표시할 시간</param>
    public void ShowText(int fSpeechIdx, string fConvName, int fConvIdx, float fDuration)
    {
        if (mSpeechBubbleShow)
        {
            if (fSpeechIdx < 0 || fSpeechIdx >= mControlList.Count)
            {
                Debug.LogWarning(name + ".SpeechBubbleDirector.ShowText() " + "fSpeechIdx is out of Range");
                return;
            }

            ConversationFileControl convCon = null;
            if (mConvDic.TryGetValue(fConvName, out convCon))
            {
                if (fConvIdx < 0 || fConvIdx >= convCon.ConvsList.Count)
                {
                    Debug.LogWarning(name + ".SpeechBubbleDirector.ShowText() " + "fConvIdx is out of Range");
                    return;
                }

                mControlList[fSpeechIdx].ShowText(convCon.ConvsList[fConvIdx], fDuration);
            }
        }
    }

    public void ShowText(SpeechBubbleControl fCon, string fConvName, int fConvIdx, float fDuration)
    {
        if (mSpeechBubbleShow)
        {
            ConversationFileControl convCon = null;
            if (mConvDic.TryGetValue(fConvName, out convCon))
            {
                if (fConvIdx < 0 || fConvIdx >= convCon.ConvsList.Count)
                {
                    Debug.LogWarning(name + ".SpeechBubbleDirector.ShowText() " + "fConvIdx is out of Range");
                    return;
                }

                fCon.ShowText(convCon.ConvsList[fConvIdx], fDuration);
            }
            else
            {
                Debug.LogWarning("Couldn't find Convfile " + fConvName);
            }
        }
    }

    /// <summary>
    /// 지정하는 말풍선에 표시할 텍스트를 예약하는 함수
    /// </summary>
    /// <param name="fSec">예약할 시간</param>
    /// <param name="fCon">표시할 말풍선</param>
    /// <param name="fConvName">파일 이름</param>
    /// <param name="fConvIdx">표시할 말풍선 인덱스</param>
    /// <param name="fDuration">표시할 시간</param>
    public void ShowText(float fSec, SpeechBubbleControl fCon, string fConvName, int fConvIdx, float fDuration)
    {
        Debug.Log("Display Message after " + fSec);
        if (mSpeechBubbleShow)
        {
            StartCoroutine(ReserveShowText(fSec, fCon, fConvName, fConvIdx, fDuration));
        }
    }

    /// <summary>
    /// 지정하는 말풍선에 표시할 텍스트를 예약하는 함수
    /// </summary>
    /// <param name="fSec">예약할 시간</param>
    /// <param name="fSpeechIdx">표시할 말풍선의 인덱스</param>
    /// <param name="fConvName">대화 파일</param>
    /// <param name="fConvIndx">대사 인덱스</param>
    /// <param name="fDuration">표시할 시간</param>
    public void ShowText(float fSec, int fSpeechIdx, string fConvName, int fConvIdx, float fDuration)
    {
        if(mSpeechBubbleShow)
        {
            StartCoroutine(ReserveShowText(fSec, fSpeechIdx, fConvName, fConvIdx, fDuration));
        }
    }
    
    private IEnumerator ReserveShowText(float fSec, SpeechBubbleControl fCon, string fConvName, int fConvIdx, float fDuration)
    {
        yield return new WaitForSeconds(fSec);

        ShowText(fCon, fConvName, fConvIdx, fDuration);
    }
    
    private IEnumerator ReserveShowText(float fsec, int fSpeechIdx, string fConvName, int fConvIdx, float fDuration)
    {
        yield return new WaitForSeconds(fsec);

        ShowText(fSpeechIdx, fConvName, fConvIdx, fDuration);
    }
    #endregion
}
