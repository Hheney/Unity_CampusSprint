/*
 * �ð� ���� ����� �Ǵ� �̴ϰ��� ���� ��������� �����ϴ� ��ũ��Ʈ
 * - ��ȹ���� ������ǥ�� �����, �̴ϰ��� ��������� ���� �ð��� ����� �켱������ ����
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
            Debug.Log("�����: �̴ϰ��� ����");
            GameManager.Instance?.f_OnHitObstacle();
        }
        else
        {
            Debug.Log("�����: �ð� -" + fPenaltyTime + "��");
            GameManager.Instance?.f_AddTime(-fPenaltyTime);
        }
    }
}
