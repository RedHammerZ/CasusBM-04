using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    public int width;
    public int height;
    public GameObject tilePrefab;
    public GameObject[] icons;
    private BackgroundTile[,] allTiles;
    public GameObject[,] allIcons;


    // Start is called before the first frame update
    void Start()
    {
        allTiles = new BackgroundTile[width, height];
        allIcons = new GameObject[width, height];
        SetUp();
    }

    private void SetUp()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Vector2 tempPosition = new Vector2(i, j);
                GameObject backgroundTile = Instantiate(tilePrefab, tempPosition, Quaternion.identity) as GameObject;
                backgroundTile.transform.parent = this.transform;
                backgroundTile.name = "( " + i + ", " + j + " )";

                int iconToUse = Random.Range(0, icons.Length);

                int maxIterations = 0;
                while(MatchesAt(i, j, icons[iconToUse]) && maxIterations < 100)
                {
                    iconToUse = Random.Range(0, icons.Length);
                    maxIterations++;
                }
                maxIterations = 0;

                GameObject icon = Instantiate(icons[iconToUse], tempPosition, Quaternion.identity);
                icon.transform.parent = this.transform;
                icon.name = "( " + i + ", " + j + " )";
                allIcons[i, j] = icon;
            }
        }
    }

    private bool MatchesAt(int column, int row, GameObject icon)
    {
        if(column > 1 && row > 1)
        {
            if(allIcons[column -1, row].tag == icon.tag && allIcons[column -2, row].tag == icon.tag)
            {
                return true;
            }
            if (allIcons[column, row - 1].tag == icon.tag && allIcons[column, row -2].tag == icon.tag)
            {
                return true;
            }
        } else if(column <= 1 || row <= 1)
        {
            if(row > 1)
            {
                if(allIcons[column, row -1].tag == icon.tag && allIcons[column, row - 2].tag == icon.tag)
                {
                    return true;
                }
            }
            if (column > 1)
            {
                if (allIcons[column -1, row].tag == icon.tag && allIcons[column -2, row].tag == icon.tag)
                {
                    return true;
                }
            }
        }
        

        return false;
    }

    private void DestroyMatchesAt(int column, int row)
    {
        if(allIcons[column, row].GetComponent<Icon>().isMatched)
        {
            Destroy(allIcons[column, row]);
            allIcons[column, row] = null;
        }
    }

    public void DestroyMatches()
    {
        for(int i = 0; i< width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                if(allIcons[i, j] != null)
                {
                    DestroyMatchesAt(i, j);
                }

            }
        }
        StartCoroutine(DecreaseRowCo());
    }

    private IEnumerator DecreaseRowCo()
    {
        int nullcount = 0;
        for(int i = 0; i<width; i++)
        {
            for(int j = 0; j<height; j++)
            {
                if(allIcons[i, j] == null)
                {
                    nullcount++;
                }
                else if(nullcount>0)
                {
                    allIcons[i, j].GetComponent<Icon>().row -= nullcount;
                    allIcons[i, j] = null;
                }
            }
            nullcount = 0;
        }
        yield return new WaitForSeconds(.4f);

    }

}
