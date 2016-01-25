using UnityEngine;
using System.Collections;

/// <summary>
/// X 절댓값을 넘으면 오브젝트를 파괴한다
/// </summary>
public class DestroyXLimit : MonoBehaviour
{
    public float mXLimit = 0f;

	// Use this for initialization
	void Start ()
    {
        mXLimit = Mathf.Abs(mXLimit);
	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckXLimit();
	}

    private void CheckXLimit()
    {
        if(Mathf.Abs(transform.localPosition.x) >= mXLimit)
        {
            Destroy(gameObject);
        }
    }
}
