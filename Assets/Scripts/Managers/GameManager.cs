/*
 * ������ ��ü �帧�� �����ϴ� GameManager Ŭ����
 */
using UnityEngine;
using UnityEngine.SceneManagement;


//������ ���¸� �����ϴ� ������
public enum GameState
{
    Ready,      //���� ��� ���� (���� ��)
    Running,    //���� ���� ��
    MiniGame,   //�̴ϰ��� �ߴ� ����
    Paused,     //�Ͻ� ���� (�ɼ� ��)
    GameOver,   //���� ����
    GameClear   //���� ����
}

public class GameManager : MonoBehaviour //������ ��ü �帧�� �����ϴ� GameManager Ŭ����
{
    public static GameManager Instance { get; private set; } //�̱��� ���� ����
    public GameState CurrentState { get; private set; } = GameState.Ready; //���� ���� ���� (�ʱⰪ: Ready)
    [SerializeField] private float fLimitTime = 300.0f; //���ѽð� (5�� = 300��)
    
    private float fCurrentTime = 0.0f;      //���� ���� �ð�
    private bool isTimerRunning = false;    //Ÿ�̸� �۵� ����

    //���� ���� �ð��� ��ȯ�ϴ� read-only ������Ƽ
    public float CurrentTime { get { return fCurrentTime; } }

    private void Awake()
    {
        // �̱��� �ν��Ͻ� �Ҵ� �� �ߺ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //�� ��ȯ �ÿ��� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); //�̹� �ν��Ͻ��� �����ϸ� ���� ������Ʈ�� �ı�
        }
    }

    void Update()
    {
        if (CurrentState == GameState.Running && isTimerRunning) //������ ���� ���̰� Ÿ�̸Ӱ� �۵� ���� ��
        {
            fCurrentTime -= Time.deltaTime;     //�����Ӵ� �ð� ����
            if (fCurrentTime <= 0f)             //�ð��� 0 ���Ϸ� ���������
            {
                fCurrentTime = 0f;      //�ð��� 0���� ����
                f_OnGameOver();         //���� ���� ó��
            }
        }
    }

    /// <summary> ������ �����ϴ� �޼ҵ� (�ʱ�ȭ ����) </summary>
    public void f_StartGame()
    {
        fCurrentTime = fLimitTime; //���ѽð����� �ʱ�ȭ
        isTimerRunning = true;     //Ÿ�̸� �۵� ����
        CurrentState = GameState.Running; //���� ���¸� Running���� ����

        Debug.Log("���� ���۵�: ���ѽð� = " + fLimitTime); //���� ���� �α� ���
    }

    /// <summary> ��ֹ� �浹 �� �̴ϰ��� ���� </summary>
    public void f_OnHitObstacle()
    {
        if (CurrentState != GameState.Running) return; //������ ���� ���� �ƴ� ��� ����

        CurrentState = GameState.MiniGame; //���� ���¸� MiniGame���� ����
        isTimerRunning = false; //Ÿ�̸� �Ͻ� ����

        Debug.Log("�̴ϰ��� ���۵� (Ÿ�̸� �Ͻ�����)");
        MiniGameUI.Instance?.f_OpenMiniGame(); //�̴ϰ��� UI ����
    }

    /// <summary> �̴ϰ��� ��� �ݹ� (����/����) </summary>
    public void f_OnMinigameResult(bool isSuccess)
    {
        if (isSuccess) //�̴ϰ��� ���� ��
        {
            Debug.Log("�̴ϰ��� ���� - ���� �簳");
        }
        else
        {
            Debug.Log("�̴ϰ��� ���� - ���Ƽ ����");
            fCurrentTime -= 10f; // ��: 10�� ���Ƽ
        }

        CurrentState = GameState.Running; //���� ���¸� Running���� ����
        isTimerRunning = true; //Ÿ�̸� �簳
    }

    /// <summary> �÷��̾ �������� �������� �� ȣ��Ǵ� ���� Ŭ���� ó�� </summary>
    public void f_OnGameClear()
    {
        isTimerRunning = false; //Ÿ�̸� ����
        CurrentState = GameState.GameClear; //���� ���¸� GameClear�� ����

        Debug.Log("���� Ŭ����!");
        //TODO: ResultUIManager.Instance.ShowClearUI(fCurrentTime);
    }

    /// <summary> ���ѽð� �ʰ��� ���� ���� ó�� </summary>
    private void f_OnGameOver()
    {
        isTimerRunning = false; //Ÿ�̸� ����
        CurrentState = GameState.GameOver; //���� ���¸� GameOver�� ����

        Debug.Log("�ð� �ʰ� - ���� ����!");
        //TODO: ResultUIManager.Instance.ShowGameOverUI();
    }

    /// <summary> ���ѽð��� �������� �߰��ϴ� �޼ҵ� </summary>
    public void f_AddTime(float value) 
    {
        fCurrentTime += value; //���� �ð��� �߰����� ����
        fCurrentTime = Mathf.Clamp(fCurrentTime, 0f, fLimitTime); //�ִ� ���ѽð��� ���� �ʵ��� Ŭ���� �޼ҵ带 ����
    }
}
