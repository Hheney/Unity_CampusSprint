/*
 * 초기화 전용 InitScene에서 실행되는 매니저 자동 로딩 스크립트
 * - SoundManager, GameManager 등 필수 매니저를 초기화한 뒤 메인 씬으로 전환
 * - 씬 전환 전 반드시 매니저 초기화가 완료되도록 1프레임 대기하도록 함
 */
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManagerLoader : MonoBehaviour 
{
    [Header("Target loading scenename")]
    [SerializeField] private string sTargetSceneName = "MainMenuScene";

    void Start()
    {
        StartCoroutine(f_LoadNextScene()); //매니저 초기화 완료 후 다음 씬 로드
    }

    /// <summary> 매니저 초기화 완료 후 다음 씬 로드 (1프레임 대기) </summary>
    private System.Collections.IEnumerator f_LoadNextScene()
    {
        //필수 매니저 초기화
        yield return null; //Manager들이 Awake 단계에서 초기화되도록 1프레임 대기

        //BGM 자동 재생
        //SoundManager.Instance?.f_AutoPlayBGM();

        //다음 씬으로 전환
        SceneManager.LoadScene(sTargetSceneName);
    }
}
