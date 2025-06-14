/*
 * 제한 시간을 실시간으로 표시하는 UI 컨트롤러 스크립트
 * 시간 증가 시: 텍스트가 노란색으로 깜박임
 * 시간 감소 시: 텍스트가 빨간색으로 깜박임
 */
using System.Collections;
using TMPro;
using UnityEngine;

public class TimerUIManager : MonoBehaviour
{
    public static TimerUIManager Instance { get; private set; } //싱글톤 인스턴스

    [Header("타이머 텍스트")]
    [SerializeField] private TextMeshProUGUI textTimerUI = null;    //타이머 UI 텍스트 컴포넌트

    [Header("타이머 효과 설정")]
    [SerializeField] private Color colorDefault = Color.white;      //기본 텍스트 색상
    [SerializeField] private float fBlinkDuration = 0.8f;           //총 지속시간
    [SerializeField] private int iBlinkCount = 2;                   //깜박 횟수
    [SerializeField] private float fScaleSize = 1.2f;               //확대 비율
    [SerializeField] private float fScaleDuration = 0.2f;           //확대 속도

    private Vector3 vOriginalScale = Vector3.zero; //기본 스케일 저장 변수

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); //이미 인스턴스가 존재하면 현재 오브젝트 삭제
        }
    }

    void Start()
    {
        vOriginalScale = textTimerUI.rectTransform.localScale; //텍스트의 기본 스케일을 저장
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null) return; //GameManager가 null인 경우 return

        float fRemainTime = GameManager.Instance.CurrentTime; //GameManager에서 현재 남은 시간 가져오기

        int nMinutes = Mathf.FloorToInt(fRemainTime / 60f); //남은 시간을 분 단위로 변환
        int nSeconds = Mathf.FloorToInt(fRemainTime % 60f); //남은 시간을 초 단위로 변환

        textTimerUI.text = $"남은 시간: {nMinutes:00}:{nSeconds:00}"; //남은 시간을 "분:초" 형식으로 출력 $string text 형식 지정자 사용
    }

    public void f_PlayTimerEffect(Color colorEffect) //타이머 효과 재생 메소드
    {
        StopAllCoroutines(); //기존 코루틴 중지
        StartCoroutine(f_ColorPlayTimerEffect(colorEffect)); //새로운 코루틴 시작
    }

    private IEnumerator f_ColorPlayTimerEffect(Color colorEffect)
    {
        //색상 깜박임
        for (int i = 0; i < iBlinkCount; i++)
        {
            float fBlinkTime = 0.0f; //깜박임 시간 초기화
            while (fBlinkTime < fBlinkDuration / iBlinkCount)
            {
                float fBlinkSpeed = Mathf.PingPong(fBlinkTime * 4f, 1f); //깜박임 효과 빠르게
                textTimerUI.color = Color.Lerp(colorDefault, colorEffect, fBlinkSpeed);
                fBlinkTime += Time.deltaTime;
                yield return null;
            }
        }

        textTimerUI.color = colorDefault; //기본 색상으로 복원

        float fScaleAnimTime = 0.0f; //스케일 애니메이션 시간 초기화
        while (fScaleAnimTime < fScaleDuration)
        {
            float fScaleTime = fScaleAnimTime / fScaleDuration;             //애니메이션 시간 비율 계산
            float scale = Mathf.Lerp(1f, fScaleSize, fScaleTime);           //스케일 비율 계산
            textTimerUI.rectTransform.localScale = vOriginalScale * scale;  //텍스트 스케일 적용
            fScaleAnimTime += Time.deltaTime; //프레임당 시간 증가
            yield return null; 
        }

        textTimerUI.rectTransform.localScale = vOriginalScale; //원래 스케일로 복원
    }
}
