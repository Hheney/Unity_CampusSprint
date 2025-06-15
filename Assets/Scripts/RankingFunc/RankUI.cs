/*
 * [RankUI 스크립트]
 * - RankManager의 데이터를 바탕으로 UI에 출력하는 클래스
 * - 상위 랭킹 10개를 텍스트 슬롯에 자동 출력하는 기능을 수행함
 */
using UnityEngine;
using TMPro;

public class RankUI : MonoBehaviour
{
    [Header("랭킹 슬롯 텍스트 (1~10위)")]
    [SerializeField] private TMP_Text[] txtRankSlots = null; //랭킹 슬롯을 표시할 텍스트 배열

    [Header("랭킹 패널 오브젝트")]
    [SerializeField] private GameObject gRankPanel = null;

    private void Awake()
    {
        //게임 시작 시 랭킹 패널 비활성화
        if (gRankPanel != null)
        {
            gRankPanel.SetActive(false);
        }
    }

    public void f_RefreshRankUI()
    {
        var rankList = RankManager.Instance?.RankList; //RankManager의 랭크 리스트를 가져옴

        if (rankList == null) //랭크 리스트가 null인 경우
        {
            Debug.LogWarning("[RankUI] RankList is null."); //경고 메시지 출력
            return;
        }

        for (int i = 0; i < txtRankSlots.Length; i++) //랭킹 슬롯의 개수만큼 반복
        {
            if (i < rankList.Count)
            {
                string sPlayerName = rankList[i].PlayerName;   //랭크 리스트에서 플레이어 이름을 가져옴
                int nScore = rankList[i].Score;          //랭크 리스트에서 점수를 가져옴
                txtRankSlots[i].text = $"{i + 1}위 - {sPlayerName} : {nScore}점"; //랭킹 슬롯에 플레이어 이름과 점수를 표시
            }
            else
            {
                txtRankSlots[i].text = $"{i + 1}위 - ---"; //랭크 리스트에 해당 순위가 없으면 '---'로 표시
            }
        }
    }

    /// <summary> 랭킹 패널을 열고 UI를 새로 고침함 </summary>
    public void f_OpenRankPanel()
    {
        gRankPanel.SetActive(true); //랭킹 패널을 활성화
        f_RefreshRankUI(); //랭킹 UI를 새로 고침하여 최신 랭킹을 표시

        SoundManager.Instance?.f_PlaySFX(SoundName.SFX_POP, 1.0f); //팝업 효과음 재생
    }

    /// <summary> 랭킹 패널을 닫음 </summary>
    public void f_CloseRankPanel()
    {
        gRankPanel.SetActive(false); //랭킹 패널을 비활성화

        SoundManager.Instance?.f_PlaySFX(SoundName.SFX_POP, 1.0f); //팝업 효과음 재생
    }
}