/*
 * �÷��̾ ������Ʈ�� ������ GameClear ó���ϴ� Ŭ����
 */
using UnityEngine;

public class ClearZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //�±װ� "Player"�� ��츸 ó��
        {
            Debug.Log("Ŭ������ ���� - ���� Ŭ����!");
            GameManager.Instance?.f_OnGameClear(); //GameManager�� Ŭ���� ó�� ��û
        }
    }
}
