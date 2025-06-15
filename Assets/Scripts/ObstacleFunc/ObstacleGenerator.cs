/*
 * [���� �������� ��ֹ��� �����ϴ� ���ʷ����� ��ũ��Ʈ]
 * - �÷��̾� ���� �����ʿ� ��ֹ��� ������
 * - ����/����� �� ���� ���õ� ��ֹ� �߻�
 */
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] private float fSpawnInterval = 2.0f;   //��ֹ� ���� ���� (�� ����)
    [SerializeField] private float fSpawnOffsetX = 10.0f;   //��ֹ� ���� ��ġ ���� �÷��̾� x��ǥ ������ (�÷��̾� ���� �����ʿ� ����)
    [SerializeField] private float fSpawnPosY = -7.8f;  //��ֹ� ���� y��ǥ��

    [Header("��ֹ� ������")]
    [SerializeField] private GameObject gBuffPrefab = null; //���� ������ (25% Ȯ���� ����)
    [SerializeField] private List<GameObject> gListDebuffPrefab = new List<GameObject>(); //����� ������ ����Ʈ (75% Ȯ���� ����)

    [Header("�÷��̾�(��ġ) ����")]
    [SerializeField] private Transform gPlayer = null; //�÷��̾��� Transform ������Ʈ ���� �ʵ�(��ֹ� ���� ������)

    private float fTimer = 0.0f;    //��ֹ� ���� Ÿ�̸�
    private float fDirInput = 0.0f; // �÷��̾� �Է°� (-1, 0, 1)

    void Update()
    {
        //������ ���� ���� �ƴ� ��� Early Return �Ͽ� ���� ������ �������� ����
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameState.Running) { return; }

        fDirInput = Input.GetAxisRaw("Horizontal"); //GetAxisRaw�� -1, 0, 1���� ������

        if (fDirInput != 0) //������ ��� �������� ����
        {
            fTimer += Time.deltaTime; //�����Ӵ� �ð� ����

            if (fTimer >= fSpawnInterval) //������ �ð� ������ ������ ��ֹ� ����
            {
                fTimer = 0.0f;
                f_GenerateRandomObstacle(); //��ֹ� ���� �޼ҵ� ȣ��
            }
        }
    }

    /// <summary> ��ֹ� ���� ���� �޼ҵ� </summary>
    private void f_GenerateRandomObstacle()
    {
        GameObject gSelectObstacle = null; //���õ� ��ֹ� �������� ������ ����

        /*
         * ���� Ȯ���� �̿��Ͽ� ������ ������� ����
         * 0.0 ~ 1.0 ������ ���� �� ����
         */
        float fRandomValue = Random.value;

        if (fRandomValue < 0.25f) //25% Ȯ���� ���� ������ ����
        {
            gSelectObstacle = gBuffPrefab;
        }
        else //75% Ȯ���� ����� ������ ����Ʈ���� ����
        {
            int nSelectIndexNum = Random.Range(0, gListDebuffPrefab.Count); //����� ������ ����Ʈ���� ���� �ε��� ����
            gSelectObstacle = gListDebuffPrefab[nSelectIndexNum];           //���õ� ����� ������
        }

        Vector3 vSpawnPos = new Vector3(gPlayer.position.x + fSpawnOffsetX, fSpawnPosY, 0.0f); //��ֹ� ���� ��ġ ��� (�÷��̾� ���� �����ʿ� ����)
        Instantiate(gSelectObstacle, vSpawnPos, Quaternion.identity); //���õ� ��ֹ� �������� ���� ��ġ�� ����
    }
}
