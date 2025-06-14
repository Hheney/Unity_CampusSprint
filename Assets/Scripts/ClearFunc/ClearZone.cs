/*
 * 플레이어가 오브젝트에 닿으면 GameClear 처리하는 클래스
 */
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //태그가 "Player"인 경우만 처리
        {
            Debug.Log("클리어존 도달 - 게임 클리어!");
            GameManager.Instance?.f_OnGameClear(); //GameManager에 클리어 처리 요청
        }
    }
}
