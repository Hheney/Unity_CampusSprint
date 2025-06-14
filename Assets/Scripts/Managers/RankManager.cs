/*
 * ������ �����ϴ� ��ũ�Ŵ���
 */
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using System.Security.Cryptography; //AES ��ȣȭ�� ���� ���

/*
 * ����ȭ(Serialization)
 * ������Ʈ�� �����ϰų� ������ �� �ִ� ���·� ��ȯ�ϴ� ������ �ǹ���
 * �� ��ũ�Ŵ��������� JSON ���� �������� ��ȯ�ϱ� ���� ����ȭ ������ �����
 */

///<summary>���� ��ŷ ���� ���� Ŭ����</summary>
[System.Serializable]
public class RankData
{
    [SerializeField] private string sPlayerName = null;
    [SerializeField] private int nScore = 0;

    //������Ƽ
    public string PlayerName { get { return sPlayerName; } }
    public int Score { get { return nScore; } }

    public RankData(string UserName, int UserScore)
    {
        sPlayerName = UserName;
        nScore = UserScore;
    }
}

///<summary>����ȭ�� ���� ��ũ ����Ʈ ���� Ŭ����</summary>
[System.Serializable]
public class RankListWrapper
{
    [SerializeField] private List<RankData> list;

    //������Ƽ
    public List<RankData> RankList { get { return list; } }

    public RankListWrapper(List<RankData> rankList)
    {
        list = rankList;
    }
}

///<summary>JSON ���Ϸ� ��ũ�� �����ϴ� ��ũ �Ŵ��� Ŭ����</summary>
public class RankManager : MonoBehaviour
{
    [SerializeField] private List<RankData> rankList = new List<RankData>();

    private string SavePath => Path.Combine(Application.persistentDataPath, "Rank.json"); //���� ��� Read-Only �ʵ�

    public IReadOnlyList<RankData> RankList => rankList.AsReadOnly(); //�ܺο����� Read-Only ��ũ ����Ʈ

    private static RankManager _instance = null;

    public static RankManager Instance
    {
        get
        {
            if (_instance == null) Debug.Log("RankManager is null.");
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Debug.Log("RankManager has another instance.");
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject); //���� ����Ǿ ���� ���� ������Ʈ�� ������Ű�� �޼ҵ�

        f_LoadRank(); //���۰� ���ÿ� ��ũ�� �ε�
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>��ũ �����͸� JSON���� �����ϴ� �޼ҵ�</summary>
    private void f_SaveRank()
    {
        string json = JsonUtility.ToJson(new RankListWrapper(rankList), true);
        File.WriteAllText(SavePath, json);
    }

    /// <summary>����� JSON ���Ϸκ��� ��ũ �����͸� �ҷ����� �޼ҵ�</summary>
    private void f_LoadRank()
    {
        if (File.Exists(SavePath))
        {
            string json = File.ReadAllText(SavePath);
            RankListWrapper wrapper = JsonUtility.FromJson<RankListWrapper>(json);
            if (wrapper?.RankList != null)
            {
                rankList = wrapper.RankList;
            }
        }
    }

    /// <summary>
    /// ���ο� ��ũ �����͸� �߰��ϰ� ����Ʈ�� ���� �� �����ϴ� �޼ҵ�
    /// </summary>
    /// <param name="playerName">�÷��̾� �̸�</param>
    /// <param name="score">ȹ���� ����</param>
    public void f_AddRank(string playerName, int score)
    {
        rankList.Add(new RankData(playerName, score));
        rankList.Sort((a, b) => b.Score.CompareTo(a.Score)); //���� ��������

        if (rankList.Count > 10)
        {
            rankList.RemoveAt(rankList.Count - 1); //���� 10���� ����
        }

        f_SaveRank();
    }
}

