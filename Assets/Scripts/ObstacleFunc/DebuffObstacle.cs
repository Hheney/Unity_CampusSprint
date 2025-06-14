/*
 * �ð� ���� ����� �Ǵ� �̴ϰ��� ���� ��������� �����ϴ� ��ũ��Ʈ
 * - ��ȹ���� ������ǥ�� �����, �̴ϰ��� ��������� ���� �ð��� ����� �켱������ ����
 */
using UnityEngine;

public class DebuffObstacle : BaseObstacleEffect
{
    [SerializeField] private bool isTriggerMiniGame = true; //�̴ϰ��� ���� ����
    [SerializeField] private float fPenaltyTime = 30.0f;    //������ �ð�(�� ����)

    protected override void f_ApplyEffect(GameObject player) //�÷��̾�� �浹 �� ȣ��Ǵ� �޼ҵ�
    {
        if (isTriggerMiniGame) //�̴ϰ��� ���� ����(Inspector���� ���� ����)
        {
            Debug.Log("�����: �̴ϰ��� ����");
            GameManager.Instance?.f_OnHitObstacle(); //GameManager�� f_OnHitObstacle �޼ҵ带 ȣ���Ͽ� �̴ϰ��� ����
        }
        else
        {
            Debug.Log("�����: �ð� -" + fPenaltyTime + "��"); //����� �α� ���
            GameManager.Instance?.f_SubtractTime(fPenaltyTime); //GameManager�� f_SubtractTime �޼ҵ带 ȣ���Ͽ� �ð� ����
        }
    }
}
