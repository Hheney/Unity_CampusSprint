using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameUI : MonoBehaviour
{
    public static MiniGameUI Instance { get; private set; } //�̱��� �ν��Ͻ� ����

    [Header("UI ����")]
    [SerializeField] private GameObject gMiniGamePanel = null;          //�̴ϰ��� �г�
    [SerializeField] private TextMeshProUGUI txtQuizQuestion = null;    //���� �ؽ�Ʈ
    [SerializeField] private Button btnTrue = null;     //O ��ư
    [SerializeField] private Button btnFalse = null;    //X ��ư

    private List<QuizData> listQuestions = new List<QuizData>();    //���� ���
    private QuizData currentQuestion;                               //���� ����

    private void Awake() // Awake �޼��忡�� �̱��� �ν��Ͻ� ����
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); //�̹� �ν��Ͻ��� �����ϸ� ���� ������Ʈ�� �ı�
        }

        gMiniGamePanel.SetActive(false); //�̴ϰ��� �г��� �ʱ⿡�� ��Ȱ��ȭ
    }

    private void Start()
    {
        btnTrue.onClick.AddListener(() => f_CheckAnswer(true));
        btnFalse.onClick.AddListener(() => f_CheckAnswer(false));

        //���� �߰�
        listQuestions.Add(new QuizData("������ �¾��� ����", true));
        listQuestions.Add(new QuizData("1km�� 100m�̴�", false));
        listQuestions.Add(new QuizData("�ڳ����� �������̴�", true));
        listQuestions.Add(new QuizData("���� ȭ�н��� CO2�̴�", false));
    }

    public void f_OpenMiniGame() //�̴ϰ��� ���� �޼ҵ�
    {
        gMiniGamePanel.SetActive(true); //�̴ϰ��� �г� Ȱ��ȭ
        f_SelectRandomQuestion();       //���� ���� ����
    }

    private void f_SelectRandomQuestion() //���� ���� ���� �޼ҵ�
    {
        int nRandomIndex = Random.Range(0, listQuestions.Count);   //���� ��Ͽ��� ���� �ε��� ����
        currentQuestion = listQuestions[nRandomIndex];             //�������� ���õ� ���� ����
        txtQuizQuestion.text = currentQuestion.sQuestion;          //���� �ؽ�Ʈ ������Ʈ
    }

    private void f_CheckAnswer(bool userAnswer) //������� �亯 Ȯ�� �޼ҵ�
    {
        bool isCorrect = (userAnswer == currentQuestion.isAnswer); //������� �亯�� ���� ������ ���� ��
        gMiniGamePanel.SetActive(false);                           //�̴ϰ��� �г� ��Ȱ��ȭ
        GameManager.Instance?.f_OnMinigameResult(isCorrect);       //GameManager�� null�� �ƴҰ��, GameManger�� ��� ����
    }
}