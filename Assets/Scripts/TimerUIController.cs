/*
 * 제한 시간을 실시간으로 표시하는 UI 컨트롤러 스크립트
 */
using TMPro;
using UnityEngine;

public class TimerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTimerUI = null; //타이머 UI 텍스트 컴포넌트

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null) return; //GameManager가 null인 경우 return

        float fRemainTime = GameManager.Instance.CurrentTime; //GameManager에서 현재 남은 시간 가져오기

        int nMinutes = Mathf.FloorToInt(fRemainTime / 60f); //남은 시간을 분 단위로 변환
        int nSeconds = Mathf.FloorToInt(fRemainTime % 60f); //남은 시간을 초 단위로 변환

        textTimerUI.text = $"남은 시간: {nMinutes:00}:{nSeconds:00}"; //남은 시간을 "분:초" 형식으로 출력 $string text 형식 지정자 사용
    }
}
