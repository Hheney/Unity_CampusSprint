/*
 * ���� �ð��� �ǽð����� ǥ���ϴ� UI ��Ʈ�ѷ� ��ũ��Ʈ
 * �ð� ���� ��: �ؽ�Ʈ�� ��������� ������
 * �ð� ���� ��: �ؽ�Ʈ�� ���������� ������
 */
using System.Collections;
using TMPro;
using UnityEngine;

public class TimerUIManager : MonoBehaviour
{
    public static TimerUIManager Instance { get; private set; } //�̱��� �ν��Ͻ�

    [Header("Ÿ�̸� �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI textTimerUI = null;    //Ÿ�̸� UI �ؽ�Ʈ ������Ʈ

    [Header("Ÿ�̸� ȿ�� ����")]
    [SerializeField] private Color colorDefault = Color.white;      //�⺻ �ؽ�Ʈ ����
    [SerializeField] private float fBlinkDuration = 0.8f;           //�� ���ӽð�
    [SerializeField] private int iBlinkCount = 2;                   //���� Ƚ��
    [SerializeField] private float fScaleSize = 1.2f;               //Ȯ�� ����
    [SerializeField] private float fScaleDuration = 0.2f;           //Ȯ�� �ӵ�

    private Vector3 vOriginalScale = Vector3.zero; //�⺻ ������ ���� ����

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); //�̹� �ν��Ͻ��� �����ϸ� ���� ������Ʈ ����
        }
    }

    void Start()
    {
        vOriginalScale = textTimerUI.rectTransform.localScale; //�ؽ�Ʈ�� �⺻ �������� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance == null) return; //GameManager�� null�� ��� return

        float fRemainTime = GameManager.Instance.CurrentTime; //GameManager���� ���� ���� �ð� ��������

        int nMinutes = Mathf.FloorToInt(fRemainTime / 60f); //���� �ð��� �� ������ ��ȯ
        int nSeconds = Mathf.FloorToInt(fRemainTime % 60f); //���� �ð��� �� ������ ��ȯ

        textTimerUI.text = $"���� �ð�: {nMinutes:00}:{nSeconds:00}"; //���� �ð��� "��:��" �������� ��� $string text ���� ������ ���
    }

    public void f_PlayTimerEffect(Color colorEffect) //Ÿ�̸� ȿ�� ��� �޼ҵ�
    {
        StopAllCoroutines(); //���� �ڷ�ƾ ����
        StartCoroutine(f_ColorPlayTimerEffect(colorEffect)); //���ο� �ڷ�ƾ ����
    }

    private IEnumerator f_ColorPlayTimerEffect(Color colorEffect)
    {
        //���� ������
        for (int i = 0; i < iBlinkCount; i++)
        {
            float fBlinkTime = 0.0f; //������ �ð� �ʱ�ȭ
            while (fBlinkTime < fBlinkDuration / iBlinkCount)
            {
                float fBlinkSpeed = Mathf.PingPong(fBlinkTime * 4f, 1f); //������ ȿ�� ������
                textTimerUI.color = Color.Lerp(colorDefault, colorEffect, fBlinkSpeed);
                fBlinkTime += Time.deltaTime;
                yield return null;
            }
        }

        textTimerUI.color = colorDefault; //�⺻ �������� ����

        float fScaleAnimTime = 0.0f; //������ �ִϸ��̼� �ð� �ʱ�ȭ
        while (fScaleAnimTime < fScaleDuration)
        {
            float fScaleTime = fScaleAnimTime / fScaleDuration;             //�ִϸ��̼� �ð� ���� ���
            float scale = Mathf.Lerp(1f, fScaleSize, fScaleTime);           //������ ���� ���
            textTimerUI.rectTransform.localScale = vOriginalScale * scale;  //�ؽ�Ʈ ������ ����
            fScaleAnimTime += Time.deltaTime; //�����Ӵ� �ð� ����
            yield return null; 
        }

        textTimerUI.rectTransform.localScale = vOriginalScale; //���� �����Ϸ� ����
    }
}
