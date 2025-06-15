/*
 * ���� Ŭ���� ����� �����ִ� UI ��Ʈ�ѷ� ��ũ��Ʈ
 * �ð� ��� ���� ���, Ŭ���� �г��� ǥ���ϴ� ���
 */
using UnityEngine;
using TMPro;

public class GameResultUI : MonoBehaviour
{
    public static GameResultUI Instance { get; private set; } //�̱��� �ν��Ͻ�

    [Header("Ŭ���� UI")]
    [SerializeField] private GameObject gClearPanel = null;     //Ŭ���� �г� ������Ʈ
    [SerializeField] private TMP_InputField inputPlayerName;    //�÷��̾� �̸� �Է� �ʵ�
    [SerializeField] private TMP_Text txtScore = null;          //���� �ؽ�Ʈ

    private int nLastScore = 0;         //������ ���� ����� ����
    private bool isScoreSaved = false;  //���� ���� ���� �÷���(�ߺ� ����)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); //�̹� �ν��Ͻ��� �����ϸ� ���� ������Ʈ�� �ı�
        }

        gClearPanel.SetActive(false); //���� �� �г� ��Ȱ��ȭ
    }

    /// <summary> ���� Ŭ���� UI�� ����ϴ� �޼ҵ� </summary>
    public void f_ShowClearUI(int nScore)
    {
        gClearPanel.SetActive(true);            //Ŭ���� �г� Ȱ��ȭ

        nLastScore = nScore; //������ ���� ����
        txtScore.text = $"���� : {nScore}";     //���� ���
        inputPlayerName.text = "Player"; //�Է��� ���� ��� �⺻�� �̸��� "Player"����
        isScoreSaved = false; //���� ���� ���� �ʱ�ȭ
    }

    public void f_OnRetryGame()
    {
        f_SaveScoreAndName(); //�г��Ӱ� ���� ����
        //GameManager.Instance?.f_StartGame(); //���� ����� *�ʱ�ȭ ���� ���׷� ���ؼ� �ּ�ó�� �� �ٸ� �������

        //TODO : ���� Bamsongi ���ӿ����� ���������� �Ŵ����� ������ ������ ���� ���װ� �߻���. ������ ������ �ʿ���.
        //GameManager�� f_StartGame() �޼ҵ带 ȣ���ϴ� ��� FlowManager�� ���� ���� ������ϵ��� ����

        FlowManager.Instance?.f_SetRetryMode(true); //����� ��� ����
        FlowManager.Instance?.f_OpenScene(SceneName.MainMenuScene); //���� �޴��� �̵�
    }

    public void f_OnMainMenu()
    {
        f_SaveScoreAndName(); //�г��Ӱ� ���� ����

        FlowManager.Instance?.f_SetRetryMode(false); //����� ��� ��Ȱ��ȭ
        FlowManager.Instance?.f_OpenScene(SceneName.MainMenuScene); //���� �޴��� �̵�
    }

    /// <summary> �г��Ӱ� ������ ��ũ�Ŵ����� �����ϴ� �޼ҵ� </summary>
    private void f_SaveScoreAndName()
    {
        if (isScoreSaved) return; //�̹� ������ ����Ǿ����� �ߺ� ���� ������ ���� Eaerly Return
        isScoreSaved = true; //���� ���� �÷��� ����

        //Trim()�� ���ڿ�(string)�� ���� ���� �ִ� ����(�����̽�, ��, �ٹٲ� ��)�� �����ϴ� �޼ҵ�
        //����� ������ ���� ��츦 ����Ͽ� Trim() �����(���� ����)
        string playerName = inputPlayerName.text.Trim();
        if (string.IsNullOrEmpty(playerName)) //�Էµ� �̸��� ����ְų� null�� ���
        {
            playerName = "Player"; //�⺻������ "Player" ����
        }

        RankManager.Instance?.f_AddRank(playerName, nLastScore); //��ũ �Ŵ����� ������ �̸� ����
    }

}
