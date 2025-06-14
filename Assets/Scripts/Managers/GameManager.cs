/*
 * 게임의 전체 흐름을 제어하는 GameManager 클래스
 */
using UnityEngine;
using UnityEngine.SceneManagement;


//게임 상태 정의 Enum
public enum GameState
{
    Ready,      //게임 대기 상태 (시작 전)
    Running,    //게임 진행 중
    MiniGame,   //미니게임 중단 상태
    Paused,     //일시 정지 (옵션 등)
    GameOver,   //실패 상태
    GameClear   //성공 도착
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } //싱글톤 패턴 구현
    public GameState CurrentState { get; private set; } = GameState.Ready;

    [SerializeField] private float fLimitTime = 300f; //제한시간 (5분 = 300초)
    
    private float fCurrentTime = 0f;                  //현재 남은 시간
    private bool isTimerRunning = false;

    public float CurrentTime { get { return fCurrentTime; } } //남은 시간을 반환하는 read-only 프로퍼티

    private void Awake()
    {
        // 싱글톤 인스턴스 할당 및 중복 제거
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (CurrentState == GameState.Running && isTimerRunning)
        {
            fCurrentTime -= Time.deltaTime;
            if (fCurrentTime <= 0f)
            {
                fCurrentTime = 0f;
                f_OnGameOver();
            }
        }
    }

    /// <summary>
    /// 게임을 시작하는 메소드 (초기화 포함)
    /// </summary>
    public void f_StartGame()
    {
        fCurrentTime = fLimitTime;
        isTimerRunning = true;
        CurrentState = GameState.Running;

        Debug.Log("게임 시작됨: 제한시간 = " + fLimitTime);
    }

    /// <summary>
    /// 장애물 충돌 시 미니게임 시작
    /// </summary>
    public void f_OnHitObstacle()
    {
        if (CurrentState != GameState.Running) return;

        CurrentState = GameState.MiniGame;
        isTimerRunning = false;

        Debug.Log("미니게임 시작됨 (타이머 일시정지)");
        // TODO: MinigameManager.Instance.f_StartMinigame(OnMinigameResult);
    }

    /// <summary>
    /// 미니게임 결과 콜백 (성공/실패)
    /// </summary>
    public void f_OnMinigameResult(bool isSuccess)
    {
        if (isSuccess)
        {
            Debug.Log("미니게임 성공 - 게임 재개");
        }
        else
        {
            Debug.Log("미니게임 실패 - 페널티 적용");
            fCurrentTime -= 10f; // 예: 10초 페널티
        }

        CurrentState = GameState.Running;
        isTimerRunning = true;
    }

    /// <summary>
    /// 플레이어가 도착점에 도달했을 때 호출되는 게임 클리어 처리
    /// </summary>
    public void f_OnGameClear()
    {
        isTimerRunning = false;
        CurrentState = GameState.GameClear;

        Debug.Log("게임 클리어!");
        // TODO: ResultUIManager.Instance.ShowClearUI(fCurrentTime);
    }

    /// <summary>
    /// 제한시간 초과로 게임 오버 처리
    /// </summary>
    private void f_OnGameOver()
    {
        isTimerRunning = false;
        CurrentState = GameState.GameOver;

        Debug.Log("시간 초과 - 게임 오버!");
        // TODO: ResultUIManager.Instance.ShowGameOverUI();
    }

    /// <summary>
    /// 현재 씬 이름을 반환하는 유틸리티 메소드
    /// </summary>
    public string f_GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void f_AddTime(float value)
    {
        fCurrentTime += value;
        fCurrentTime = Mathf.Clamp(fCurrentTime, 0f, fLimitTime);
    }
}
