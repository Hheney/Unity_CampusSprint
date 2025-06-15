/*
 * [��ֹ� ȿ���� ���� ��� Ŭ����]
 * �� ������(����, �����) �ڳ� Ŭ������ �� �θ� Ŭ������ ��ӹ޾� ȿ���� ������
 */
using UnityEngine;

public abstract class BaseObstacleEffect : MonoBehaviour 
{
    /// <summary> �÷��̾�� �浹 �� �ߵ��Ǵ� ���� ó�� </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //�÷��̾�� �浹���� ���� ����
        {
            f_ApplyEffect(collision.gameObject); //�� �ڽ� Ŭ�������� ������ ȿ�� ó�� �޼ҵ� ȣ��
            Destroy(gameObject); //�� �� ��� �� �ı�
        }
    }

    /// <summary> �� ȿ�� Ŭ�������� ������ ȿ�� ó�� �޼ҵ� </summary>
    protected abstract void f_ApplyEffect(GameObject gPlayer);
}
