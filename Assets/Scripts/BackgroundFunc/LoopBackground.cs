/*
 * X�� �������� �ݺ��Ǵ� �Ϲ����� ��� Ÿ��
 * ���� ��ǥ���� scrollWidth��ŭ �������� ����� ���������� �̵�
 */
using UnityEngine;

public class LoopBackground : BaseLoopBackground //BaseLoopBackground�� ��ӹ޾� ��� ���
{
    protected override void f_CheckAndLoop()
    {
        //���� ��ġ �������� ����� �������� ����� ����� ���������� ���ġ
        if (transform.position.x + fScrollWidth < gReferenceTarget.position.x)
        {
            //ȭ�� ������ ����ٸ� ���������� 2�� �̵���Ŵ
            transform.position += Vector3.right * fScrollWidth * 2f;
        }
    }
}
