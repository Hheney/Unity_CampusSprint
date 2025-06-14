/*
 * [장애물 오브젝트의 이동을 제어하는 컨트롤러]
 * - 플레이어 입력에 따라 배경처럼 장애물을 반대 방향으로 이동시킴
 */
using UnityEngine;


public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float fSpeedMultiplier = 1.2f;     //배경 속도에 가산할 값
    [SerializeField] private float fDestroyX = -15.0f;          //카메라 바깥 좌측 기준
    

    private float fMoveSpeed = 0.0f; //장애물을 이동시킬 속도
    float fDirInput = 0.0f;  // -1, 0, 1 중 하나 (플레이어 입력값)
    float fDirection = 0.0f;

    void Start()
    {
        if(BackgroundController.Instance != null)
        {
            fMoveSpeed = BackgroundController.Instance.BaseMoveSpeed * fSpeedMultiplier; //배경 속도에 가산하여 장애물 속도 설정
        }
    }

    void Update()
    {
        f_MoveObstacleByInput(); //플레이어 입력에 따라 장애물 이동 메소드 호출
    }


    void f_MoveObstacleByInput()
    {
        //게임이 진행 중이 아닐 경우 Early return 처리
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameState.Running) { return; }

        fDirInput = Input.GetAxisRaw("Horizontal"); // -1, 0, 1 중 하나

        //방향 입력이 있을 때만 이동
        if (fDirInput != 0)
        {
            fDirection = -Mathf.Sign(fDirInput); // -1 또는 1로 설정 (플레이어가 왼쪽으로 이동하면 -1, 오른쪽으로 이동하면 1)
            Vector3 vMove = Vector3.right * fDirection * fMoveSpeed * Time.deltaTime; //이동할 벡터 계산
            transform.position += vMove; //장애물 이동
        }

        if (transform.position.x < fDestroyX) //장애물이 화면 왼쪽을 벗어나면 제거
        {
            Destroy(gameObject);
        }
    }
}
