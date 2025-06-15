/*
 * �ʱ�ȭ ���� InitScene���� ����Ǵ� �Ŵ��� �ڵ� �ε� ��ũ��Ʈ
 * - SoundManager, GameManager �� �ʼ� �Ŵ����� �ʱ�ȭ�� �� ���� ������ ��ȯ
 * - �� ��ȯ �� �ݵ�� �Ŵ��� �ʱ�ȭ�� �Ϸ�ǵ��� 1������ ����ϵ��� ��
 */
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManagerLoader : MonoBehaviour 
{
    [Header("Target loading scenename")]
    [SerializeField] private string sTargetSceneName = "MainMenuScene";

    void Start()
    {
        StartCoroutine(f_LoadNextScene()); //�Ŵ��� �ʱ�ȭ �Ϸ� �� ���� �� �ε�
    }

    /// <summary> �Ŵ��� �ʱ�ȭ �Ϸ� �� ���� �� �ε� (1������ ���) </summary>
    private System.Collections.IEnumerator f_LoadNextScene()
    {
        //�ʼ� �Ŵ��� �ʱ�ȭ
        yield return null; //Manager���� Awake �ܰ迡�� �ʱ�ȭ�ǵ��� 1������ ���

        //BGM �ڵ� ���
        //SoundManager.Instance?.f_AutoPlayBGM();

        //���� ������ ��ȯ
        SceneManager.LoadScene(sTargetSceneName);
    }
}
