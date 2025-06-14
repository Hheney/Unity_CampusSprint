using UnityEngine;

/// <summary>
/// 플레이어 입력에 따라 배경 레이어를 이동시켜 착시효과를 구현하는 컨트롤러
/// 레이어 간 Z값 차이에 따라 속도 차등 적용
/// </summary>
public class BackgroundController : MonoBehaviour
{
    public static BackgroundController Instance;

    [SerializeField] private float baseMoveSpeed = 2f;    // 기준 이동 속도 (가장 앞 레이어 기준)
    [SerializeField] private float parallaxScale = 0.5f;  // 레이어 간 차등 비율

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
            layerSpeedRatio[i] = ((maxZ - zDist) / maxZ) * parallaxScale + (1f - parallaxScale); // 가까울수록 속도 빠르게
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
    /// PlayerController에서 호출됨: 입력값에 따라 반대 방향으로 배경 이동
    /// </summary>
    public void f_MoveByInput(float input)
    {
        if (Mathf.Abs(input) < 0.01f) return;

        float direction = -input; // 플레이어 방향 반대로 이동해야 착시효과

        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            Vector3 move = Vector3.right * direction * baseMoveSpeed * layerSpeedRatio[i] * Time.deltaTime;
            backgroundLayers[i].position += move;
        }
    }
}
