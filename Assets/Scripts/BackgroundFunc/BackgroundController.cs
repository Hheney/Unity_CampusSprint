using UnityEngine;

/// <summary>
/// 플레이어 입력에 따라 배경 레이어를 이동시켜 착시효과를 구현하는 컨트롤러
/// 레이어 간 Z값 차이에 따라 속도 차등 적용
/// </summary>
public class BackgroundController : MonoBehaviour
{
    public static BackgroundController Instance;

    [SerializeField] private float baseMoveSpeed = 5.0f;  //기준 이동 속도 (가장 앞 레이어 기준)
    [SerializeField] private float parallaxScale = 0.5f;  //레이어 간 차등 비율

    public float BaseMoveSpeed { get { return baseMoveSpeed; } } //baseMoveSpeed를 참조하기위한 read-only 프로퍼티

    private Transform[] backgroundLayers = null; //배경 레이어들을 저장할 배열
    private float[] layerSpeedRatio = null;      //각 레이어의 속도 비율을 저장할 배열

    private void Awake()
    {
        Instance = this;

        int nLayerCount = transform.childCount;         //자식 오브젝트의 개수(배경 레이어 개수)를 가져옴
        backgroundLayers = new Transform[nLayerCount];  //배경 레이어들을 저장할 배열 초기화
        layerSpeedRatio = new float[nLayerCount];       //각 레이어의 속도 비율을 저장할 배열 초기화

        float maxZ = 0.0f; //가장 멀리 있는 레이어의 Z값을 저장할 변수

        for (int i = 0; i < nLayerCount; i++)
        {
            backgroundLayers[i] = transform.GetChild(i);                //자식 오브젝트를 배열에 저장
            float zDist = Mathf.Abs(backgroundLayers[i].position.z);    //Z값의 절대값을 계산하여 저장
            if (zDist > maxZ) maxZ = zDist;                             //가장 멀리 있는 레이어의 Z값을 갱신
        }

        for (int i = 0; i < nLayerCount; i++)
        {
            float zDist = Mathf.Abs(backgroundLayers[i].position.z);   //현재 레이어의 Z값 절대값 계산
            layerSpeedRatio[i] = ((maxZ - zDist) / maxZ) * parallaxScale + (1f - parallaxScale); //속도 비율 계산(가장 멀리 있는 레이어가 가장 느리게 이동)
        }
    }

    /// <summary> PlayerController에서 호출됨: 입력값에 따라 반대 방향으로 배경 이동 <summary>
    public void f_MoveByInput(float input)
    {
        if (Mathf.Abs(input) < 0.01f) return; //입력이 거의 0에 가까우면 이동하지 않음(오류 방지)

        float fDir = -input; //플레이어 방향 반대로 이동, 착시효과 구현을 위함

        for (int i = 0; i < backgroundLayers.Length; i++)
        {
            Vector3 vMoveLayer = Vector3.right * fDir * baseMoveSpeed * layerSpeedRatio[i] * Time.deltaTime; //레이어별 속도 비율을 적용하여 이동 벡터 계산
            backgroundLayers[i].position += vMoveLayer; //각 레이어를 이동시킴
        }
    }
}
