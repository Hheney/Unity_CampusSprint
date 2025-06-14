/*
 * [��ֹ� ������Ʈ�� �̵��� �����ϴ� ��Ʈ�ѷ�]
 * - �÷��̾� �Է¿� ���� ���ó�� ��ֹ��� �ݴ� �������� �̵���Ŵ
 */
using UnityEngine;


public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float fSpeedMultiplier = 10.0f;     //��� �ӵ��� ������ ��
    [SerializeField] private float fDestroyX = -15.0f;          //ī�޶� �ٱ� ���� ����
    

    private float fMoveSpeed = 0.0f; //��ֹ��� �̵���ų �ӵ�
    float fDirInput = 0.0f;
    float fDirection = 0.0f;

    void Start()
    {
        if(BackgroundController.Instance != null)
        {
            fMoveSpeed = BackgroundController.Instance.BaseMoveSpeed * fSpeedMultiplier;
        }
            
    }

    void Update()
    {
        fDirInput = Input.GetAxisRaw("Horizontal"); // -1, 0, 1 �� �ϳ�

        //���� �Է��� ���� ���� �̵�
        if (fDirInput != 0)
        {
            fDirection = -Mathf.Sign(fDirInput); // �ݴ� ���� �̵�
            Vector3 vMove = Vector3.right * fDirection * fMoveSpeed * Time.deltaTime;
            transform.position += vMove;
        }

        if(transform.position.x < fDestroyX)
        {
            Destroy(gameObject); //���� ȭ���� ����� ������Ʈ ����
        }
    }
}
