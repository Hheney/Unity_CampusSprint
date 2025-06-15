/*
 * [Ŭ������ �̵��� �����ϴ� ��Ʈ�ѷ�]
 * - ��ֹ��� �����ϰ� ���ó�� �÷��̾� �ݴ� �������� �̵�
 * - ���� �Ÿ� �������� ����� �ڵ� ����
 */
using UnityEngine;

public class ClearZoneController : MonoBehaviour
{
    [SerializeField] private float fSpeedMultiplier = 1.2f;     //��� �ӵ��� ������ ����
    [SerializeField] private float fDestroyX = -15.0f;          //���� ȭ�� �ۿ� ���� �� ����

    private float fMoveSpeed = 0.0f;     //���� �̵� �ӵ�
    private float fDirInput = 0.0f;      //�Է� ���� (-1, 0, 1)
    private float fDirection = 0.0f;     //�̵� ���� (-1 or 1)

    void Start()
    {
        if (BackgroundController.Instance != null)
        {
            fMoveSpeed = BackgroundController.Instance.BaseMoveSpeed * fSpeedMultiplier; //��� �ӵ��� ���� Ŭ������ �̵� �ӵ� ����
        }
    }

    void Update()
    {
        f_MoveClearZoneByInput(); 
    }

    /// <summary> �÷��̾� �Է¿� ���� �̵� ó�� </summary>
    void f_MoveClearZoneByInput()
    {
        //������ ���� ���� �ƴ� ��� Early return ó��
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameState.Running) { return; }

        fDirInput = Input.GetAxisRaw("Horizontal"); // -1, 0, 1(�÷��̾� �Է°�)

        if (fDirInput != 0) //�÷��̾ �¿� �Է��� ���� ���� �̵�
        {
            fDirection = -Mathf.Sign(fDirInput); //�÷��̾� �Է� �ݴ� �������� �̵�
            Vector3 vMove = Vector3.right * fDirection * fMoveSpeed * Time.deltaTime; //�̵��� ���� ���
            transform.position += vMove; //Ŭ������ �̵�
        }

        if (transform.position.x < fDestroyX)
        {
            Destroy(gameObject);
        }
    }
}