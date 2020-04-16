using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unansweredQuestions;

    private Question currentQuestion;
    private QuestionPop questionPop;
    

    [SerializeField]
    private Text vraagText;

    [SerializeField]
    private Text AntwoordText1;

    [SerializeField]
    private Text AntwoordText2;

    [SerializeField]
    private Text AntwoordText3;

    [SerializeField]
    private Text AntwoordText4;

    [SerializeField]
    private Text CorrectIncorrectText;

    [SerializeField]
    private Text IncorrectText;

    [SerializeField]
    private Animator Animator;

    [SerializeField]
    private float tijdNaVraag = 0f;


    void Start ()
    {
        questionPop = FindObjectOfType<QuestionPop>();
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        SetCurrentQuestion();
        
    }

    void SetCurrentQuestion()
    {
        int randQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randQuestionIndex];

        var antwoorden = new List<string> { currentQuestion.correctAntwoord, currentQuestion.foutAntwoord1, currentQuestion.foutAntwoord2, currentQuestion.foutAntwoord3 };
        
        vraagText.text = currentQuestion.vraag;

        int randAntIndex = Random.Range(0, antwoorden.Count);
        AntwoordText1.text = antwoorden[randAntIndex];
        antwoorden.RemoveAt(randAntIndex);
        randAntIndex = Random.Range(0, antwoorden.Count);
        AntwoordText2.text = antwoorden[randAntIndex];
        antwoorden.RemoveAt(randAntIndex);
        randAntIndex = Random.Range(0, antwoorden.Count);
        AntwoordText3.text = antwoorden[randAntIndex];
        antwoorden.RemoveAt(randAntIndex);
        randAntIndex = Random.Range(0, antwoorden.Count);
        AntwoordText4.text = antwoorden[randAntIndex];
        antwoorden.RemoveAt(randAntIndex);

        unansweredQuestions.RemoveAt(randQuestionIndex);
    }

    //IEnumerator nextScene ()
    //{
    //    yield return new WaitForSeconds(tijdNaVraag);

    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    //}

    public void button1 ()
    {
        if (AntwoordText1.text == currentQuestion.correctAntwoord)
        {
            Debug.Log("CORRECT");
            CorrectIncorrectText.text = "CORRECT";
            IncorrectText.text = "";
        }
        else
        {
            Debug.Log("FALSE");
            CorrectIncorrectText.text = "FOUT";
            IncorrectText.text = "Het goede antwoord was " + currentQuestion.correctAntwoord;
        }

        Animator.SetTrigger("AntwoordGegeven");
    }

    public void button2()
    {
        if (AntwoordText2.text == currentQuestion.correctAntwoord)
        {
            Debug.Log("CORRECT");
            CorrectIncorrectText.text = "CORRECT";
            IncorrectText.text = "";
        }
        else
        {
            Debug.Log("FALSE");
            CorrectIncorrectText.text = "FOUT";
            IncorrectText.text = "Het goede antwoord was " + currentQuestion.correctAntwoord;
        }

        Animator.SetTrigger("AntwoordGegeven");
    }

    public void button3()
    {
        if (AntwoordText3.text == currentQuestion.correctAntwoord)
        {
            Debug.Log("CORRECT");
            CorrectIncorrectText.text = "CORRECT";
            IncorrectText.text = "";
        }
        else
        {
            Debug.Log("FALSE");
            CorrectIncorrectText.text = "FOUT";
            IncorrectText.text = "Het goede antwoord was " + currentQuestion.correctAntwoord;
        }

        Animator.SetTrigger("AntwoordGegeven");
    }

    public void button4()
    {
        if (AntwoordText4.text == currentQuestion.correctAntwoord)
        {
            Debug.Log("CORRECT");
            CorrectIncorrectText.text = "CORRECT";
            IncorrectText.text = "";
        }
        else
        {
            Debug.Log("FALSE");
            CorrectIncorrectText.text = "FOUT";
            IncorrectText.text = "Het goede antwoord was " + currentQuestion.correctAntwoord;
        }

        Animator.SetTrigger("AntwoordGegeven");
    }

    public void Button5()
    {
        questionPop.isQuestioned = false;
        questionPop.isResume = 1;
    }
}
