using UnityEngine;

public class ClearZoneGenerator : MonoBehaviour
{
    [Header("���� ����")]
    [SerializeField] private int nRequiredLoopCount = 10;   //�ݺ� Ƚ�� ����
    [SerializeField] private float fSpawnOffsetX = 15.0f;   //�÷��̾� ���� X ������
    [SerializeField] private float fSpawnPosY = -7.8f;      //���� Y��ǥ

    [Header("������ �� ����")]
    [SerializeField] private GameObject gClearZonePrefab = null;  //Ŭ������ ������
    [SerializeField] private Transform gPlayer = null;            //�÷��̾� ��ġ ����

    private bool isSpawned = false; //�ߺ� ���� ����

    void Update()
    {
        //������ ���� ���� �ƴ� ��� Early Return ó��
        if (GameManager.Instance == null || GameManager.Instance.CurrentState != GameState.Running) return;
        if (isSpawned) return; //�̹� Ŭ�������� �����Ǿ����� Early Return


        // Ŭ���� ���� �޼�
        if (ClearCounterManager.Instance != null &&
            ClearCounterManager.Instance.LoopCount >= nRequiredLoopCount) //Ŭ���� ī���� �Ŵ����� null�� �ƴϸ�, ���� ī��Ʈ�� ����(10ȸ) �̻��� ���
        {
            isSpawned = true; //Ŭ������ ���� �÷��� ����

            Vector3 vSpawnPos = new Vector3(gPlayer.position.x + fSpawnOffsetX, fSpawnPosY, 0.0f); //�÷��̾� ��ġ �������� X �������� ������ Ŭ������ ���� ��ġ
            Instantiate(gClearZonePrefab, vSpawnPos, Quaternion.identity); //Ŭ������ �������� ���� ��ġ�� ����

            Debug.Log($"[Ŭ������ ����] ��ġ: {vSpawnPos}");
        }
    }


}
