using UnityEngine;

public class ClearZoneGenerator : MonoBehaviour
{
    [Header("생성 조건")]
    [SerializeField] private int nRequiredLoopCount = 10;   //반복 횟수 기준
    [SerializeField] private float fSpawnOffsetX = 15.0f;   //플레이어 기준 X 오프셋
    [SerializeField] private float fSpawnPosY = -7.8f;      //고정 Y좌표

    [Header("프리팹 및 참조")]
    [SerializeField] private GameObject gClearZonePrefab = null;  //클리어존 프리팹
    [SerializeField] private Transform gPlayer = null;            //플레이어 위치 참조

    private bool isSpawned = false; //중복 생성 방지

    void Update()
    {
        //게임이 진행 중이 아닐 경우 Early Return 처리
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameState.Running) return;
        if (isSpawned) return; //이미 클리어존이 생성되었으면 Early Return


        // 클리어 조건 달성
        if (ClearCounterManager.Instance != null &&
            ClearCounterManager.Instance.LoopCount >= nRequiredLoopCount) //클리어 카운터 매니저가 null이 아니며, 루프 카운트가 조건(10회) 이상일 경우
        {
            isSpawned = true; //클리어존 생성 플래그 설정

            Vector3 vSpawnPos = new Vector3(gPlayer.position.x + fSpawnOffsetX, fSpawnPosY, 0.0f); //플레이어 위치 기준으로 X 오프셋을 적용한 클리어존 생성 위치
            Instantiate(gClearZonePrefab, vSpawnPos, Quaternion.identity); //클리어존 프리팹을 생성 위치에 생성

            Debug.Log($"[클리어존 생성] 위치: {vSpawnPos}");
        }
    }


}
