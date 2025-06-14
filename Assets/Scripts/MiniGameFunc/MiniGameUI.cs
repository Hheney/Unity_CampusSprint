using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameUI : MonoBehaviour
{
    public static MiniGameUI Instance { get; private set; } //싱글톤 인스턴스 생성

    [Header("UI 연결")]
    [SerializeField] private GameObject gMiniGamePanel = null;          //미니게임 패널
    [SerializeField] private TextMeshProUGUI txtQuizQuestion = null;    //문제 텍스트
    [SerializeField] private Button btnTrue = null;     //O 버튼
    [SerializeField] private Button btnFalse = null;    //X 버튼

    private List<QuizData> listQuestions = new List<QuizData>();    //문제 목록
    private QuizData currentQuestion;                               //현재 문제

    private void Awake() // Awake 메서드에서 싱글톤 인스턴스 설정
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); //이미 인스턴스가 존재하면 현재 오브젝트를 파괴
        }

        gMiniGamePanel.SetActive(false); //미니게임 패널은 초기에는 비활성화
    }

    private void Start()
    {
        btnTrue.onClick.AddListener(() => f_CheckAnswer(true));
        btnFalse.onClick.AddListener(() => f_CheckAnswer(false));

        //문제 추가
        listQuestions.Add(new QuizData("지구는 태양을 돈다", true));
        listQuestions.Add(new QuizData("1km는 100m이다", false));
        listQuestions.Add(new QuizData("코끼리는 포유류이다", true));
        listQuestions.Add(new QuizData("물의 화학식은 CO2이다", false));
    }

    public void f_OpenMiniGame() //미니게임 시작 메소드
    {
        gMiniGamePanel.SetActive(true); //미니게임 패널 활성화
        f_SelectRandomQuestion();       //랜덤 문제 선택
    }

    private void f_SelectRandomQuestion() //랜덤 문제 선택 메소드
    {
        int nRandomIndex = Random.Range(0, listQuestions.Count);   //문제 목록에서 랜덤 인덱스 생성
        currentQuestion = listQuestions[nRandomIndex];             //랜덤으로 선택된 문제 저장
        txtQuizQuestion.text = currentQuestion.sQuestion;          //문제 텍스트 업데이트
    }

    private void f_CheckAnswer(bool userAnswer) //사용자의 답변 확인 메소드
    {
        bool isCorrect = (userAnswer == currentQuestion.isAnswer); //사용자의 답변과 현재 문제의 정답 비교
        gMiniGamePanel.SetActive(false);                           //미니게임 패널 비활성화
        GameManager.Instance?.f_OnMinigameResult(isCorrect);       //GameManager가 null이 아닐경우, GameManger에 결과 전달
    }
}