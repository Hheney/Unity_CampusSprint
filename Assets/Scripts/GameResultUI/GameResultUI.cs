/*
 * 게임 클리어 결과를 보여주는 UI 컨트롤러 스크립트
 * 시간 기반 점수 출력, 클리어 패널을 표시하는 기능
 */
using UnityEngine;
using TMPro;

public class GameResultUI : MonoBehaviour
{
    public static GameResultUI Instance { get; private set; } //싱글톤 인스턴스

    [Header("클리어 UI")]
    [SerializeField] private GameObject gClearPanel = null;     //클리어 패널 오브젝트
    [SerializeField] private TMP_InputField inputPlayerName;    //플레이어 이름 입력 필드
    [SerializeField] private TMP_Text txtScore = null;          //점수 텍스트

    private int nLastScore = 0;         //마지막 점수 저장용 변수
    private bool isScoreSaved = false;  //점수 저장 여부 플래그(중복 방지)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); //이미 인스턴스가 존재하면 현재 오브젝트를 파괴
        }

        gClearPanel.SetActive(false); //시작 시 패널 비활성화
    }

    /// <summary> 게임 클리어 UI를 출력하는 메소드 </summary>
    public void f_ShowClearUI(int nScore)
    {
        gClearPanel.SetActive(true);            //클리어 패널 활성화

        nLastScore = nScore; //마지막 점수 저장
        txtScore.text = $"점수 : {nScore}";     //점수 출력
        inputPlayerName.text = "Player"; //입력이 없을 경우 기본값 이름인 "Player"설정
        isScoreSaved = false; //점수 저장 여부 초기화
    }

    public void f_OnRetryGame()
    {
        f_SaveScoreAndName(); //닉네임과 점수 저장
        //GameManager.Instance?.f_StartGame(); //게임 재시작 *초기화 관련 버그로 인해서 주석처리 및 다른 로직사용

        //TODO : 이전 Bamsongi 게임에서와 마찬가지인 매니저간 의존성 문제로 인해 버그가 발생함. 구조적 개편이 필요함.
        //GameManager의 f_StartGame() 메소드를 호출하는 대신 FlowManager를 통해 씬을 재시작하도록 변경

        FlowManager.Instance?.f_SetRetryMode(true); //재시작 모드 설정
        FlowManager.Instance?.f_OpenScene(SceneName.MainMenuScene); //메인 메뉴로 이동
    }

    public void f_OnMainMenu()
    {
        f_SaveScoreAndName(); //닉네임과 점수 저장

        FlowManager.Instance?.f_SetRetryMode(false); //재시작 모드 비활성화
        FlowManager.Instance?.f_OpenScene(SceneName.MainMenuScene); //메인 메뉴로 이동
    }

    /// <summary> 닉네임과 점수를 랭크매니저에 전달하는 메소드 </summary>
    private void f_SaveScoreAndName()
    {
        if (isScoreSaved) return; //이미 점수가 저장되었으면 중복 저장 방지를 위해 Eaerly Return
        isScoreSaved = true; //점수 저장 플래그 설정

        //Trim()은 문자열(string)의 양쪽 끝에 있는 공백(스페이스, 탭, 줄바꿈 등)을 제거하는 메소드
        //저장시 공백이 있을 경우를 대비하여 Trim() 사용함(오류 방지)
        string playerName = inputPlayerName.text.Trim();
        if (string.IsNullOrEmpty(playerName)) //입력된 이름이 비어있거나 null인 경우
        {
            playerName = "Player"; //기본값으로 "Player" 설정
        }

        RankManager.Instance?.f_AddRank(playerName, nLastScore); //랭크 매니저에 점수와 이름 전달
    }

}
