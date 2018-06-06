using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : MonoBehaviour {
    /// Helper method to animate potential matches
    public static IEnumerator AnimatePotentialMatches(IEnumerable<GameObject> potentialMatches)
    {
        for (float i = 1f; i >= 0.3f; i -= 0.1f)
        {
            foreach (var item in potentialMatches)
            {
                Color c = item.GetComponent<SpriteRenderer>().color;
                c.a = i;
                item.GetComponent<SpriteRenderer>().color = c;
            }
            yield return new WaitForSeconds(Constant.OpacityAnimationFrameDelay);
        }
        for (float i = 0.3f; i <= 1f; i += 0.1f)
        {
            foreach (var item in potentialMatches)
            {
                Color c = item.GetComponent<SpriteRenderer>().color;
                c.a = i;
                item.GetComponent<SpriteRenderer>().color = c;
            }
            yield return new WaitForSeconds(Constant.OpacityAnimationFrameDelay);
        }
    }
    public static bool AreNeighBors(Candy candy1, Candy candy2)
    {
        return (candy1.Column == candy2.Column
            || candy1.Row == candy2.Row)
            && Mathf.Abs(candy1.Column - candy2.Column) <= 1
            && Mathf.Abs(candy1.Row - candy2.Row) <= 1;

    }

    /// Will check for potential matches vertically and horizontally
    public static IEnumerable<GameObject> GetPotentialMatches(CandyArray candies)
    {
        //list contain all matches found
        List<List<GameObject>> matches = new List<List<GameObject>>();
        for(int i = 0;i< Constant.xSize; i++)
        {
            for(int  j = 0; j < Constant.ySize; j++)
            {
                var matches1 = CheckHorizontal1(i, j, candies);
                var matches2 = CheckHorizontal2(i, j, candies);
                var matches3 = CheckHorizontal3(i, j, candies);
                var matches4 = CheckVertical1(i, j, candies);
                var matches5 = CheckVertical2(i, j, candies);
                var matches6 = CheckVertical3(i, j, candies);

                if (matches1 != null) matches.Add(matches1);
                if (matches2 != null) matches.Add(matches2);
                if (matches3 != null) matches.Add(matches3);
                if (matches4 != null) matches.Add(matches4);
                if (matches5 != null) matches.Add(matches5);
                if (matches6 != null) matches.Add(matches6);

                if (matches.Count >= 3)
                    return matches[UnityEngine.Random.Range(0, matches.Count)];

                if( j >= Constant.xSize/2 && matches.Count > 0 && matches.Count >=2)
                {
                    return matches[UnityEngine.Random.Range(0, matches.Count - 1)];
                }


            }
        }
        return null;
    }
    public static List<GameObject> CheckHorizontal1(int row, int column, CandyArray Candys)
    {
        if (column <= Constant.ySize - 2)
        {
            if (Candys[row, column].GetComponent<Candy>().IsSameType(Candys[row, column + 1].GetComponent<Candy>())) 
            {
                if (row >= 1 && column >= 1)
                    if (Candys[row, column].GetComponent<Candy>().
                    IsSameType(Candys[row - 1, column - 1].GetComponent<Candy>()))
                        return new List<GameObject>()
                                {
                                    Candys[row, column],
                                    Candys[row, column + 1],
                                    Candys[row - 1, column - 1]
                                };

                /* example *\
                 * * * * *
                 * * * * *
                 * * * * *
                 * & & * *
                 & * * * *
                \* example  */

                if (row <= Constant.xSize - 2 && column >= 1)
                    if (Candys[row, column].GetComponent<Candy>().
                    IsSameType(Candys[row + 1, column - 1].GetComponent<Candy>()))
                        return new List<GameObject>()
                                {
                                    Candys[row, column],
                                    Candys[row, column + 1],
                                    Candys[row + 1, column - 1]
                                };

                /* example *\
                 * * * * *
                 * * * * *
                 & * * * *
                 * & & * *
                 * * * * *
                \* example  */
            }
        }
        return null;
    }


    public static List<GameObject> CheckHorizontal2(int row, int column, CandyArray Candys)
    {
        if (column <= Constant.ySize - 3)
        {
            if (Candys[row, column].GetComponent<Candy>().
                IsSameType(Candys[row, column + 1].GetComponent<Candy>()))
            {

                if (row >= 1 && column <= Constant.ySize - 3)
                    if (Candys[row, column].GetComponent<Candy>().
                    IsSameType(Candys[row - 1, column + 2].GetComponent<Candy>()))
                        return new List<GameObject>()
                                {
                                    Candys[row, column],
                                    Candys[row, column + 1],
                                    Candys[row - 1, column + 2]
                                };

                /* example *\
                 * * * * *
                 * * * * *
                 * * * * *
                 * & & * *
                 * * * & *
                \* example  */

                if (row <= Constant.xSize - 2 && column <= Constant.ySize - 3)
                    if (Candys[row, column].GetComponent<Candy>().
                    IsSameType(Candys[row + 1, column + 2].GetComponent<Candy>()))
                        return new List<GameObject>()
                                {
                                    Candys[row, column],
                                    Candys[row, column + 1],
                                    Candys[row + 1, column + 2]
                                };

                /* example *\
                 * * * * *
                 * * * * *
                 * * * & *
                 * & & * *
                 * * * * *
                \* example  */
            }
        }
        return null;
    }

    public static List<GameObject> CheckHorizontal3(int row, int column, CandyArray Candys)
    {
        if (column <= Constant.ySize - 4)
        {
            if (Candys[row, column].GetComponent<Candy>().
               IsSameType(Candys[row, column + 1].GetComponent<Candy>()) &&
               Candys[row, column].GetComponent<Candy>().
               IsSameType(Candys[row, column + 3].GetComponent<Candy>()))
            {
                return new List<GameObject>()
                                {
                                    Candys[row, column],
                                    Candys[row, column + 1],
                                    Candys[row, column + 3]
                                };
            }

            /* example *\
              * * * * *  
              * * * * *
              * * * * *
              * & & * &
              * * * * *
            \* example  */
        }
        if (column >= 2 && column <= Constant.ySize - 2)
        {
            if (Candys[row, column].GetComponent<Candy>().
               IsSameType(Candys[row, column + 1].GetComponent<Candy>()) &&
               Candys[row, column].GetComponent<Candy>().
               IsSameType(Candys[row, column - 2].GetComponent<Candy>()))
            {
                return new List<GameObject>()
                                {
                                    Candys[row, column],
                                    Candys[row, column + 1],
                                    Candys[row, column -2]
                                };
            }

            /* example *\
              * * * * * 
              * * * * *
              * * * * *
              * & * & &
              * * * * *
            \* example  */
        }
        return null;
    }

    public static List<GameObject> CheckVertical1(int row, int column, CandyArray Candys)
    {
        if (row <= Constant.xSize - 2)
        {
            if (Candys[row, column].GetComponent<Candy>().
                IsSameType(Candys[row + 1, column].GetComponent<Candy>()))
            {
                if (column >= 1 && row >= 1)
                    if (Candys[row, column].GetComponent<Candy>().
                    IsSameType(Candys[row - 1, column - 1].GetComponent<Candy>()))
                        return new List<GameObject>()
                                {
                                    Candys[row, column],
                                    Candys[row + 1, column],
                                    Candys[row - 1, column -1]
                                };

                /* example *\
                  * * * * *
                  * * * * *
                  * & * * *
                  * & * * *
                  & * * * *
                \* example  */

                if (column <= Constant.ySize - 2 && row >= 1)
                    if (Candys[row, column].GetComponent<Candy>().
                    IsSameType(Candys[row - 1, column + 1].GetComponent<Candy>()))
                        return new List<GameObject>()
                                {
                                    Candys[row, column],
                                    Candys[row + 1, column],
                                    Candys[row - 1, column + 1]
                                };

                /* example *\
                  * * * * *
                  * * * * *
                  * & * * *
                  * & * * *
                  * * & * *
                \* example  */
            }
        }
        return null;
    }

    public static List<GameObject> CheckVertical2(int row, int column, CandyArray Candys)
    {
        if (row <= Constant.xSize - 3)
        {
            if (Candys[row, column].GetComponent<Candy>().
                IsSameType(Candys[row + 1, column].GetComponent<Candy>()))
            {
                if (column >= 1)
                    if (Candys[row, column].GetComponent<Candy>().
                    IsSameType(Candys[row + 2, column - 1].GetComponent<Candy>()))
                        return new List<GameObject>()
                                {
                                    Candys[row, column],
                                    Candys[row + 1, column],
                                    Candys[row + 2, column -1]
                                };

                /* example *\
                  * * * * *
                  & * * * *
                  * & * * *
                  * & * * *
                  * * * * *
                \* example  */

                if (column <= Constant.ySize - 2)
                    if (Candys[row, column].GetComponent<Candy>().
                    IsSameType(Candys[row + 2, column + 1].GetComponent<Candy>()))
                        return new List<GameObject>()
                                {
                                    Candys[row, column],
                                    Candys[row+1, column],
                                    Candys[row + 2, column + 1]
                                };

                /* example *\
                  * * * * *
                  * * & * *
                  * & * * *
                  * & * * *
                  * * * * *
                \* example  */

            }
        }
        return null;
    }

    public static List<GameObject> CheckVertical3(int row, int column, CandyArray Candys)
    {
        if (row <= Constant.xSize - 4)
        {
            if (Candys[row, column].GetComponent<Candy>().
               IsSameType(Candys[row + 1, column].GetComponent<Candy>()) &&
               Candys[row, column].GetComponent<Candy>().
               IsSameType(Candys[row + 3, column].GetComponent<Candy>()))
            {
                return new List<GameObject>()
                                {
                                    Candys[row, column],
                                    Candys[row + 1, column],
                                    Candys[row + 3, column]
                                };
            }
        }

        /* example *\
          * & * * *
          * * * * *
          * & * * *
          * & * * *
          * * * * *
        \* example  */

        if (row >= 2 && row <= Constant.xSize - 2)
        {
            if (Candys[row, column].GetComponent<Candy>().
               IsSameType(Candys[row + 1, column].GetComponent<Candy>()) &&
               Candys[row, column].GetComponent<Candy>().
               IsSameType(Candys[row - 2, column].GetComponent<Candy>()))
            {
                return new List<GameObject>()
                                {
                                    Candys[row, column],
                                    Candys[row + 1, column],
                                    Candys[row - 2, column]
                                };
            }
        }

        /* example *\
          * * * * *
          * & * * *
          * & * * *
          * * * * *
          * & * * *
        \* example  */
        return null;
    }
  
}
