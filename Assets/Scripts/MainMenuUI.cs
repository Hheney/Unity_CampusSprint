using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� �޴����� ��ư �Է��� ó���ϴ� ���� ��ũ��Ʈ (FlowManager ��� ����)
/// </summary>
public class MainMenuUI : MonoBehaviour
{
    [Header("��ư ������Ʈ ����")]
    [SerializeField] private Button startButton = null;
    [SerializeField] private Button exitButton = null;

    [Header("���� �� ���� (Enum ���)")]
    [SerializeField] private SceneName gameScene = SceneName.GameScene;

    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ ���
        if (startButton != null)
            startButton.onClick.AddListener(f_OnClickStart);

        if (exitButton != null)
            exitButton.onClick.AddListener(f_OnClickExit);
    }

    /// <summary>
    /// ���� ���� ��ư Ŭ�� �� FlowManager�� ���� ���� ������ ��ȯ
    /// </summary>
    private void f_OnClickStart()
    {
        Debug.Log("���� ����");
        FlowManager.Instance?.f_OpenScene(gameScene);
    }

    /// <summary>
    /// ���� ��ư Ŭ�� �� ���ø����̼� ����
    /// </summary>
    private void f_OnClickExit()
    {
        Debug.Log("���� ����");

        //������ ����(������ �󿡼� ���α׷��� ����Ǳ� ������ ������ ������ ����)
        UnityEditor.EditorApplication.isPlaying = false;
    }


}