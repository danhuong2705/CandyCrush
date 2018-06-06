using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CandyArray {
    private GameObject[,] candies = new GameObject[Constant.xSize,Constant.ySize];
    private GameObject backUpObj1;
    private GameObject backUpObj2;
    public GameObject this[int row,int column]
    {
        get
        {
            try
            {
                return candies[row, column];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        set
        {
            candies[row, column] = value;
        }
    }
    public void  Swap(GameObject obj1, GameObject obj2)
    {
        //backup in case no match
        backUpObj1 = obj1;
        backUpObj2 = obj2;

        var Candy1 = obj1.GetComponent<Candy>();
        var Candy2 = obj2.GetComponent<Candy>();

        //get array index
        int obj1Row = Candy1.Row;
        int obj2Row = Candy2.Row;
        int obj1Col = Candy1.Column;
        int obj2Col = Candy2.Column;

        //swap in array
        var temp = candies[obj1Row, obj1Col];
        candies[obj1Row, obj1Col] = candies[obj2Row, obj2Col];
        candies[obj2Row, obj2Col] = temp;

        //swap their respective properties
        Candy.SwapCandy(Candy1, Candy2);

    }

    public void UndoSwap()
    {
        if(backUpObj1 == null || backUpObj2 == null)
        {
            throw new Exception("Backup is null");
           
        }
        Swap(backUpObj1, backUpObj2);
    }
    public MatchesInfo GetMatches(GameObject obj)
    {
        MatchesInfo matchesInfo = new MatchesInfo();

        var horizontalMatches = GetMatchesHorizontally(obj);
        
        matchesInfo.AddObjectRange(horizontalMatches);

        var verticalMatches = GetMatchesVertically(obj);
       
        matchesInfo.AddObjectRange(verticalMatches);

        return matchesInfo;
    }

    public IEnumerable<GameObject> GetMatches(IEnumerable<GameObject> objs)
    {
        List<GameObject> matches = new List<GameObject>();
        foreach (var obj in objs)
        {
            matches.AddRange(GetMatches(obj).MatchedCandy);
        }
        return matches.Distinct();
    }

    /// Searches horizontally for matches

    private IEnumerable<GameObject> GetMatchesHorizontally(GameObject obj)
    {
        List<GameObject> matches = new List<GameObject>();
        matches.Add(obj);
        var candy = obj.GetComponent<Candy>();
        //check left
        if (candy.Column != 0)
            for (int column = candy.Column - 1; column >= 0; column--)
            {
                if (candies[candy.Row, column].GetComponent<Candy>().IsSameType(candy))
                {
                    matches.Add(candies[candy.Row, column]);
                }
                else
                    break;
            }

        //check right
        if (candy.Column != Constant.ySize - 1)
            for (int column = candy.Column + 1; column < Constant.ySize; column++)
            {
                if (candies[candy.Row, column].GetComponent<Candy>().IsSameType(candy))
                {
                    matches.Add(candies[candy.Row, column]);
                }
                else
                    break;
            }

        //we want more than three matches
        if (matches.Count < Constant.MinimumMatches)
            matches.Clear();

        return matches.Distinct();
    }

   
    /// Searches vertically for matches
    private IEnumerable<GameObject> GetMatchesVertically(GameObject obj)
    {
        List<GameObject> matches = new List<GameObject>();
        matches.Add(obj);
        var candy = obj.GetComponent<Candy>();
        //check bottom
        if (candy.Row != 0)
            for (int row = candy.Row - 1; row >= 0; row--)
            {
                if (candies[row, candy.Column] != null &&
                    candies[row, candy.Column].GetComponent<Candy>().IsSameType(candy))
                {
                    matches.Add(candies[row, candy.Column]);
                }
                else
                    break;
            }

        //check top
        if (candy.Row != Constant.xSize - 1)
            for (int row = candy.Row + 1; row < Constant.xSize; row++)
            {
                if (candies[row, candy.Column] != null &&
                    candies[row, candy.Column].GetComponent<Candy>().IsSameType(candy))
                {
                    matches.Add(candies[row, candy.Column]);
                }
                else
                    break;
            }


        if (matches.Count < Constant.MinimumMatches)
            matches.Clear();

        return matches.Distinct();
    }

    /// Removes (sets as null) an item from the array

    public void Remove(GameObject item)
    {
        candies[item.GetComponent<Candy>().Row, item.GetComponent<Candy>().Column] = null;
    }
    public AlteredCandyInfo Collapse(IEnumerable<int> columns)
    {
        AlteredCandyInfo collapseInfo = new AlteredCandyInfo();


        ///search in every column
        foreach (var column in columns)
        {
            //begin from bottom row
            for (int row = 0; row < Constant.xSize - 1; row++)
            {
                //if you find a null item
                if (candies[row, column] == null)
                {
                    //start searching for the first non-null
                    for (int row2 = row + 1; row2 < Constant.xSize; row2++)
                    {
                        //if you find one, bring it down (i.e. replace it with the null you found)
                        if (candies[row2, column] != null)
                        {
                            candies[row, column] = candies[row2, column];
                            candies[row2, column] = null;

                            //calculate the biggest distance
                            if (row2 - row > collapseInfo.MaxDistance)
                                collapseInfo.MaxDistance = row2 - row;

                            //assign new row and column (name does not change)
                            candies[row, column].GetComponent<Candy>().Row = row;
                            candies[row, column].GetComponent<Candy>().Column = column;

                            collapseInfo.AddCandy(candies[row, column]);
                            break;
                        }
                    }
                }
            }
        }

        return collapseInfo;
    }
    /// Searches the specific column and returns info about null items
    public IEnumerable<CandyInfo> GetEmptyItemsOnColumn(int column)
    {
        List<CandyInfo> emptyItems = new List<CandyInfo>();
        for (int row = 0; row < Constant.xSize; row++)
        {
            if (candies[row, column] == null)
                emptyItems.Add(new CandyInfo() { Row = row, Column = column });
        }
        return emptyItems;
    }
}
