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

    [SerializeField] private float fJumpForce = 10.0f;              //플레이어의 점프 힘 필드
    [SerializeField] private float fFallGravityMultiplier = 2.5f;   //플레이어의 낙하가속도 필드

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
        f_JumpPlayer();         //점프 메소드 호출
        f_CallBackgroundMove(); //배경 이동 메소드 호출
        f_ApplyFallGravity();   //낙하 중일 때 낙하 속도 증가 메소드 호출
    }

    void f_CallBackgroundMove()
    {
        //미니게임 중에는 이동하지 않도록 Early return 처리함, 해당 경우 아래 로직을 실행하지 않음
        if (GameManager.Instance != null && GameManager.Instance.CurrentState == GameState.MiniGame) { return; }

        /*
         * 프로젝트 ClimbCloud "f_PlayerMoveAxisX()"의 GetKeyDown 방식이 아닌 GetAxisRaw를 사용하여 로직 단축
         * GetAxis / GetAxisRaw : GetAxis는 보간기법이 사용되어 리턴값이 지속적으로 발생함, GetAxisRaw는 -1, 0, 1의 상태를 바로 반환함
         * 따라서 키값을 명확하고 빠르게 얻을 수 있도록 GetAxisRaw를 사용함
         */
        float fDirInput = Input.GetAxisRaw("Horizontal"); // -1, 0, 1 중 하나의 값 리턴
        BackgroundController.Instance?.f_MoveByInput(fDirInput); //BackgroundController가 null이 아닐경우 f_MoveByInput 실행

        if(fDirInput != 0) //-1, 1값이면 이동, 0이면 멈춤이므로 0이 아닌경우 bool값 변경
        {
            Vector3 vLocalScale = transform.localScale;     //플레이어의 로컬 스케일을 가져옴
            vLocalScale.x = Mathf.Sign(fDirInput);          //fDirInput 값이 -1일 경우 플레이어방향 왼쪽 전환
                                                            //fDirInput 값이 1일 경우 플레이어방향 오른쪽 전환
            transform.localScale = vLocalScale;             //플레이어의 로컬 스케일을 변경하여 방향 전환

            m_animatorPlayer.SetBool("isRun", true); //isRun 파라미터를 true로 설정하여 달리는 애니메이션 실행
        }
        else
        {
            m_animatorPlayer.SetBool("isRun", false); //isRun 파라미터를 false로 설정하여 달리는 애니메이션 중지
        }
    }

    /// <summary> 점프 및 점프 애니메이션 제어 메소드 </summary>
    void f_JumpPlayer()
    {
        //게임 상태가 Running이 아닐 경우 Early return 처리하여 점프 기능을 비활성화함
        if (GameManager.Instance != null && GameManager.Instance.CurrentState != GameState.Running) { return; }

        if (Input.GetKeyDown(KeyCode.Space) && isGround) //스페이스바를 눌렀고 바닥에 있을 때
        {
            m_rigidPlayer.AddForce(Vector2.up * fJumpForce, ForceMode2D.Impulse); //플레이어에게 위쪽으로 힘을 가함
            m_animatorPlayer.SetBool("isJump", true); //isJump 파라미터를 true로 설정하여 점프 애니메이션 실행
            
            SoundManager.Instance?.f_PlaySFX(SoundName.SFX_Jump, 1.0f); //점프 효과음 재생
        }
    }

    /// <summary> 낙하 중일 때 낙하 속도 증가 처리 메소드 </summary>
    void f_ApplyFallGravity()
    {
        if (m_rigidPlayer.linearVelocity.y < 0) //떨어지고 있을 때
        {
            //낙하 중일 때 중력을 증가시켜 낙하 속도를 빠르게 변경
            m_rigidPlayer.linearVelocity += Vector2.up * Physics2D.gravity.y * (fFallGravityMultiplier - 1) * Time.deltaTime;
        }
    }

    /// <summary> 바닥 착지 감지 처리 </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        //이전 ClimbCloud 프로젝트의 착지 감지 로직의 문제점을 개선하기 위해 ContactPoint2D를 사용하여 충돌한 접촉점의 법선 벡터를 확인함
        foreach (ContactPoint2D contact in collision.contacts) //foreach문을 사용해 충돌한 모든 접촉점에 대해 반복 실행
        {
            if (contact.normal.y > 0.5f) //접촉점의 법선 벡터가 위쪽을 향하고 있을 때 (바닥에 착지한 경우)
            {
                isGround = true; //바닥에 착지값 true로 변경
                m_animatorPlayer.SetBool("isJump", false); //isJump 파라미터를 false로 설정하여 점프 애니메이션 중지
                break;
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGround = false; //바닥에서 벗어났을 때 바닥 착지값 false로 변경
    }
}
