using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icon : MonoBehaviour
{

    public int column;
    public int row;
    public int targetX;
    public int targetY;
    private Board board;
    private GameObject otherIcon;
    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;
    private Vector2 tempPosition;
    public float swipeAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        board = FindObjectOfType<Board>();
        targetX = (int)transform.position.x;
        targetY = (int)transform.position.y;
        row = targetY;
        column = targetX;
    }

    // Update is called once per frame
    void Update()
    {
        targetX = column;
        targetY = row;
        if(Mathf.Abs(targetX - transform.position.x) > .1)
        {
            //move towards target
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);

        } else
        {
            //set position absolutely
            tempPosition = new Vector2(targetX, transform.position.y);
            transform.position = tempPosition;
            board.allIcons[column, row] = this.gameObject;
        }


        if (Mathf.Abs(targetY - transform.position.y) > .1)
        {
            //move towards target
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.Lerp(transform.position, tempPosition, .4f);

        }
        else
        {
            //set position absolutely
            tempPosition = new Vector2(transform.position.x, targetY);
            transform.position = tempPosition;
            board.allIcons[column, row] = this.gameObject;
        }
    }

    void OnMouseDown()
    {
        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }

    private void OnMouseUp()
    {
        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CalculateAngle();
    }

    void CalculateAngle()
    {
        swipeAngle = Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
        //Debug.Log(swipeAngle);
        MoveIcons();
    }

    void MoveIcons()
    {
        if (swipeAngle > -45 && swipeAngle <= 45 && column < board.width)
        {
            //Right
            otherIcon = board.allIcons[column + 1, row];
            otherIcon.GetComponent<Icon>().column -= 1;
            column += 1;
        } else if (swipeAngle > 45 && swipeAngle <= 135 && row < board.height)
        {
            //Up
            otherIcon = board.allIcons[column, row + 1];
            otherIcon.GetComponent<Icon>().row -= 1;
            row += 1;
        } else if ((swipeAngle > 135 || swipeAngle <= -135) && column > 0 )
        {
            //Left
            otherIcon = board.allIcons[column - 1, row];
            otherIcon.GetComponent<Icon>().column += 1;
            column -= 1;
        } else if (swipeAngle < -45 && swipeAngle >= -135 && row > 0)
        {
            //Down
            otherIcon = board.allIcons[column, row - 1];
            otherIcon.GetComponent<Icon>().row += 1;
            row -= 1;
        }
    }
}
