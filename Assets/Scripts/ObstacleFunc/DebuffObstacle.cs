/*
 * 시간 감소 디버프 또는 미니게임 진입 유도기능을 수행하는 스크립트
 * - 기획서상 구현목표는 디버프, 미니게임 모두이지만 마감 시간상 디버프 우선적으로 구현
 */
using UnityEngine;

public class DebuffObstacle : BaseObstacleEffect
{
    [SerializeField] private bool isTriggerMiniGame = true;
    [SerializeField] private float fPenaltyTime = 30.0f;

    protected override void f_ApplyEffect(GameObject player)
    {
        if (isTriggerMiniGame)
        {
            Debug.Log("디버프: 미니게임 진입");
            GameManager.Instance?.f_OnHitObstacle();
        }
        else
        {
            Debug.Log("디버프: 시간 -" + fPenaltyTime + "초");
            GameManager.Instance?.f_AddTime(-fPenaltyTime);
        }
    }
}
