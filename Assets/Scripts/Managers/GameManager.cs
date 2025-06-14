/*
 * 게임의 전체 흐름을 제어하는 GameManager 클래스
 */
using UnityEngine;
using UnityEngine.SceneManagement;


//게임의 상태를 정의하는 열거형
public enum GameState
{
    Ready,      //게임 대기 상태 (시작 전)
    Running,    //게임 진행 중
    MiniGame,   //미니게임 중단 상태
    Paused,     //일시 정지 (옵션 등)
    GameOver,   //실패 상태
    GameClear   //성공 도착
}

public class GameManager : MonoBehaviour //게임의 전체 흐름을 제어하는 GameManager 클래스
{
    public static GameManager Instance { get; private set; } //싱글톤 패턴 구현
    public GameState CurrentState { get; private set; } = GameState.Ready; //현재 게임 상태 (초기값: Ready)
    [SerializeField] private float fLimitTime = 300.0f; //제한시간 (5분 = 300초)
    
    private float fCurrentTime = 0.0f;      //현재 남은 시간
    private bool isTimerRunning = false;    //타이머 작동 여부

    //현재 남은 시간을 반환하는 read-only 프로퍼티
    public float CurrentTime { get { return fCurrentTime; } }

    private void Awake()
    {
        // 싱글톤 인스턴스 할당 및 중복 제거
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //씬 전환 시에도 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); //이미 인스턴스가 존재하면 현재 오브젝트를 파괴
        }
    }

    void Update()
    {
        if (CurrentState == GameState.Running && isTimerRunning) //게임이 진행 중이고 타이머가 작동 중일 때
        {
            fCurrentTime -= Time.deltaTime;     //프레임당 시간 감소
            if (fCurrentTime <= 0f)             //시간이 0 이하로 떨어질경우
            {
                fCurrentTime = 0f;      //시간을 0으로 지정
                f_OnGameOver();         //게임 오버 처리
            }
        }
    }

    /// <summary> 게임을 시작하는 메소드 (초기화 포함) </summary>
    public void f_StartGame()
    {
        fCurrentTime = fLimitTime; //제한시간으로 초기화
        isTimerRunning = true;     //타이머 작동 시작
        CurrentState = GameState.Running; //게임 상태를 Running으로 변경

        Debug.Log("게임 시작됨: 제한시간 = " + fLimitTime); //게임 시작 로그 출력
    }

    /// <summary> 장애물 충돌 시 미니게임 시작 </summary>
    public void f_OnHitObstacle()
    {
        if (CurrentState != GameState.Running) return; //게임이 진행 중이 아닐 경우 무시

        CurrentState = GameState.MiniGame; //게임 상태를 MiniGame으로 변경
        isTimerRunning = false; //타이머 일시 정지

        Debug.Log("미니게임 시작됨 (타이머 일시정지)");
        MiniGameUI.Instance?.f_OpenMiniGame(); //미니게임 UI 열기
    }

    /// <summary> 미니게임 결과 콜백 (성공/실패) </summary>
    public void f_OnMinigameResult(bool isSuccess)
    {
        if (isSuccess) //미니게임 성공 시
        {
            Debug.Log("미니게임 성공 - 게임 재개");
        }
        else
        {
            Debug.Log("미니게임 실패 - 페널티 적용");
            fCurrentTime -= 10f; // 예: 10초 페널티
        }

        CurrentState = GameState.Running; //게임 상태를 Running으로 변경
        isTimerRunning = true; //타이머 재개
    }

    /// <summary> 플레이어가 도착점에 도달했을 때 호출되는 게임 클리어 처리 </summary>
    public void f_OnGameClear()
    {
        isTimerRunning = false; //타이머 중지
        CurrentState = GameState.GameClear; //게임 상태를 GameClear로 변경

        Debug.Log("게임 클리어!");
        //TODO: ResultUIManager.Instance.ShowClearUI(fCurrentTime);
    }

    /// <summary> 제한시간 초과로 게임 오버 처리 </summary>
    private void f_OnGameOver()
    {
        isTimerRunning = false; //타이머 중지
        CurrentState = GameState.GameOver; //게임 상태를 GameOver로 변경

        Debug.Log("시간 초과 - 게임 오버!");
        //TODO: ResultUIManager.Instance.ShowGameOverUI();
    }

    /// <summary> 제한시간에 변수값을 추가하는 메소드 </summary>
    public void f_AddTime(float value) 
    {
        fCurrentTime += value; //현재 시간에 추가값을 더함
        fCurrentTime = Mathf.Clamp(fCurrentTime, 0f, fLimitTime); //최대 제한시간을 넘지 않도록 클램프 메소드를 적용
    }
}
