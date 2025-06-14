/*
 * [플레이어 이동 및 점프 컨트롤 스크립트]
 * 플레이어는 x축 이동 없이 화면 중앙에 고정됨(배경이 움직이며 착시효과 유발)
 * 구조상 플레이어 자체는 스페이스바로 점프만 가능하며, 애니메이션과 착지 판정을 하는 기능을 수행함
 * - 이전 프로젝트인 ClimbCloud에서 개발한 스크립트를 재활용하여 작성함
 */
using System.Net.Sockets;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Unity 기본 접근 지정자가 private이지만 이 프로젝트에서는 가독성 및 명시를 위해 private 접근 지정자를 붙임

    [SerializeField] private float fJumpForce = 7f; //플레이어의 점프 힘 필드

    private Rigidbody2D m_rigidPlayer = null;   //Rigidbody2D 참조 필드
    private Animator m_animatorPlayer = null;   //Animator 참조 필드

    private bool isGround = false;      //바닥 착지 여부 Bool 필드

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_rigidPlayer = GetComponent<Rigidbody2D>();
        m_animatorPlayer = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        f_JumpPlayer();
        f_CallBackgroundMove();
    }

    void f_CallBackgroundMove()
    {
        /*
         * 프로젝트 ClimbCloud "f_PlayerMoveAxisX()"의 GetKeyDown 방식이 아닌 GetAxisRaw를 사용하여 로직 단축
         * GetAxis / GetAxisRaw : GetAxis는 보간기법이 사용되어 리턴값이 지속적으로 발생함, GetAxisRaw는 -1, 0, 1의 상태를 바로 반환함
         * 따라서 키값을 명확하고 빠르게 얻을 수 있도록 GetAxisRaw를 사용함
         */
        float fDirInput = Input.GetAxisRaw("Horizontal");
        BackgroundController.Instance?.f_MoveByInput(fDirInput); //BackgroundController가 null이 아닐경우 f_MoveByInput 실행

        if(fDirInput != 0) //-1, 1값이면 이동, 0이면 멈춤이므로 0이 아닌경우 bool값 변경
        {
            Vector3 vLocalScale = transform.localScale;
            vLocalScale.x = Mathf.Sign(fDirInput); //fDirInput 값이 -1일 경우 플레이어방향 왼쪽 전환
                                                   //fDirInput 값이 1일 경우 플레이어방향 오른쪽 전환
            transform.localScale = vLocalScale;

            m_animatorPlayer.SetBool("isRun", true);
        }
        else
        {
            m_animatorPlayer.SetBool("isRun", false);
        }
    }

    /// <summary> 점프 및 점프 애니메이션 제어 메소드 </summary>
    void f_JumpPlayer()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            m_rigidPlayer.AddForce(Vector2.up * fJumpForce, ForceMode2D.Impulse);
            m_animatorPlayer.SetBool("isJump", true);
            //SoundManager.Instance?.f_PlaySFX(SoundName.SFX_Jump, 0.1f);
        }
    }

    /// <summary> 바닥 착지 감지 처리 </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y > 0.5f)
            {
                isGround = true;
                m_animatorPlayer.SetBool("isJump", false);
                break;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false;
    }
}
