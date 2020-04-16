using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionPop : MonoBehaviour
{
    public GameObject questionMenuCanvas;
    public Board board;

    public bool isQuestioned;
    public int isResume;


    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        isResume = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isQuestioned)
        {
            questionMenuCanvas.SetActive(true);
            //Time.timeScale = 0f;
        }
        else
        {
            questionMenuCanvas.SetActive(false);
        }


    }

    public void Resume()
    {
        isQuestioned = false;
    }
}
