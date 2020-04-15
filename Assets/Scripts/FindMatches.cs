using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FindMatches : MonoBehaviour
{
    private Board board;
    public List<GameObject> currentMatches = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
    }

    public void FindallMatches()
    {
        StartCoroutine(FindAllMatchesCo());
    }

    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(.2f);
        for(int i = 0; i < board.width; i++)
        {
            for(int j = 0; j < board.height; j++)
            {
                GameObject currentIcon = board.allIcons[i, j];
                if(currentIcon != null)
                {
                    if(i > 0 && i < board.width - 1)
                    {
                        GameObject leftIcon = board.allIcons[i - 1, j];
                        GameObject rightIcon = board.allIcons[i + 1, j];
                        if(leftIcon != null && rightIcon != null)
                        {
                            if (leftIcon.tag == currentIcon.tag && rightIcon.tag == currentIcon.tag)
                            {
                                if (currentIcon.GetComponent<Icon>().isRowBomb || leftIcon.GetComponent<Icon>().isRowBomb || rightIcon.GetComponent<Icon>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j));
                                }

                                if (currentIcon.GetComponent<Icon>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i));
                                }

                                if (leftIcon.GetComponent<Icon>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i - 1));
                                }

                                if (rightIcon.GetComponent<Icon>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i + 1));
                                }
                                if (!currentMatches.Contains(leftIcon))
                                {
                                    currentMatches.Add(leftIcon);
                                }
                                leftIcon.GetComponent<Icon>().isMatched = true;
                                if (!currentMatches.Contains(rightIcon))
                                {
                                    currentMatches.Add(rightIcon);
                                }
                                rightIcon.GetComponent<Icon>().isMatched = true;
                                if (!currentMatches.Contains(currentIcon))
                                {
                                    currentMatches.Add(currentIcon);
                                }
                                currentIcon.GetComponent<Icon>().isMatched = true;
                            }
                        }
                    }
                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upIcon = board.allIcons[i, j + 1];
                        GameObject downIcon = board.allIcons[i, j - 1];
                        if (upIcon != null && downIcon != null)
                        {
                            if (upIcon.tag == currentIcon.tag && downIcon.tag == currentIcon.tag)
                            {
                                if (currentIcon.GetComponent<Icon>().isColumnBomb || upIcon.GetComponent<Icon>().isColumnBomb || downIcon.GetComponent<Icon>().isColumnBomb)
                                {
                                    currentMatches.Union(GetColumnPieces(i));
                                }

                                if (currentIcon.GetComponent<Icon>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j));
                                }

                                if (upIcon.GetComponent<Icon>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j + 1));
                                }

                                if (downIcon.GetComponent<Icon>().isRowBomb)
                                {
                                    currentMatches.Union(GetRowPieces(j - 1));
                                }
                                if (!currentMatches.Contains(upIcon))
                                {
                                    currentMatches.Add(upIcon);
                                }
                                upIcon.GetComponent<Icon>().isMatched = true;
                                if (!currentMatches.Contains(downIcon))
                                {
                                    currentMatches.Add(downIcon);
                                }
                                downIcon.GetComponent<Icon>().isMatched = true;
                                if (!currentMatches.Contains(currentIcon))
                                {
                                    currentMatches.Add(currentIcon);
                                }
                                currentIcon.GetComponent<Icon>().isMatched = true;
                            }
                        }
                    }
                }
            }
        }
    }

    List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> icons = new List<GameObject>();
        for(int i = 0; i < board.height; i++)
        {
            if(board.allIcons[column, i] != null)
            {
                icons.Add(board.allIcons[column, i]);
                board.allIcons[column, i].GetComponent<Icon>().isMatched = true;
            }
        }
        
        return icons;
    }

    List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> icons = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
            if (board.allIcons[i, row] != null)
            {
                icons.Add(board.allIcons[i, row]);
                board.allIcons[i, row].GetComponent<Icon>().isMatched = true;
            }
        }

        return icons;
    }

    public void CheckBombs()
    {
        if(board.currentIcon != null)
        {
            if(board.currentIcon.isMatched)
            {
                board.currentIcon.isMatched = false;
                int typeOfBomb = Random.Range(0, 100);
                if(typeOfBomb < 50)
                {
                    board.currentIcon.MakeRowBomb();
                } else if(typeOfBomb >= 50)
                {
                    board.currentIcon.MakeColumnBomb();
                }
            } 
            else if (board.currentIcon.otherIcon != null) 
            {
                Icon otherIcon = board.currentIcon.otherIcon.GetComponent<Icon>();
                if (otherIcon.isMatched)
                {
                    otherIcon.isMatched = false;
                    int typeOfBomb = Random.Range(0, 100);
                    if (typeOfBomb < 50)
                    {
                        otherIcon.MakeRowBomb();                      
                    }
                    else if (typeOfBomb >= 50)
                    {
                        otherIcon.MakeColumnBomb();
                    }
                }
            }
        }
    }

}
