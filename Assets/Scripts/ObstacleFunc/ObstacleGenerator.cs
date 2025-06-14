/*
 * [일정 간격으로 장애물을 생성하는 제너레이터 스크립트]
 * - 플레이어 기준 오른쪽에 장애물을 생성함
 * - 버프/디버프 중 랜덤 선택된 장애물 발생
 */
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [Header("생성 설정")]
    [SerializeField] private float fSpawnInterval = 2.0f;
    [SerializeField] private float fSpawnOffsetX = 10.0f;
    [SerializeField] private float fSpawnPosY = -7.8f;  //장애물 생성 y좌표값

    [Header("장애물 프리팹")]
    [SerializeField] private GameObject gBuffPrefab = null;
    [SerializeField] private GameObject gDebuffPrefab = null;

    [Header("플레이어(위치) 참조")]
    [SerializeField] private Transform gPlayer = null;

    private float fTimer = 0.0f;
    private float fDirInput = 0.0f;

    void Update()
    {
        if (gPlayer == null) return;

        fDirInput = Input.GetAxisRaw("Horizontal"); //GetAxisRaw는 -1, 0, 1값을 리턴함

        if (fDirInput != 0) //움직일 경우 생성로직 실행
        {
            fTimer += Time.deltaTime;

            if (fTimer >= fSpawnInterval)
            {
                fTimer = 0f;
                f_GenerateRandomObstacle();
            }
        }
    }

    /// <summary> 장애물 랜덤 생성 메소드 </summary>
    private void f_GenerateRandomObstacle()
    {
        int nRandNum = Random.Range(0, 2);
        GameObject gSelectObstacle = (nRandNum == 0) ? gBuffPrefab : gDebuffPrefab; //삼항조건연산자 사용

        Vector3 vSpawnPos = new Vector3(gPlayer.position.x + fSpawnOffsetX, fSpawnPosY, 0f);
        Instantiate(gSelectObstacle, vSpawnPos, Quaternion.identity);
    }
}
