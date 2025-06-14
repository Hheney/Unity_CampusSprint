/*
 * ���� �ð��� �ǽð����� ǥ���ϴ� UI ��Ʈ�ѷ� ��ũ��Ʈ
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
