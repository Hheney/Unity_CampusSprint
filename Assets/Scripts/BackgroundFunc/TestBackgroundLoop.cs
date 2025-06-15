/*
 * 배경 오브젝트가 일정 거리 이상 이동했을 때 반복 위치로 재배치되도록 처리하는 반복 배경 스크립트
 * BackgroundController와 분리되어 SRP(단일 책임 원칙)를 따르도록 하여 반복처리만 담당하는 스크립트 → 유지보수에 유리
 */
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [SerializeField] private Transform g_ReferenceTarget; //기준 위치(플레이어를 기준점으로 지정)
    
    private SpriteRenderer spriteRenderer = null;
    private float fScrollWidth = 0.0f; //레이어의 크기를 측정할 필드
                                       //(에셋 제공 크기는 40unit이지만 변경될 수 있으므로 크기 측정을 자동화하기 위함)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //SpriteRenderer 컴포넌트를 가져와서 SpriteRenderer 가로 길이로 fScrollWidth 자동 계산
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        fScrollWidth = spriteRenderer.bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        //기준 위치 기준으로 배경이 왼쪽으로 충분히 벗어나면 오른쪽으로 재배치
        if (transform.position.x + fScrollWidth < g_ReferenceTarget.position.x)
        {
            //화면 왼쪽을 벗어났다면 오른쪽으로 2배 이동시킴
            transform.position += Vector3.right * fScrollWidth * 2.0f;
        }
    }
}
