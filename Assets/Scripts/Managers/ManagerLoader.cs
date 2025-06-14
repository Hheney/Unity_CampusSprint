using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �ʱ�ȭ ���� InitScene���� ����Ǵ� �Ŵ��� �ڵ� �δ� ��ũ��Ʈ
/// - SoundManager, GameManager �� �ʼ� �Ŵ����� �ʱ�ȭ�� �� ���� ������ ��ȯ
/// - �� ��ȯ �� �ݵ�� �Ŵ��� �ʱ�ȭ�� �Ϸ�ǵ��� 1������ ���
/// </summary>
public class ManagerLoader : MonoBehaviour
{
    [Header("Target loading scenename")]
    [SerializeField] private string nextSceneName = "MainMenuScene";

    private void Start()
    {
        // Start ������ �ڷ�ƾ���� ���� �� ��ȯ
        StartCoroutine(f_LoadNextScene());
    }

    /// <summary>
    /// �Ŵ��� �ʱ�ȭ �Ϸ� �� ���� �� �ε� (1������ ���)
    /// </summary>
    private System.Collections.IEnumerator f_LoadNextScene()
    {
        yield return null; // �Ŵ������� Awake �ܰ迡�� �ʱ�ȭ�ǵ��� 1������ ���

        // BGM �ڵ� ��� (�ɼ�)
        //SoundManager.Instance?.f_AutoPlayBGM();

        // ���� ������ ��ȯ
        SceneManager.LoadScene(nextSceneName);
    }
}
