//���� �޴� UI�� �����ϴ� ��ũ��Ʈ
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("��ư ������Ʈ ����")]
    [SerializeField] private Button BtnStart = null; //���� ���� ��ư
    [SerializeField] private Button BtnExit = null;  //���� ���� ��ư

    [Header("���� �� ���� (Enum ���)")]
    [SerializeField] private SceneName gameScene = SceneName.GameScene; //���� �� �̸� (Enum���� ����)

    void Start()
    {
        //���� ����� ��尡 Ȱ��ȭ�Ǿ� �ִٸ�, FlowManager�� ���� ���� ������ �ڵ� ��ȯ
        if (FlowManager.Instance != null && FlowManager.Instance.IsRetryMode)
        {
            FlowManager.Instance.f_SetRetryMode(false); //���� ����� ��� �ʱ�ȭ
            f_OnClickStart(); //�ڵ����� ���� �� ����
            return;
        }

        //��ư Ŭ�� �̺�Ʈ ���
        if (BtnStart != null)
            BtnStart.onClick.AddListener(f_OnClickStart); //���� ���� ��ư Ŭ�� �� f_OnClickStart �޼ҵ� ȣ��

        if (BtnExit != null)
            BtnExit.onClick.AddListener(f_OnClickExit); //���� ���� ��ư Ŭ�� �� f_OnClickExit �޼ҵ� ȣ��

        SoundManager.Instance?.f_AutoPlayBGM(); //SoundManager�� ���� �ڵ����� BGM ���
    }

    /// <summary> ���� ���� ��ư Ŭ�� �� FlowManager�� ���� ���� ������ ��ȯ </summary>
    private void f_OnClickStart()
    {
        Debug.Log("���� ����");
        SoundManager.Instance?.f_PlaySFX(SoundName.SFX_POP, 1.0f); //�˾� ȿ���� ���
        SoundManager.Instance?.f_StopAllBGM(); //��� BGM ����
        FlowManager.Instance?.f_OpenScene(gameScene); //FlowManager�� ���� ������ ���� ������ ��ȯ
    }

    /// <summary> ���� ��ư Ŭ�� �� ���ø����̼� ���� </summary>
    private void f_OnClickExit()
    {
        Debug.Log("���� ����");
        SoundManager.Instance?.f_PlaySFX(SoundName.SFX_POP, 1.0f); //�˾� ȿ���� ���
        SoundManager.Instance?.f_StopAllBGM(); //��� BGM ����
        //������ ����(������ �󿡼� ���α׷��� ����Ǳ� ������ ������ ������ ����)
        UnityEditor.EditorApplication.isPlaying = false;
    }
}