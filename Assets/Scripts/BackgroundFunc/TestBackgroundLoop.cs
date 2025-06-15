/*
 * ��� ������Ʈ�� ���� �Ÿ� �̻� �̵����� �� �ݺ� ��ġ�� ���ġ�ǵ��� ó���ϴ� �ݺ� ��� ��ũ��Ʈ
 * BackgroundController�� �и��Ǿ� SRP(���� å�� ��Ģ)�� �������� �Ͽ� �ݺ�ó���� ����ϴ� ��ũ��Ʈ �� ���������� ����
 */
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [SerializeField] private Transform g_ReferenceTarget; //���� ��ġ(�÷��̾ ���������� ����)
    
    private SpriteRenderer spriteRenderer = null;
    private float fScrollWidth = 0.0f; //���̾��� ũ�⸦ ������ �ʵ�
                                       //(���� ���� ũ��� 40unit������ ����� �� �����Ƿ� ũ�� ������ �ڵ�ȭ�ϱ� ����)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SpriteRenderer ������Ʈ�� �����ͼ� SpriteRenderer ���� ���̷� fScrollWidth �ڵ� ���
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        fScrollWidth = spriteRenderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //���� ��ġ �������� ����� �������� ����� ����� ���������� ���ġ
        if (transform.position.x + fScrollWidth < g_ReferenceTarget.position.x)
        {
            //ȭ�� ������ ����ٸ� ���������� 2�� �̵���Ŵ
            transform.position += Vector3.right * fScrollWidth * 2.0f;
        }
    }
}
