/*
 * 시간 버프를 제공하는 장애물 기능 스크립트 
 * - 플레이어가 충돌시 +5초의 추가시간을 제공한다.
 */

using UnityEngine;

public class BuffObstacle : BaseObstacleEffect
{
    [SerializeField] private float fBonusTime = 5.0f; //추가시간 필드

    protected override void f_ApplyEffect(GameObject player) //플레이어와 충돌 시 호출되는 메소드
    {
        Debug.Log("버프 획득! 시간 +" + fBonusTime + "초"); //디버그 로그 출력
        GameManager.Instance?.f_AddTime(fBonusTime); //GameManager의 f_AddTime 메소드를 호출하여 시간 추가
    }
}
