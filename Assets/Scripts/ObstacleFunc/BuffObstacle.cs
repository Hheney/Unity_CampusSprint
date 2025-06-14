/*
 * �ð� ������ �����ϴ� ��ֹ� ��� ��ũ��Ʈ 
 * - �÷��̾ �浹�� +5���� �߰��ð��� �����Ѵ�.
 */

using UnityEngine;

public class BuffObstacle : BaseObstacleEffect
{
    [SerializeField] private float fBonusTime = 5.0f; //�߰��ð� �ʵ�

    protected override void f_ApplyEffect(GameObject player)
    {
        Debug.Log("���� ȹ��! �ð� +" + fBonusTime + "��");
        GameManager.Instance?.f_AddTime(fBonusTime);
    }
}
