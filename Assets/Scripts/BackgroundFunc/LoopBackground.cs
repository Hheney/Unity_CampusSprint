/*
 * X�� �������� �ݺ��Ǵ� �Ϲ����� ��� Ÿ��
 * ���� ��ǥ���� scrollWidth��ŭ �������� ����� ���������� �̵�
 */
using UnityEngine;

public class LoopBackground : BaseLoopBackground //BaseLoopBackground�� ��ӹ޾� ��� ���
{
    //��� ���̾ �����̹Ƿ�, �� ���̾ �ݺ��ɶ� ī��Ʈ�� �ߺ��ؼ� �����ϴ� ������ �߻���
    //����, �� ������Ʈ�� ī��Ʈ�� ����ϵ��� �����ϴ� ������ �߰�
    [SerializeField] private bool isMainCounter = false;

    protected override void f_CheckAndLoop()
    {
        //���� ��ġ �������� ����� �������� ����� ����� ���������� ���ġ
        if (transform.position.x + fScrollWidth < gReferenceTarget.position.x)
        {
            //ȭ�� ������ ����ٸ� ���������� 2�� �̵���Ŵ
            transform.position += Vector3.right * fScrollWidth * 2f;

            //���� ī���Ͱ� Ȱ��ȭ�� ��쿡�� ClearCounterManager�� ī��Ʈ ���� ��û
            if (isMainCounter && ClearCounterManager.Instance != null)
            {
                ClearCounterManager.Instance.f_AddCount();
            }
        }
    }
}
