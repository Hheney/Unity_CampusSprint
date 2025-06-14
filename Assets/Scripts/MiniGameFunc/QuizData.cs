using UnityEngine;

[System.Serializable]
public class QuizData : MonoBehaviour
{
    public string sQuestion;    //���� �ؽ�Ʈ
    public bool isAnswer;       //���� ���� (true: O, false: X)

    public QuizData(string sQuestion, bool isAnswer)
    {
        this.sQuestion = sQuestion;
        this.isAnswer = isAnswer;
    }
}
