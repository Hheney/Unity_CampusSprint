using UnityEngine;

/// <summary>
/// �÷��̾� �Է¿� ���� ��� ���̾ �̵����� ����ȿ���� �����ϴ� ��Ʈ�ѷ�
/// ���̾� �� Z�� ���̿� ���� �ӵ� ���� ����
/// </summary>
public class BackgroundController : MonoBehaviour
{
    public static BackgroundController Instance;

    [SerializeField] private float baseMoveSpeed = 5.0f;  //���� �̵� �ӵ� (���� �� ���̾� ����)
    [SerializeField] private float parallaxScale = 0.5f;  //���̾� �� ���� ����

    public float BaseMoveSpeed { get { return baseMoveSpeed; } } //baseMoveSpeed�� �����ϱ����� read-only ������Ƽ

    private Transform[] backgroundLayers = null; //��� ���̾���� ������ �迭
    private float[] layerSpeedRatio = null;      //�� ���̾��� �ӵ� ������ ������ �迭

    private void Awake()
    {
        Instance = this;

        int nLayerCount = transform.childCount;         //�ڽ� ������Ʈ�� ����(��� ���̾� ����)�� ������
        backgroundLayers = new Transform[nLayerCount];  //��� ���̾���� ������ �迭 �ʱ�ȭ
        layerSpeedRatio = new float[nLayerCount];       //�� ���̾��� �ӵ� ������ ������ �迭 �ʱ�ȭ

        float maxZ = 0.0f; //���� �ָ� �ִ� ���̾��� Z���� ������ ����

        for (int i = 0; i < nLayerCount; i++)
        {
            backgroundLayers[i] = transform.GetChild(i);                //�ڽ� ������Ʈ�� �迭�� ����
            float zDist = Mathf.Abs(backgroundLayers[i].position.z);    //Z���� ���밪�� ����Ͽ� ����
            if (zDist > maxZ) maxZ = zDist;                             //���� �ָ� �ִ� ���̾��� Z���� ����
        }

        for (int i = 0; i < nLayerCount; i++)
        {
            float zDist = Mathf.Abs(backgroundLayers[i].position.z);   //���� ���̾��� Z�� ���밪 ���
            layerSpeedRatio[i] = ((maxZ - zDist) / maxZ) * parallaxScale + (1f - parallaxScale); //�ӵ� ���� ���(���� �ָ� �ִ� ���̾ ���� ������ �̵�)
        }
    }

    /// <summary> PlayerController���� ȣ���: �Է°��� ���� �ݴ� �������� ��� �̵� <summary>
    public void f_MoveByInput(float input)
    {
        if (Mathf.Abs(input) < 0.01f) return; //�Է��� ���� 0�� ������ �̵����� ����(���� ����)

        float fDir = -input; //�÷��̾� ���� �ݴ�� �̵�, ����ȿ�� ������ ����

        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            Vector3 vMoveLayer = Vector3.right * fDir * baseMoveSpeed * layerSpeedRatio[i] * Time.deltaTime; //���̾ �ӵ� ������ �����Ͽ� �̵� ���� ���
            backgroundLayers[i].position += vMoveLayer; //�� ���̾ �̵���Ŵ
        }
    }
}
