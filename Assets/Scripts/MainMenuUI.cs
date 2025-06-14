//���� �޴� UI�� �����ϴ� ��ũ��Ʈ
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("��ư ������Ʈ ����")]
    [SerializeField] private Button startButton = null; //���� ���� ��ư
    [SerializeField] private Button exitButton = null;  //���� ���� ��ư

    [Header("���� �� ���� (Enum ���)")]
    [SerializeField] private SceneName gameScene = SceneName.GameScene; //���� �� �̸� (Enum���� ����)

    void Start()
    {
        //��ư Ŭ�� �̺�Ʈ ���
        if (startButton != null)
            startButton.onClick.AddListener(f_OnClickStart); //���� ���� ��ư Ŭ�� �� f_OnClickStart �޼ҵ� ȣ��

        if (exitButton != null)
            exitButton.onClick.AddListener(f_OnClickExit); //���� ���� ��ư Ŭ�� �� f_OnClickExit �޼ҵ� ȣ��
    }

    /// <summary> ���� ���� ��ư Ŭ�� �� FlowManager�� ���� ���� ������ ��ȯ </summary>
    private void f_OnClickStart()
    {
        Debug.Log("���� ����");
        FlowManager.Instance?.f_OpenScene(gameScene); //FlowManager�� ���� ������ ���� ������ ��ȯ
    }

    /// <summary> ���� ��ư Ŭ�� �� ���ø����̼� ���� </summary>
    private void f_OnClickExit()
    {
        Debug.Log("���� ����");

        //������ ����(������ �󿡼� ���α׷��� ����Ǳ� ������ ������ ������ ����)
        UnityEditor.EditorApplication.isPlaying = false;
    }
}