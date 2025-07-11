/*
 * [배경 반복 오브젝트의 공통 로직을 정의한 추상 클래스]
 * 기능 : fScrollWidth 자동 계산 gReferenceTarget 연결
 * 반복 재생 조건은 하위 클래스에서 오버라이드(덮어쓰기)
 * 추상 클래스를 사용해서 반복되는 부분은 통합하여 유지보수성을 높임
*/

using UnityEngine;

public abstract class BaseLoopBackground : MonoBehaviour
{
    //정의된 내용은 모두 하위 클래스에서 상속예정이므로 protected 접근지정

    [SerializeField] protected Transform gReferenceTarget; //기준이 되는 타겟(플레이어)의 Transform 컴포넌트 필드

    protected SpriteRenderer spriteRenderer = null;
    protected float fScrollWidth = 0.0f;    //레이어의 크기를 측정할 필드
                                            //(에셋 제공 크기는 40unit이지만 변경될 수 있으므로 크기 측정을 자동화하기 위함)

    protected virtual void Awake()
    {
        //SpriteRenderer 컴포넌트를 가져와서 SpriteRenderer 가로 길이로 fScrollWidth 자동 계산
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            fScrollWidth = spriteRenderer.bounds.size.x; //SpriteRenderer의 bounds를 사용하여 가로 길이를 계산
        }
    }

    protected virtual void Update()
    {
        if (gReferenceTarget == null) return;   //기준점이 null일 경우 Early Return
                                                //하위 로직이 구현안되도록 하기 위함

        f_CheckAndLoop(); //반복 여부 판단 및 재설정 메소드 호출
    }

    protected abstract void f_CheckAndLoop(); //하위 클래스에서 구현해야 하는 추상 메소드

    /// <summary> 외부에서 기준 타겟 설정할 수 있는 메소드 </summary>
    public void f_SetReferenceTarget(Transform target)
    {
        gReferenceTarget = target;
    }
}
