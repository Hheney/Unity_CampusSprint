/*
 * [클리어존 이동을 제어하는 컨트롤러]
 * - 장애물과 동일하게 배경처럼 플레이어 반대 방향으로 이동
 * - 일정 거리 왼쪽으로 벗어나면 자동 제거
 */
using UnityEngine;

public class ClearZoneController : MonoBehaviour
{
    [SerializeField] private float fSpeedMultiplier = 1.2f;     //배경 속도에 곱해줄 배율
    [SerializeField] private float fDestroyX = -15.0f;          //왼쪽 화면 밖에 도달 시 제거

    private float fMoveSpeed = 0.0f;     //실제 이동 속도
    private float fDirInput = 0.0f;      //입력 방향 (-1, 0, 1)
    private float fDirection = 0.0f;     //이동 방향 (-1 or 1)

    void Start()
    {
        if (BackgroundController.Instance != null)
        {
            fMoveSpeed = BackgroundController.Instance.BaseMoveSpeed * fSpeedMultiplier; //배경 속도에 곱해 클리어존 이동 속도 설정
        }
    }

    void Update()
    {
        f_MoveClearZoneByInput(); 
    }

    /// <summary> 플레이어 입력에 따라 이동 처리 </summary>
    void f_MoveClearZoneByInput()
    {
        //게임이 진행 중이 아닐 경우 Early return 처리
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameState.Running) { return; }

        fDirInput = Input.GetAxisRaw("Horizontal"); // -1, 0, 1(플레이어 입력값)

        if (fDirInput != 0) //플레이어가 좌우 입력을 했을 때만 이동
        {
            fDirection = -Mathf.Sign(fDirInput); //플레이어 입력 반대 방향으로 이동
            Vector3 vMove = Vector3.right * fDirection * fMoveSpeed * Time.deltaTime; //이동할 벡터 계산
            transform.position += vMove; //클리어존 이동
        }

        if (transform.position.x < fDestroyX)
        {
            Destroy(gameObject);
        }
    }
}