/*
 * ������ ��ü �帧�� �����ϴ� GameManager Ŭ����
 */
using UnityEngine;
using UnityEngine.SceneManagement;


//���� ���� ���� Enum
public enum GameState
{
    Ready,      //���� ��� ���� (���� ��)
    Running,    //���� ���� ��
    MiniGame,   //�̴ϰ��� �ߴ� ����
    Paused,     //�Ͻ� ���� (�ɼ� ��)
    GameOver,   //���� ����
    GameClear   //���� ����
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; } //�̱��� ���� ����
    public GameState CurrentState { get; private set; } = GameState.Ready;

    [SerializeField] private float fLimitTime = 300f; //���ѽð� (5�� = 300��)
    
    private float fCurrentTime = 0f;                  //���� ���� �ð�
    private bool isTimerRunning = false;

    public float CurrentTime { get { return fCurrentTime; } } //���� �ð��� ��ȯ�ϴ� read-only ������Ƽ

    private void Awake()
    {
        // �̱��� �ν��Ͻ� �Ҵ� �� �ߺ� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (CurrentState == GameState.Running && isTimerRunning)
        {
            fCurrentTime -= Time.deltaTime;
            if (fCurrentTime <= 0f)
            {
                fCurrentTime = 0f;
                f_OnGameOver();
            }
        }
    }

    /// <summary>
    /// ������ �����ϴ� �޼ҵ� (�ʱ�ȭ ����)
    /// </summary>
    public void f_StartGame()
    {
        fCurrentTime = fLimitTime;
        isTimerRunning = true;
        CurrentState = GameState.Running;

        Debug.Log("���� ���۵�: ���ѽð� = " + fLimitTime);
    }

    /// <summary>
    /// ��ֹ� �浹 �� �̴ϰ��� ����
    /// </summary>
    public void f_OnHitObstacle()
    {
        if (CurrentState != GameState.Running) return;

        CurrentState = GameState.MiniGame;
        isTimerRunning = false;

        Debug.Log("�̴ϰ��� ���۵� (Ÿ�̸� �Ͻ�����)");
        // TODO: MinigameManager.Instance.f_StartMinigame(OnMinigameResult);
    }

    /// <summary>
    /// �̴ϰ��� ��� �ݹ� (����/����)
    /// </summary>
    public void f_OnMinigameResult(bool isSuccess)
    {
        if (isSuccess)
        {
            Debug.Log("�̴ϰ��� ���� - ���� �簳");
        }
        else
        {
            Debug.Log("�̴ϰ��� ���� - ���Ƽ ����");
            fCurrentTime -= 10f; // ��: 10�� ���Ƽ
        }

        CurrentState = GameState.Running;
        isTimerRunning = true;
    }

    /// <summary>
    /// �÷��̾ �������� �������� �� ȣ��Ǵ� ���� Ŭ���� ó��
    /// </summary>
    public void f_OnGameClear()
    {
        isTimerRunning = false;
        CurrentState = GameState.GameClear;

        Debug.Log("���� Ŭ����!");
        // TODO: ResultUIManager.Instance.ShowClearUI(fCurrentTime);
    }

    /// <summary>
    /// ���ѽð� �ʰ��� ���� ���� ó��
    /// </summary>
    private void f_OnGameOver()
    {
        isTimerRunning = false;
        CurrentState = GameState.GameOver;

        Debug.Log("�ð� �ʰ� - ���� ����!");
        // TODO: ResultUIManager.Instance.ShowGameOverUI();
    }

    /// <summary>
    /// ���� �� �̸��� ��ȯ�ϴ� ��ƿ��Ƽ �޼ҵ�
    /// </summary>
    public string f_GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void f_AddTime(float value)
    {
        fCurrentTime += value;
        fCurrentTime = Mathf.Clamp(fCurrentTime, 0f, fLimitTime);
    }
}
