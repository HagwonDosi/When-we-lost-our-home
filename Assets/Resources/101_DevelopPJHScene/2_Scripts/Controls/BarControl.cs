using UnityEngine;
using System.Collections;

/*
 * 이 스크립트는 반드시 UISprite 스크립트를 갖고 있는 오브젝트와 같은 오브젝트에 놓을 것 해당 UISprite는 Bar로서 기능하게 됨
*/
public class BarControl : MonoBehaviour
{
    //수직Bar로 할 것인가
    public bool mVertical = false;
    public float mMaxValue = 0f;

    protected float mCurValue = 0f;

    private UISprite mSprite = null;

	// Use this for initialization
	protected void Start ()
    {
        mSprite = GetComponent<UISprite>();

        StartCoroutine(UpdateBar());
	}
	
    IEnumerator UpdateBar()
    {
        Vector3 oriScale = transform.localScale;

        while(true)
        {

            if(mVertical)
            {
                transform.localScale = new Vector3(oriScale.x, oriScale.y * (mCurValue / mMaxValue), oriScale.z);
            }
            else
            {
                transform.localScale = new Vector3(oriScale.x * (mCurValue / mMaxValue), oriScale.y, oriScale.z);
            }

            yield return null;
        }
    }
}
