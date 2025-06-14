/*
 * ���� �ð��� �ǽð����� ǥ���ϴ� UI ��Ʈ�ѷ� ��ũ��Ʈ
 */
using TMPro;
using UnityEngine;

public class TimerUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTimerUI = null; //Ÿ�̸� UI �ؽ�Ʈ ������Ʈ

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null) return; //GameManager�� null�� ��� return

        float fRemainTime = GameManager.Instance.CurrentTime; //GameManager���� ���� ���� �ð� ��������

        int nMinutes = Mathf.FloorToInt(fRemainTime / 60f); //���� �ð��� �� ������ ��ȯ
        int nSeconds = Mathf.FloorToInt(fRemainTime % 60f); //���� �ð��� �� ������ ��ȯ

        textTimerUI.text = $"���� �ð�: {nMinutes:00}:{nSeconds:00}"; //���� �ð��� "��:��" �������� ��� $string text ���� ������ ���
    }
}
