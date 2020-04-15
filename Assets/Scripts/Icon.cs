using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{

    [Header("Board Variables")]
    public int column;
    public int row;
    public int previousColumn;
    public int previousRow;
    public int targetX;
    public int targetY;
    public bool isMatched = false;


    private FindMatches findMatches;
    private Board board;
    public GameObject otherIcon;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;

    [Header("Swipe")]
    public float swipeAngle = 0;
    public float swipeResist = 1f;

    [Header("Power ups")]
    public bool isColumnBomb;
    public bool isRowBomb;
    public GameObject rowArrow;
    public GameObject columnArrow;

    // Start is called before the first frame update
    void Start()
    {
        isColumnBomb = false;
        isRowBomb = false;

        board = FindObjectOfType<Board>();
        findMatches = FindObjectOfType<FindMatches>();
        //targetX = (int)transform.position.x;
        //targetY = (int)transform.position.y;
        //row = targetY;
        //column = targetX;
        //previousRow = row;
        //previousColumn = column;
    }


    //Testing for Bombs
    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isColumnBomb = true;
            GameObject arrow = Instantiate(columnArrow, transform.position, Quaternion.identity);
            arrow.transform.parent = this.transform;
        }
    }

    // Update is called once per frame


    void Update()
    {


        //if (isMatched)
        //{
        //    SpriteRenderer mySprite = GetComponent<SpriteRenderer>();
        //    mySprite.color = new Color(1f, 1f, 1f, .2f);
        //}
        targetX = column;
        targetY = row;
        if(Mathf.Abs(targetX - transform.position.x) > .1)
        {
            //move towards target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if(board.allIcons[column, row] != this.gameObject)
            {
                board.allIcons[column, row] = this.gameObject;
            }

            findMatches.FindallMatches();
        } else
        {
            //set position absolutely
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
        }


        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //move towards target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .6f);
            if (board.allIcons[column, row] != this.gameObject)
            {
                board.allIcons[column, row] = this.gameObject;
            }

            findMatches.FindallMatches();

        }
        else
        {
            //set position absolutely
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
        }
    }

    public IEnumerator CheckMoveCo()
    {
        yield return new WaitForSeconds(.5f);
        if(otherIcon != null)
        {
            if (!isMatched && !otherIcon.GetComponent<Icon>().isMatched)
            {
                otherIcon.GetComponent<Icon>().row = row;
                otherIcon.GetComponent<Icon>().column = column;
                row = previousRow;
                column = previousColumn;
                yield return new WaitForSeconds(.5f);
                board.currentIcon = null;
                board.currentState = GameState.move;

            }
            else
            {
                board.DestroyMatches();
               
            }
            //otherIcon = null;
        }
    }

    private void OnMouseDown()
    {
        if(board.currentState == GameState.move)
        {
            firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void OnMouseUp()
    {
        if(board.currentState == GameState.move)
        {
            finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CalculateAngle();
        }
    }

    void CalculateAngle()
    {
        if (Mathf.Abs(finalTouchPosition.y - firstTouchPosition.y) > swipeResist || Mathf.Abs(finalTouchPosition.x - firstTouchPosition.x) > swipeResist)
        {
            swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
            MoveIcons();
            board.currentState = GameState.wait;
            board.currentIcon = this;
        }
        else
        {
            board.currentState = GameState.move;
        }

    }

    void MoveIcons()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width - 1)
        {
            //Right
            otherIcon = board.allIcons[column + 1, row];
            previousRow = row;
            previousColumn = column;
            otherIcon.GetComponent<Icon>().column -= 1;
            column += 1;
        } else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height - 1)
        {
            //Up
            otherIcon = board.allIcons[column, row + 1];
            previousRow = row;
            previousColumn = column;
            otherIcon.GetComponent<Icon>().row -= 1;
            row += 1;
        } else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0 )
        {
            //Left
            otherIcon = board.allIcons[column - 1, row];
            previousRow = row;
            previousColumn = column;
            otherIcon.GetComponent<Icon>().column += 1;
            column -= 1;
        } else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            //Down
            otherIcon = board.allIcons[column, row - 1];
            previousRow = row;
            previousColumn = column;
            otherIcon.GetComponent<Icon>().row += 1;
            row -= 1;
        }
        StartCoroutine(CheckMoveCo());
    }

    //void FindMatches()
    //{
    //    if(column > 0 && column < board.width - 1)
    //    {
    //        GameObject leftIcon1 = board.allIcons[column - 1, row];
    //        GameObject rightIcon1 = board.allIcons[column + 1, row];
    //        if(leftIcon1 != null && rightIcon1 != null)
    //        {
    //            if (leftIcon1.tag == this.gameObject.tag && rightIcon1.tag == this.gameObject.tag)
    //            {
    //                leftIcon1.GetComponent<Icon>().isMatched = true;
    //                rightIcon1.GetComponent<Icon>().isMatched = true;
    //                isMatched = true;
    //            }
    //        }
           
    //    }
    //    if (row > 0 && row < board.height - 1)
    //    {
    //        GameObject downIcon1 = board.allIcons[column, row - 1];
    //        GameObject upIcon1 = board.allIcons[column, row + 1];
    //        if (downIcon1 != null && upIcon1 != null)
    //        {
    //            if (downIcon1.tag == this.gameObject.tag && upIcon1.tag == this.gameObject.tag)
    //            {
    //                downIcon1.GetComponent<Icon>().isMatched = true;
    //                upIcon1.GetComponent<Icon>().isMatched = true;
    //                isMatched = true;
    //            }
    //        }
            
    //    }
    //}

    public void MakeRowBomb()
    {
        isRowBomb = true;
        GameObject arrow = Instantiate(rowArrow, transform.position, Quaternion.identity);
        arrow.transform.parent = this.transform;
    }

    public void MakeColumnBomb()
    {
        isColumnBomb = true;
        GameObject arrow = Instantiate(columnArrow, transform.position, Quaternion.identity);
        arrow.transform.parent = this.transform;
    }
}
