using UnityEngine;

/// <summary>
/// �÷��̾� �Է¿� ���� ��� ���̾ �̵����� ����ȿ���� �����ϴ� ��Ʈ�ѷ�
/// ���̾� �� Z�� ���̿� ���� �ӵ� ���� ����
/// </summary>
public class BackgroundController : MonoBehaviour
{
    public static BackgroundController Instance;

    [SerializeField] private float baseMoveSpeed = 2f;    // ���� �̵� �ӵ� (���� �� ���̾� ����)
    [SerializeField] private float parallaxScale = 0.5f;  // ���̾� �� ���� ����

    private Transform[] backgroundLayers = null;
    private float[] layerSpeedRatio = null;

    private void Awake()
    {
        Instance = this;

        int count = transform.childCount;
        backgroundLayers = new Transform[count];
        layerSpeedRatio = new float[count];

        float maxZ = 0f;

        for (int i = 0; i < count; i++)
        {
            backgroundLayers[i] = transform.GetChild(i);
            float zDist = Mathf.Abs(backgroundLayers[i].position.z);
            if (zDist > maxZ) maxZ = zDist;
        }

        for (int i = 0; i < count; i++)
        {
            float zDist = Mathf.Abs(backgroundLayers[i].position.z);
            layerSpeedRatio[i] = ((maxZ - zDist) / maxZ) * parallaxScale + (1f - parallaxScale); // �������� �ӵ� ������
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// PlayerController���� ȣ���: �Է°��� ���� �ݴ� �������� ��� �̵�
    /// </summary>
    public void f_MoveByInput(float input)
    {
        if (Mathf.Abs(input) < 0.01f) return;

        float direction = -input; // �÷��̾� ���� �ݴ�� �̵��ؾ� ����ȿ��

        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            Vector3 move = Vector3.right * direction * baseMoveSpeed * layerSpeedRatio[i] * Time.deltaTime;
            backgroundLayers[i].position += move;
        }
    }
}
