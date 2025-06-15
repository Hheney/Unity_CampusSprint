/*
 * 게임 시작시 GameManger에게 게임 시작여부를 알려주기 위한 스크립트
 * GameManger내에서는 Enum으로 지정된 상태에 따라 게임 상태를 관장함
 */
using UnityEngine;

public class GameStartTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance?.f_StartGame(); //GameManger가 null이 아닐경우 f_StartGame() 메소드 실행
    }
}
