/*
 * 시간 감소 디버프 또는 미니게임 진입 유도기능을 수행하는 스크립트
 * - 기획서상 구현목표는 디버프, 미니게임 모두이지만 마감 시간상 디버프 우선적으로 구현
 */
using UnityEngine;

public class DebuffObstacle : BaseObstacleEffect
{
    [SerializeField] private bool isTriggerMiniGame = true; //미니게임 진입 여부
    [SerializeField] private float fPenaltyTime = 30.0f;    //감소할 시간(초 단위)

    protected override void f_ApplyEffect(GameObject player) //플레이어와 충돌 시 호출되는 메소드
    {
        if (isTriggerMiniGame) //미니게임 진입 여부(Inspector에서 설정 가능)
        {
            Debug.Log("디버프: 미니게임 진입");
            GameManager.Instance?.f_OnHitObstacle(); //GameManager의 f_OnHitObstacle 메소드를 호출하여 미니게임 진입
        }
        else
        {
            Debug.Log("디버프: 시간 -" + fPenaltyTime + "초"); //디버그 로그 출력
            GameManager.Instance?.f_SubtractTime(fPenaltyTime); //GameManager의 f_SubtractTime 메소드를 호출하여 시간 감소
        }
    }
}
