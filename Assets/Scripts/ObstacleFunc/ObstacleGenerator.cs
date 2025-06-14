/*
 * [���� �������� ��ֹ��� �����ϴ� ���ʷ����� ��ũ��Ʈ]
 * - �÷��̾� ���� �����ʿ� ��ֹ��� ������
 * - ����/����� �� ���� ���õ� ��ֹ� �߻�
 */
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] private float fSpawnInterval = 2.0f;
    [SerializeField] private float fSpawnOffsetX = 10.0f;
    [SerializeField] private float fSpawnPosY = -7.8f;  //��ֹ� ���� y��ǥ��

    [Header("��ֹ� ������")]
    [SerializeField] private GameObject gBuffPrefab = null;
    [SerializeField] private GameObject gDebuffPrefab = null;

    [Header("�÷��̾�(��ġ) ����")]
    [SerializeField] private Transform gPlayer = null;

    private float fTimer = 0.0f;
    private float fDirInput = 0.0f;

    void Update()
    {
        if (gPlayer == null) return;

        fDirInput = Input.GetAxisRaw("Horizontal"); //GetAxisRaw�� -1, 0, 1���� ������

        if (fDirInput != 0) //������ ��� �������� ����
        {
            fTimer += Time.deltaTime;

            if (fTimer >= fSpawnInterval)
            {
                fTimer = 0f;
                f_GenerateRandomObstacle();
            }
        }
    }

    /// <summary> ��ֹ� ���� ���� �޼ҵ� </summary>
    private void f_GenerateRandomObstacle()
    {
        int nRandNum = Random.Range(0, 2);
        GameObject gSelectObstacle = (nRandNum == 0) ? gBuffPrefab : gDebuffPrefab; //�������ǿ����� ���

        Vector3 vSpawnPos = new Vector3(gPlayer.position.x + fSpawnOffsetX, fSpawnPosY, 0f);
        Instantiate(gSelectObstacle, vSpawnPos, Quaternion.identity);
    }
}
