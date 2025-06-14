using UnityEngine;

[System.Serializable]
public class QuizData : MonoBehaviour
{
    public string sQuestion;    //문제 텍스트
    public bool isAnswer;       //정답 여부 (true: O, false: X)

    public QuizData(string sQuestion, bool isAnswer)
    {
        this.sQuestion = sQuestion;
        this.isAnswer = isAnswer;
    }
}
