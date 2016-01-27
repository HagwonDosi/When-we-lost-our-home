using UnityEngine;
using System.Collections;

public class Enemy_Hp : BarControl
{

    public EnemyStatus mEnemy = null;

    // Use this for initialization
    void Start() {
        mMaxValue = 400f;
        mCurValue = mEnemy.HP;

        StartCoroutine(UpdateCurVal());

        base.Start();
	}

    IEnumerator UpdateCurVal()
    {
        while(true)
        {
            mCurValue = mEnemy.HP;

            yield return null;
        }
    }
}
