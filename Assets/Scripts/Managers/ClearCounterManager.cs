using UnityEngine;

/// <summary>
///  반복 배경 카운트를 관리하는 ClearCounterManager 클래스
/// </summary>
public class ClearCounterManager : MonoBehaviour
{
    public static ClearCounterManager Instance { get; private set; }

    private int nLoopCount = 0; //반복 배경 카운트

    public int LoopCount { get { return nLoopCount; } } //읽기 전용 프로퍼티, 외부에서 카운트 값 참조가능

    private void Awake()
    {
        // 싱글톤 인스턴스 할당 및 중복 제거
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); //이미 인스턴스가 존재하면 현재 오브젝트를 파괴
        }
    }

    public void f_AddCount()
    {
        nLoopCount++;
        Debug.Log($"반복 배경 카운트: {nLoopCount}");
    }
}
