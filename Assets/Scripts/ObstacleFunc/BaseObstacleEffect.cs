/*
 * [장애물 효과의 공통 기반 클래스]
 * 각 아이템(버프, 디버프) 자녀 클래스는 이 부모 클래스를 상속받아 효과를 구현함
 */
using UnityEngine;

public abstract class BaseObstacleEffect : MonoBehaviour 
{
    /// <summary> 플레이어와 충돌 시 발동되는 공통 처리 </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //플레이어와 충돌했을 때만 실행
        {
            f_ApplyEffect(collision.gameObject); //각 자식 클래스에서 구현한 효과 처리 메소드 호출
            Destroy(gameObject); //한 번 사용 후 파괴
        }
    }

    /// <summary> 각 효과 클래스에서 구현할 효과 처리 메소드 </summary>
    protected abstract void f_ApplyEffect(GameObject gPlayer);
}
