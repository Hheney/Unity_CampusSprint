/*
 * ���� ���۽� GameManger���� ���� ���ۿ��θ� �˷��ֱ� ���� ��ũ��Ʈ
 * GameManger�������� Enum���� ������ ���¿� ���� ���� ���¸� ������
 */
using UnityEngine;

public class GameStartTrigger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance?.f_StartGame(); //GameManger�� null�� �ƴҰ�� f_StartGame() �޼ҵ� ����
    }
}
