using UnityEngine;

/// <summary>
///  �ݺ� ��� ī��Ʈ�� �����ϴ� ClearCounterManager Ŭ����
/// </summary>
public class ClearCounterManager : MonoBehaviour
{
    public static ClearCounterManager Instance { get; private set; }

    private int nLoopCount = 0; //�ݺ� ��� ī��Ʈ

    public int LoopCount { get { return nLoopCount; } } //�б� ���� ������Ƽ, �ܺο��� ī��Ʈ �� ��������

    private void Awake()
    {
        // �̱��� �ν��Ͻ� �Ҵ� �� �ߺ� ����
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); //�̹� �ν��Ͻ��� �����ϸ� ���� ������Ʈ�� �ı�
        }
    }

    public void f_AddCount()
    {
        nLoopCount++;
        Debug.Log($"�ݺ� ��� ī��Ʈ: {nLoopCount}");
    }
}
