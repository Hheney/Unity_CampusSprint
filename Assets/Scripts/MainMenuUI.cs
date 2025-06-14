using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 메인 메뉴에서 버튼 입력을 처리하는 전용 스크립트 (FlowManager 사용 버전)
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [Header("버튼 오브젝트 연결")]
    [SerializeField] private Button startButton = null;
    [SerializeField] private Button exitButton = null;

    [Header("게임 씬 지정 (Enum 기반)")]
    [SerializeField] private SceneName gameScene = SceneName.GameScene;

    void Start()
    {
        // 버튼 클릭 이벤트 등록
        if (startButton != null)
            startButton.onClick.AddListener(f_OnClickStart);

        if (exitButton != null)
            exitButton.onClick.AddListener(f_OnClickExit);
    }

    /// <summary>
    /// 게임 시작 버튼 클릭 시 FlowManager를 통해 게임 씬으로 전환
    /// </summary>
    private void f_OnClickStart()
    {
        Debug.Log("게임 시작");
        FlowManager.Instance?.f_OpenScene(gameScene);
    }

    /// <summary>
    /// 종료 버튼 클릭 시 애플리케이션 종료
    /// </summary>
    private void f_OnClickExit()
    {
        Debug.Log("게임 종료");

        //에디터 종료(에디터 상에서 프로그램이 실행되기 때문에 에디터 실행을 종료)
        UnityEditor.EditorApplication.isPlaying = false;
    }


}