/*
 * �ð� ������ �����ϴ� ��ֹ� ��� ��ũ��Ʈ 
 * - �÷��̾ �浹�� +5���� �߰��ð��� �����Ѵ�.
 */

using UnityEngine;

public class BuffObstacle : BaseObstacleEffect
{
    [SerializeField] private float fBonusTime = 5.0f; //�߰��ð� �ʵ�

    protected override void f_ApplyEffect(GameObject player) //�÷��̾�� �浹 �� ȣ��Ǵ� �޼ҵ�
    {
        Debug.Log("���� ȹ��! �ð� +" + fBonusTime + "��"); //����� �α� ���
        GameManager.Instance?.f_AddTime(fBonusTime); //GameManager�� f_AddTime �޼ҵ带 ȣ���Ͽ� �ð� �߰�
    }
}
