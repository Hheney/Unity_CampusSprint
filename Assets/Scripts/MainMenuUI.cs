//메인 메뉴 UI를 관리하는 스크립트
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("버튼 오브젝트 연결")]
    [SerializeField] private Button BtnStart = null; //게임 시작 버튼
    [SerializeField] private Button BtnExit = null;  //게임 종료 버튼

    [Header("게임 씬 지정 (Enum 기반)")]
    [SerializeField] private SceneName gameScene = SceneName.GameScene; //게임 씬 이름 (Enum으로 지정)

    void Start()
    {
        //게임 재시작 모드가 활성화되어 있다면, FlowManager를 통해 게임 씬으로 자동 전환
        if (FlowManager.Instance != null && FlowManager.Instance.IsRetryMode)
        {
            FlowManager.Instance.f_SetRetryMode(false); //게임 재시작 모드 초기화
            f_OnClickStart(); //자동으로 게임 씬 진입
            return;
        }

        //버튼 클릭 이벤트 등록
        if (BtnStart != null)
            BtnStart.onClick.AddListener(f_OnClickStart); //게임 시작 버튼 클릭 시 f_OnClickStart 메소드 호출

        if (BtnExit != null)
            BtnExit.onClick.AddListener(f_OnClickExit); //게임 종료 버튼 클릭 시 f_OnClickExit 메소드 호출

        SoundManager.Instance?.f_AutoPlayBGM(); //SoundManager를 통해 자동으로 BGM 재생
    }

    /// <summary> 게임 시작 버튼 클릭 시 FlowManager를 통해 게임 씬으로 전환 </summary>
    private void f_OnClickStart()
    {
        Debug.Log("게임 시작");
        SoundManager.Instance?.f_PlaySFX(SoundName.SFX_POP, 1.0f); //팝업 효과음 재생
        SoundManager.Instance?.f_StopAllBGM(); //모든 BGM 정지
        FlowManager.Instance?.f_OpenScene(gameScene); //FlowManager를 통해 지정된 게임 씬으로 전환
    }

    /// <summary> 종료 버튼 클릭 시 애플리케이션 종료 </summary>
    private void f_OnClickExit()
    {
        Debug.Log("게임 종료");
        SoundManager.Instance?.f_PlaySFX(SoundName.SFX_POP, 1.0f); //팝업 효과음 재생
        SoundManager.Instance?.f_StopAllBGM(); //모든 BGM 정지
        //에디터 종료(에디터 상에서 프로그램이 실행되기 때문에 에디터 실행을 종료)
        UnityEditor.EditorApplication.isPlaying = false;
    }
}