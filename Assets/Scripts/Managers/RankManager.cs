/*
 * RankManager 스크립트, 랭킹 시스템을 관리하는 클래스
 * - 점수 등록 기능
 * - 정렬된 랭킹 리스트 유지 기능
 * - JSON 파일로 저장/불러오기 기능
 */
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Overlays;
using UnityEngine;
using System.Security.Cryptography; //AES 암호화를 위해 사용(TODO)

/*
 * 직렬화(Serialization)
 * 오브젝트를 저장하거나 전송할 수 있는 형태로 변환하는 과정을 의미함
 * 이 랭크매니저에서는 JSON 파일 형식으로 전환하기 위해 직렬화 개념을 사용함
 */

///<summary>개별 랭킹 정보 저장 클래스</summary>
[System.Serializable]
public class RankData
{
    [SerializeField] private string sPlayerName = null; //플레이어 이름
    [SerializeField] private int nScore = 0;            //획득한 점수

    //프로퍼티
    public string PlayerName { get { return sPlayerName; } } //플레이어 이름을 반환하는 프로퍼티
    public int Score { get { return nScore; } } //획득한 점수를 반환하는 프로퍼티

    public RankData(string UserName, int UserScore)
    {
        sPlayerName = UserName; //플레이어 이름 초기화
        nScore = UserScore;     //획득한 점수 초기화
    }
}

///<summary> 랭크 리스트를 JSON으로 직렬화하기 위한 래퍼 클래스 </summary>
[System.Serializable]
public class RankListWrapper
{
    [SerializeField] private List<RankData> list; //랭크 데이터 리스트

    //프로퍼티
    public List<RankData> RankList { get { return list; } } //랭크 리스트를 반환하는 프로퍼티

    public RankListWrapper(List<RankData> rankList)
    {
        list = rankList; //랭크 리스트 초기화
    }
}

///<summary>JSON 파일로 랭크를 관리하는 랭크 매니저 클래스</summary>
public class RankManager : MonoBehaviour
{
    [SerializeField] private List<RankData> rankList = new List<RankData>(); //랭크 데이터를 저장할 리스트

    private string SavePath => Path.Combine(Application.persistentDataPath, "Rank.json"); //저장 경로 Read-Only 필드

    public IReadOnlyList<RankData> RankList => rankList.AsReadOnly(); //외부에서는 Read-Only 랭크 리스트

    private static RankManager _instance = null; //싱글톤 인스턴스

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
        DontDestroyOnLoad(gameObject); //씬이 변경되어도 현재 게임 오브젝트를 유지시키는 메소드

        f_LoadRank(); //시작과 동시에 랭크를 로드

        //랭크 저장 경로 출력
        Debug.Log("[RankManager] JSON 저장 경로: " + Application.persistentDataPath);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>랭크 데이터를 JSON으로 저장하는 메소드</summary>
    private void f_SaveRank()
    {
        string json = JsonUtility.ToJson(new RankListWrapper(rankList), true);
        File.WriteAllText(SavePath, json);
    }

    /// <summary>저장된 JSON 파일로부터 랭크 데이터를 불러오는 메소드</summary>
    private void f_LoadRank()
    {
        if (File.Exists(SavePath)) //저장된 JSON 파일이 존재하는가?
        {
            string json = File.ReadAllText(SavePath);
            RankListWrapper wrapper = JsonUtility.FromJson<RankListWrapper>(json); //JSON 파일을 래퍼 클래스로 역직렬화
            if (wrapper?.RankList != null)
            {
                rankList = wrapper.RankList; //RankList를 래퍼로부터 가져옴
            }
        }
    }

    /// <summary>
    /// 새로운 랭크 데이터를 추가하고 리스트를 정렬 및 저장하는 메소드
    /// </summary>
    /// <param name="sPlayerName">플레이어 이름</param>
    /// <param name="nScore">획득한 점수</param>
    public void f_AddRank(string sPlayerName, int nScore)
    {
        rankList.Add(new RankData(sPlayerName, nScore));        //새로운 랭크 데이터를 리스트에 추가
        rankList.Sort((a, b) => b.Score.CompareTo(a.Score));    //점수를 기준으로 내림차순 정렬

        if (rankList.Count > 10) //랭크 리스트가 10개를 초과하는 경우
        {
            rankList.RemoveAt(rankList.Count - 1); //상위 10개만 유지
        }

        f_SaveRank(); //변경된 랭크 리스트를 JSON 파일로 저장
    }
}

