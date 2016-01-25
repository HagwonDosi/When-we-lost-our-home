using UnityEngine;
using System.Collections;

/*
 * 이 스크립트는 주어진 문자열을 mLabel에 정해진 간격으로 천천히 띄워주는 역할을 한다.
*/
public class ShowTextSlowly : MonoBehaviour
{
    public UILabel mLabel = null;
    public bool mCanBeUsed = true;      // 현재 사용가능한가?
    public float mSecPerLetter = 0.05f; // 글자당 초 수

    private string mStrToShow = "NewLabel"; // 보여줄 전체 문자열

    /*
      * 보여주게 될 전체 문자열을 다 보여주었을 때의 크기를 알아오는 함수
     */
    public Vector2 getFullSize(string fStr)
    {
        string ori = mLabel.text;

        mLabel.text = fStr;
        mLabel.GetComponent<UIWidget>().MakePixelPerfect();
        var size = mLabel.localSize;

        mLabel.text = ori;
        mLabel.GetComponent<UIWidget>().MakePixelPerfect();

        return size;
    }

    public void setNewString(string fStr)
    {
        if (mCanBeUsed)
        {
            mCanBeUsed = false;
            mStrToShow = fStr;
            StartCoroutine(UpdateShowingString());
        }
        else
        {
            Debug.LogWarning("ShowTextSlowly at " + name + " Can't be Used.");
        }
    }

    public void ClearText()
    {
        mLabel.text = "";
    }

    IEnumerator UpdateShowingString()
    {
        int curIdx = 0;
        float curTime = 0f;

        while (true)
        {
            //만약 모든 문자를 표시했다면
            if (curIdx >= mStrToShow.Length)
            {
                mCanBeUsed = true;
                StopCoroutine(UpdateShowingString());
                break;
            }
            else
            {
                if (curTime >= mSecPerLetter)
                {
                    curTime -= mSecPerLetter;
                    curIdx += 1;

                    string outputStr = new string(mStrToShow.ToCharArray(), 0, curIdx);

                    mLabel.text = outputStr;
                    mLabel.GetComponent<UIWidget>().MakePixelPerfect();
                }
                else
                {
                    curTime += Time.deltaTime;
                }
            }

            yield return null;
        }
    }
}