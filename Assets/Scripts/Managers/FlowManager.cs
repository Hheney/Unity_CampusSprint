/*
 * [FlowManager(플로우 매니저)]
 * 게임의 씬 전환 및 전체 흐름을 제어하는 싱글톤 매니저 클래스
 * - 씬 전환(Scene 전환)
 * - 현재 씬 이름 조회
 * - 미니게임 흐름, 제한 시간, 게임오버 상태 등을 제어할 확장성을 고려한 구조
 *
 * 기존 GameManager의 기능을 분리/확장한 클래스
 */

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary> 프로젝트 내에서 사용할 씬 이름 열거형(Enum) </summary>
public enum SceneName
{
    MainMenuScene,  //메인 메뉴 씬
    GameScene       //게임 씬
}

/// <summary> 게임의 흐름을 제어하는 FlowManager 클래스 </summary>
public class FlowManager : MonoBehaviour
{
    private static FlowManager _instance = null;

    //프로퍼티로 게임 재시작 모드 여부를 외부에서 간접 접근
    public bool IsRetryMode { get; private set; } = false; //게임 재시작 모드 여부 (기본값: false)

    public static FlowManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogWarning("FlowManager is null.");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Debug.Log("FlowManager has another instance.");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 디바이스 성능에 관계없이 일정한 속도로 게임을 실행하기 위해 프레임레이트 고정
        Application.targetFrameRate = 60;
    }

    /// <summary> 게임 재시작 모드 설정 메소드 </summary>
    public void f_SetRetryMode(bool isRetry)
    {
        IsRetryMode = isRetry; //게임 재시작 모드 설정
    }

    /// <summary> 지정한 씬으로 전환하는 메소드 </summary>
    public void f_OpenScene(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
    }

    /// <summary> 현재 씬을 SceneName Enum으로 반환하는 메소드 </summary>
    public SceneName f_GetCurrentSceneName()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (System.Enum.TryParse(sceneName, out SceneName currentScene))
        {
            return currentScene;
        }
        else
        {
            Debug.LogWarning($"씬 {sceneName}이 SceneName Enum에 존재하지 않습니다.");
            return SceneName.MainMenuScene; //예외 상황 발생 시 기본 씬 반환
        }
    }

    /// <summary> 현재 씬 이름을 문자열로 반환하는 메소드 (사운드 매핑 등 외부 활용용) </summary>
    public string f_GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
