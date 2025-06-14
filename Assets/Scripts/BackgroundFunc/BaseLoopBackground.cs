/*
 * [��� �ݺ� ������Ʈ�� ���� ������ ������ �߻� Ŭ����]
 * ��� : fScrollWidth �ڵ� ��� gReferenceTarget ����
 * �ݺ� ��� ������ ���� Ŭ�������� �������̵�(�����)
 * �߻� Ŭ������ ����ؼ� �ݺ��Ǵ� �κ��� �����Ͽ� ������������ ����
*/

using UnityEngine;

public abstract class BaseLoopBackground : MonoBehaviour
{
    //���ǵ� ������ ��� ���� Ŭ�������� ��ӿ����̹Ƿ� protected ��������

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
            fScrollWidth = spriteRenderer.bounds.size.x; //SpriteRenderer�� bounds�� ����Ͽ� ���� ���̸� ���
        }
    }

    protected virtual void Update()
    {
        if (gReferenceTarget == null) return;   //�������� null�� ��� Early Return
                                                //���� ������ �����ȵǵ��� �ϱ� ����

        f_CheckAndLoop(); //�ݺ� ���� �Ǵ� �� �缳�� �޼ҵ� ȣ��
    }

    protected abstract void f_CheckAndLoop(); //���� Ŭ�������� �����ؾ� �ϴ� �߻� �޼ҵ�

    /// <summary> �ܺο��� ���� Ÿ�� ������ �� �ִ� �޼ҵ� </summary>
    public void f_SetReferenceTarget(Transform target)
    {
        gReferenceTarget = target;
    }
}
