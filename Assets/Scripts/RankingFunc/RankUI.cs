/*
 * [RankUI ��ũ��Ʈ]
 * - RankManager�� �����͸� �������� UI�� ����ϴ� Ŭ����
 * - ���� ��ŷ 10���� �ؽ�Ʈ ���Կ� �ڵ� ����ϴ� ����� ������
 */
using UnityEngine;
using TMPro;

public class RankUI : MonoBehaviour
{
    [Header("��ŷ ���� �ؽ�Ʈ (1~10��)")]
    [SerializeField] private TMP_Text[] txtRankSlots = null; //��ŷ ������ ǥ���� �ؽ�Ʈ �迭

    [Header("��ŷ �г� ������Ʈ")]
    [SerializeField] private GameObject gRankPanel = null;

    private void Awake()
    {
        //���� ���� �� ��ŷ �г� ��Ȱ��ȭ
        if (gRankPanel != null)
        {
            gRankPanel.SetActive(false);
        }
    }

    public void f_RefreshRankUI()
    {
        var rankList = RankManager.Instance?.RankList; //RankManager�� ��ũ ����Ʈ�� ������

        if (rankList == null) //��ũ ����Ʈ�� null�� ���
        {
            Debug.LogWarning("[RankUI] RankList is null."); //��� �޽��� ���
            return;
        }

        for (int i = 0; i < txtRankSlots.Length; i++) //��ŷ ������ ������ŭ �ݺ�
        {
            if (i < rankList.Count)
            {
                string sPlayerName = rankList[i].PlayerName;   //��ũ ����Ʈ���� �÷��̾� �̸��� ������
                int nScore = rankList[i].Score;          //��ũ ����Ʈ���� ������ ������
                txtRankSlots[i].text = $"{i + 1}�� - {sPlayerName} : {nScore}��"; //��ŷ ���Կ� �÷��̾� �̸��� ������ ǥ��
            }
            else
            {
                txtRankSlots[i].text = $"{i + 1}�� - ---"; //��ũ ����Ʈ�� �ش� ������ ������ '---'�� ǥ��
            }
        }
    }

    /// <summary> ��ŷ �г��� ���� UI�� ���� ��ħ�� </summary>
    public void f_OpenRankPanel()
    {
        gRankPanel.SetActive(true); //��ŷ �г��� Ȱ��ȭ
        f_RefreshRankUI(); //��ŷ UI�� ���� ��ħ�Ͽ� �ֽ� ��ŷ�� ǥ��

        SoundManager.Instance?.f_PlaySFX(SoundName.SFX_POP, 1.0f); //�˾� ȿ���� ���
    }

    /// <summary> ��ŷ �г��� ���� </summary>
    public void f_CloseRankPanel()
    {
        gRankPanel.SetActive(false); //��ŷ �г��� ��Ȱ��ȭ

        SoundManager.Instance?.f_PlaySFX(SoundName.SFX_POP, 1.0f); //�˾� ȿ���� ���
    }
}