using UnityEngine;

[System.Serializable]
public class QuizData : MonoBehaviour
{
    public string sQuestion;    //���� �ؽ�Ʈ
    public bool isAnswer;       //���� ���� (true: O, false: X)

    public QuizData(string sQuestion, bool isAnswer)
    {
        this.sQuestion = sQuestion; //���� �ؽ�Ʈ �ʱ�ȭ
        this.isAnswer = isAnswer;   //���� ���� �ʱ�ȭ
    }
}
