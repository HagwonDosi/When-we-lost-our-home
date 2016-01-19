using UnityEngine;
using System.Collections;

/// <summary>
/// Enemy의 대부분을 담당하는 클래스
/// </summary>
public class EnemyControl : MonoBehaviour
{
    #region Variables
    public GameObject mPlayer = null;
    public EnemyStatus eState;
    public bool mAttack = false;
    public float mSpeed = 10f;
    public float mMaxSpeed = 10f;

    private Rigidbody mRB = null;
    private float mEnemyX;
    private Animator mAnimator;
    private float enemy_speed;
    /// <summary>
    /// 플레이어를 발견했는가
    /// </summary>
    private bool mTarget = false;
    #endregion

    #region get/setter
    public bool Target
    {
        get
        {
            return mTarget;
        }
        set
        {
            if(value == false)
            {
                mRB.velocity = Vector3.zero;
            }

            mTarget = value;
        }
    }

    #endregion

    #region VirtualFunctions
    // Use this for initialization
    void Start ()
    {
        mRB = GetComponent<Rigidbody>();
        mAnimator = gameObject.GetComponent<Animator>();
        mEnemyX = transform.localPosition.x;
        StartCoroutine(UpdateSpeed());
	}
    #endregion

    #region CustomFunctions
    IEnumerator UpdateSpeed()
    {
        while (true)
        {
            float playerX = mPlayer.transform.localPosition.x;
            float enemyX = transform.localPosition.x;
            mAnimator.SetFloat("M_Speed", Mathf.Abs((float)CustomMath.CustomRound(4, mRB.velocity.x)));
            
            if (mTarget == true)
            {
                int direction = 1;

                if(playerX < enemyX)
                {
                    direction *= -1;
                }

                if(Mathf.Abs(mRB.velocity.x) <= mMaxSpeed)
                {
                    mRB.AddForce(Vector3.right * direction * mSpeed * Time.deltaTime * 62.5f);
                }

                /*enemy_speed = 0.4f;
                float distance = Mathf.Abs(mEnemyX - playerX);
                if (distance < mDetectionDistance) // 탐지
                {
                    _animator.SetBool("M_Walk", true);
                    _animator.SetBool("Monster_Attack_chek", false);

                    transform.localPosition = new Vector3(mEnemyX -= enemy_speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);//타겟안에 들어오면 플레이어쪽으로 감
                    if (Gun == true)
                    {
                        transform.localPosition = new Vector3(mEnemyX += 0.05f, transform.localPosition.y, transform.localPosition.z);//총에 맞으면 뒤로 밀림
                        Gun = false;
                    }
                   
                    
                }
                if (mEnemyX - 0.1f < (playerX + 0.8f)) // 공격
                {
                    enemy_speed = 0;
                    attack = true;
                    if (attack == true)
                    {
                        _animator.SetBool("M_Walk", false);
                        _animator.SetBool("Monster_Attack_chek", true);
                        _animator.speed = 0.5f; // 애니메이션 스피드 변경
                        if (Gun == true)
                        {
                            transform.localPosition = new Vector3(mEnemyX += 0.05f, transform.localPosition.y, transform.localPosition.z);//총에 맞으면 뒤로 밀림
                            Gun = false;
                        }
                        //공격에니메이션 적용
                    }
                }
                
                
                if (mEnemyX < (playerX - 0.8f))
                {
                    _animator.SetBool("M_Walk", true);
                    _animator.SetBool("Monster_Attack_chek", false);

                    transform.localPosition = new Vector3(mEnemyX += enemy_speed * Time.deltaTime, transform.localPosition.y, transform.localPosition.z);//타겟안에 들어오면 플레이어쪽으로 감

                    if (Gun == true)
                    {
                        transform.localPosition = new Vector3(mEnemyX -= 0.05f, transform.localPosition.y, transform.localPosition.z);//총에 맞으면 뒤로 밀림
                        Gun = false;
                    }
                    if (mEnemyX + 0.1f > (playerX - 0.8f))
                    {
                        attack = true;
                        if (attack == true)
                        {
                            //공격에니메이션 적용
                            _animator.SetBool("M_Walk", false);
                            _animator.SetBool("Monster_Attack_chek", true);
                            _animator.speed = 0.5f;
                            if (Gun == true)
                            {
                                transform.localPosition = new Vector3(mEnemyX += 0.05f, transform.localPosition.y, transform.localPosition.z);//총에 맞으면 뒤로 밀림
                                Gun = false;
                            }
                        }
                    }
                   
                }*/
            }

            yield return null;
        }
    }
    #endregion
}
