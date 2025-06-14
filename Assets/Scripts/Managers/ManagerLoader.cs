using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 초기화 전용 InitScene에서 실행되는 매니저 자동 로더 스크립트
/// - SoundManager, GameManager 등 필수 매니저를 초기화한 뒤 메인 씬으로 전환
/// - 씬 전환 전 반드시 매니저 초기화가 완료되도록 1프레임 대기
/// </summary>
public class ManagerLoader : MonoBehaviour
{
    [Header("Target loading scenename")]
    [SerializeField] private string nextSceneName = "MainMenuScene";

    private void Start()
    {
        // Start 시점에 코루틴으로 다음 씬 전환
        StartCoroutine(f_LoadNextScene());
    }

    /// <summary>
    /// 매니저 초기화 완료 후 다음 씬 로드 (1프레임 대기)
    /// </summary>
    private System.Collections.IEnumerator f_LoadNextScene()
    {
        yield return null; // 매니저들이 Awake 단계에서 초기화되도록 1프레임 대기

        // BGM 자동 재생 (옵션)
        //SoundManager.Instance?.f_AutoPlayBGM();

        // 다음 씬으로 전환
        SceneManager.LoadScene(nextSceneName);
    }
}
