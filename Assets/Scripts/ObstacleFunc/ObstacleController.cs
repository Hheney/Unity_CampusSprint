/*
 * [��ֹ� ������Ʈ�� �̵��� �����ϴ� ��Ʈ�ѷ�]
 * - �÷��̾� �Է¿� ���� ���ó�� ��ֹ��� �ݴ� �������� �̵���Ŵ
 */
using UnityEngine;


public class ObstacleController : MonoBehaviour
{
    [SerializeField] private float fSpeedMultiplier = 1.2f;     //��� �ӵ��� ������ ��
    [SerializeField] private float fDestroyX = -15.0f;          //ī�޶� �ٱ� ���� ����
    

    private float fMoveSpeed = 0.0f; //��ֹ��� �̵���ų �ӵ�
    float fDirInput = 0.0f;  // -1, 0, 1 �� �ϳ� (�÷��̾� �Է°�)
    float fDirection = 0.0f;

    void Start()
    {
        if(BackgroundController.Instance != null)
        {
            fMoveSpeed = BackgroundController.Instance.BaseMoveSpeed * fSpeedMultiplier; //��� �ӵ��� �����Ͽ� ��ֹ� �ӵ� ����
        }
    }

    void Update()
    {
        f_MoveObstacleByInput(); //�÷��̾� �Է¿� ���� ��ֹ� �̵� �޼ҵ� ȣ��
    }


    void f_MoveObstacleByInput()
    {
        //������ ���� ���� �ƴ� ��� Early return ó��
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameState.Running) { return; }

        fDirInput = Input.GetAxisRaw("Horizontal"); // -1, 0, 1 �� �ϳ�

        //���� �Է��� ���� ���� �̵�
        if (fDirInput != 0)
        {
            fDirection = -Mathf.Sign(fDirInput); // -1 �Ǵ� 1�� ���� (�÷��̾ �������� �̵��ϸ� -1, ���������� �̵��ϸ� 1)
            Vector3 vMove = Vector3.right * fDirection * fMoveSpeed * Time.deltaTime; //�̵��� ���� ���
            transform.position += vMove; //��ֹ� �̵�
        }

        if (transform.position.x < fDestroyX) //��ֹ��� ȭ�� ������ ����� ����
        {
            Destroy(gameObject);
        }
    }
}
