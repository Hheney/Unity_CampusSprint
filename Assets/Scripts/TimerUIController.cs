/*
 * 제한 시간을 실시간으로 표시하는 UI 컨트롤러 스크립트
 */
using TMPro;
using UnityEngine;

public class TimerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTimerUI = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null) return;

        float fRemainTime = GameManager.Instance.CurrentTime;

        int minutes = Mathf.FloorToInt(fRemainTime / 60f);
        int seconds = Mathf.FloorToInt(fRemainTime % 60f);

        textTimerUI.text = $"Remain Time: {minutes:00}:{seconds:00}";
    }
}
