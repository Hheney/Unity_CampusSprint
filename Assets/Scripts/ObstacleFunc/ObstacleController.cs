/*
 * [장애물 오브젝트의 이동을 제어하는 컨트롤러]
 * - 플레이어 입력에 따라 배경처럼 장애물을 반대 방향으로 이동시킴
 */
using UnityEngine;


public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float fSpeedMultiplier = 10.0f;     //배경 속도에 가산할 값
    [SerializeField] private float fDestroyX = -15.0f;          //카메라 바깥 좌측 기준
    

    private float fMoveSpeed = 0.0f; //장애물을 이동시킬 속도
    float fDirInput = 0.0f;
    float fDirection = 0.0f;

    void Start()
    {
        if(BackgroundController.Instance != null)
        {
            fMoveSpeed = BackgroundController.Instance.BaseMoveSpeed * fSpeedMultiplier;
        }
            
    }

    void Update()
    {
        fDirInput = Input.GetAxisRaw("Horizontal"); // -1, 0, 1 중 하나

        //방향 입력이 있을 때만 이동
        if (fDirInput != 0)
        {
            fDirection = -Mathf.Sign(fDirInput); // 반대 방향 이동
            Vector3 vMove = Vector3.right * fDirection * fMoveSpeed * Time.deltaTime;
            transform.position += vMove;
        }

        if(transform.position.x < fDestroyX)
        {
            Destroy(gameObject); //게임 화면을 벗어나면 오브젝트 제거
        }
    }
}
