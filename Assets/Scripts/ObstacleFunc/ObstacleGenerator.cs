/*
 * [일정 간격으로 장애물을 생성하는 제너레이터 스크립트]
 * - 플레이어 기준 오른쪽에 장애물을 생성함
 * - 버프/디버프 중 랜덤 선택된 장애물 발생
 */
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [Header("생성 설정")]
    [SerializeField] private float fSpawnInterval = 2.0f;   //장애물 생성 간격 (초 단위)
    [SerializeField] private float fSpawnOffsetX = 10.0f;   //장애물 생성 위치 기준 플레이어 x좌표 오프셋 (플레이어 기준 오른쪽에 생성)
    [SerializeField] private float fSpawnPosY = -7.8f;  //장애물 생성 y좌표값

    [Header("장애물 프리팹")]
    [SerializeField] private GameObject gBuffPrefab = null; //버프 프리팹 (25% 확률로 생성)
    [SerializeField] private List<GameObject> gListDebuffPrefab = new List<GameObject>(); //디버프 프리팹 리스트 (75% 확률로 생성)

    [Header("플레이어(위치) 참조")]
    [SerializeField] private Transform gPlayer = null; //플레이어의 Transform 컴포넌트 참조 필드(장애물 생성 기준점)

    private float fTimer = 0.0f;    //장애물 생성 타이머
    private float fDirInput = 0.0f; // 플레이어 입력값 (-1, 0, 1)

    void Update()
    {
        //게임이 진행 중이 아닐 경우 Early Return 하여 하위 로직을 실행하지 않음
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameState.Running) { return; }

        fDirInput = Input.GetAxisRaw("Horizontal"); //GetAxisRaw는 -1, 0, 1값을 리턴함

        if (fDirInput != 0) //움직일 경우 생성로직 실행
        {
            fTimer += Time.deltaTime; //프레임당 시간 증가

            if (fTimer >= fSpawnInterval) //지정된 시간 간격이 지나면 장애물 생성
            {
                fTimer = 0.0f;
                f_GenerateRandomObstacle(); //장애물 생성 메소드 호출
            }
        }
    }

    /// <summary> 장애물 랜덤 생성 메소드 </summary>
    private void f_GenerateRandomObstacle()
    {
        GameObject gSelectObstacle = null; //선택된 장애물 프리팹을 저장할 변수

        /*
         * 랜덤 확률을 이용하여 버프와 디버프를 선택
         * 0.0 ~ 1.0 사이의 랜덤 값 생성
         */
        float fRandomValue = Random.value;

        if (fRandomValue < 0.25f) //25% 확률로 버프 프리팹 선택
        {
            gSelectObstacle = gBuffPrefab;
        }
        else //75% 확률로 디버프 프리팹 리스트에서 선택
        {
            int nSelectIndexNum = Random.Range(0, gListDebuffPrefab.Count); //디버프 프리팹 리스트에서 랜덤 인덱스 선택
            gSelectObstacle = gListDebuffPrefab[nSelectIndexNum];           //선택된 디버프 프리팹
        }

        Vector3 vSpawnPos = new Vector3(gPlayer.position.x + fSpawnOffsetX, fSpawnPosY, 0.0f); //장애물 생성 위치 계산 (플레이어 기준 오른쪽에 생성)
        Instantiate(gSelectObstacle, vSpawnPos, Quaternion.identity); //선택된 장애물 프리팹을 생성 위치에 생성
    }
}
