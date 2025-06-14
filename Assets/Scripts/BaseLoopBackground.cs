/*
 * ��� �ݺ� ������Ʈ�� ���� ������ ������ �߻� Ŭ����
 * scrollWidth �ڵ� ���
 * referenceTarget ����
 * �ݺ� ������ ���� Ŭ�������� �������̵�(�����)
*/

using UnityEngine;

public abstract class BaseLoopBackground : MonoBehaviour
{
    [SerializeField] protected Transform gReferenceTarget; //������ �Ǵ� Ÿ��(�÷��̾�)�� Transform ������Ʈ �ʵ�

    protected SpriteRenderer spriteRenderer = null;
    protected float fScrollWidth = 0.0f;    //���̾��� ũ�⸦ ������ �ʵ�
                                            //(���� ���� ũ��� 40unit������ ����� �� �����Ƿ� ũ�� ������ �ڵ�ȭ�ϱ� ����)

    protected virtual void Awake()
    {
        //SpriteRenderer ������Ʈ�� �����ͼ� SpriteRenderer ���� ���̷� fScrollWidth �ڵ� ���
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            fScrollWidth = spriteRenderer.bounds.size.x;
        }
    }

    protected virtual void Update()
    {
        if (gReferenceTarget == null) return;   //�������� null�� ��� Early Return
                                                //���� ������ �����ȵǵ��� �ϱ� ����

        f_CheckAndLoop();
    }

    /// <summary> �ݺ� ���� �Ǵ� �� �缳�� �޼ҵ�(�߻�ȭ �޼ҵ�, ������ ���� Ŭ�������� ������) </summary>
    protected abstract void f_CheckAndLoop();

    /// <summary> �ܺο��� ���� Ÿ�� ������ �� �ִ� �޼ҵ� </summary>
    public void f_SetReferenceTarget(Transform target)
    {
        gReferenceTarget = target;
    }
}
