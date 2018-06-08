using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public  class GameController : MonoBehaviour {
    [SerializeField]
    private GameObject[] ArrCandy;
    [SerializeField]
    private Transform CandySpace;
    private Candy[,] grid;
    private GameState state = GameState.None;
    private GameObject selectedObj = null;
    public CandyArray candies;
    public int score;
    private AudioSource audio;
    private Vector2[] SpawnPositions; 
    [SerializeField]
    private Text ScoreText;
    [SerializeField]
    private Text TimeText;
    public float time;
    public readonly Vector2 CandySize = new Vector2(1f, 1f);
    public readonly Vector2 BottomRight = new Vector2(-1.37f, -3.27f);

    private IEnumerator CheckPotentialMatchesCoroutine;
    private IEnumerator AnimatePotentialMatchesCoroutine;

    IEnumerable<GameObject> potentialMatches;

    public static GameController current;

    void Start()
    {
        Init();
    }
    void Awake()
    {
        current = this;
    }
    public  void Init()
    {
        time = 20f;
        audio = GetComponent<AudioSource>();
        InitializeTypesOnPrefabCandy();
        
        FillGrid();
        StartCheckForPotentialMatches();

    }
  public void FillGrid()
    {
        InitializeVariables();
        if (candies != null)
            DestroyAllCandy();

        candies = new CandyArray();
        SpawnPositions = new Vector2[Constant.ySize];

        //   grid = new Candy[Constant.xSize, Constant.ySize] ;
        for (int i = 0; i < Constant.xSize; i++)
        {
            for (int j = 0; j < Constant.ySize; j++)
            {

                GameObject random = GetRandomCandy();
                //check if two previous horizontal are of the same type
                while (j >= 2 && candies[i, j - 1].GetComponent<Candy>().IsSameType(random.GetComponent<Candy>())
                    && candies[i, j - 2].GetComponent<Candy>().IsSameType(random.GetComponent<Candy>()))
                {
                    random = GetRandomCandy();
                }
                //check if two previous vertical are of the same type
                while (i >= 2 && candies[i - 1, j].GetComponent<Candy>()
                    .IsSameType(random.GetComponent<Candy>())
                    && candies[i - 2, j].GetComponent<Candy>().IsSameType(random.GetComponent<Candy>()))
                {
                    random = GetRandomCandy();
                }

                InstantiateAndPlaceNewCandy(i, j, random);

            }
        }
        SetupSpawnPositions();

    }

    void Update()
    {
      
        if (time > 0)
        {
            time -= Time.deltaTime;
            ShơwTime();
        }
      
        if (state == GameState.None)
        {
            //user has clicked or touched
            if (Input.GetMouseButtonDown(0))
            {
                //get the hit position
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null) //we have a hit!!!
                {
                    selectedObj = hit.collider.gameObject;
                    state = GameState.SelectionStarted;
                }

            }
        }
        else if (state == GameState.SelectionStarted)
        {
            //user dragged
            if (Input.GetMouseButton(0))
            {


                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                //we have a hit
                if (hit.collider != null && selectedObj != hit.collider.gameObject)
                {

                    //user did a hit, no need to show him hints 
                    StopCheckForPotentialMatches();

                    //if the two shapes are diagonally aligned (different row and column), just return
                    if (!Utilities.AreNeighBors(selectedObj.GetComponent<Candy>(), hit.collider.gameObject.GetComponent<Candy>()))
                    {
                        state = GameState.None;
                    }
                   else
                    {
                        state = GameState.Animating;
                        FixSortingLayer(selectedObj, hit.collider.gameObject);
                        StartCoroutine(FindMatchesAndCollapse(hit));
                    }
                }
            }
        }
        if (score >= 1000)
        {
            AchievementManager2.current.EarnAchievement("Press W");
        }

    }

    private void InitializeTypesOnPrefabCandy()
    {
        //just assign the name of the prefab
        foreach (var item in ArrCandy)
        {
            item.GetComponent<Candy>().Type = item.name;

        }
    }


    /// Modifies sorting layers for better appearance when dragging/animating
    private void FixSortingLayer(GameObject obj1, GameObject obj2)
    {
        SpriteRenderer sp1 = obj1.GetComponent<SpriteRenderer>();
        SpriteRenderer sp2 = obj2.GetComponent<SpriteRenderer>();
        if (sp1.sortingOrder <= sp2.sortingOrder)
        {
            sp1.sortingOrder = 1;
            sp2.sortingOrder = 0;
        }
    }
    private IEnumerator CheckPotentialMatches()
    {
        yield return new WaitForSeconds(Constant.WaitBeforePotentialMatchesCheck);
        potentialMatches = Utilities.GetPotentialMatches(candies);
        if (potentialMatches != null)
        {
            while (true)
            {

                AnimatePotentialMatchesCoroutine = Utilities.AnimatePotentialMatches(potentialMatches);
                StartCoroutine(AnimatePotentialMatchesCoroutine);
                yield return new WaitForSeconds(Constant.WaitBeforePotentialMatchesCheck);
            }
        }

    }
   
    private GameObject GetRandomCandy()
    {
        return ArrCandy[Random.Range(0, ArrCandy.Length)];
    }
    private IEnumerator FindMatchesAndCollapse(RaycastHit2D hit2)
    {
        //set second item 
        var selectedObj2 = hit2.collider.gameObject;
        candies.Swap(selectedObj, selectedObj2);

        //move the swapped ones
        selectedObj.transform.positionTo(Constant.AnimationDuration, selectedObj2.transform.position);
        selectedObj2.transform.positionTo(Constant.AnimationDuration, selectedObj.transform.position);
        yield return new WaitForSeconds(Constant.AnimationDuration);

        //get the matches via the helper methods
        var hitGomatchesInfo = candies.GetMatches(selectedObj);
        var hitGo2matchesInfo = candies.GetMatches(selectedObj2);

        var totalMatches = hitGomatchesInfo.MatchedCandy.Union(hitGo2matchesInfo.MatchedCandy).Distinct();

        //if user's swap didn't create at least a 3-match, undo their swap
        if (totalMatches.Count() < Constant.MinimumMatches)
        {
            selectedObj.transform.positionTo(Constant.AnimationDuration, selectedObj2.transform.position);
            selectedObj2.transform.positionTo(Constant.AnimationDuration, selectedObj.transform.position);
            yield return new WaitForSeconds(Constant.AnimationDuration);

            candies.UndoSwap();
        }

        int timesRun = 1;

        while (totalMatches.Count() >= Constant.MinimumMatches)
        {
            audio.Play();
            //increase score
            IncreaseScore((totalMatches.Count() - 2) * Constant.Match3Score);

            if (timesRun >= 2)
                IncreaseScore(Constant.SubsequentMatchScore);



            foreach (var item in totalMatches)
            {
                candies.Remove(item);
                RemoveFromScene(item);
            }
            //get the columns that we had a collapse
            var columns = totalMatches.Select(go => go.GetComponent<Candy>().Column).Distinct();

            //the order the 2 methods below get called is important!!!
            //collapse the ones gone
            var collapsedCandyInfo = candies.Collapse(columns);
            //create new ones
            var newCandyInfo = CreateNewCandyInSpecificColumns(columns);

            int maxDistance = Mathf.Max(collapsedCandyInfo.MaxDistance, newCandyInfo.MaxDistance);

            MoveAndAnimate(newCandyInfo.AlteredCandy, maxDistance);
            MoveAndAnimate(collapsedCandyInfo.AlteredCandy, maxDistance);



            //will wait for both of the above animations
            yield return new WaitForSeconds(Constant.MoveAnimationMinDuration * maxDistance);

            //search if there are matches with the new/collapsed items
            totalMatches = candies.GetMatches(collapsedCandyInfo.AlteredCandy).
                Union(candies.GetMatches(newCandyInfo.AlteredCandy)).Distinct();



            timesRun++;
        }
        state = GameState.None;
        StartCheckForPotentialMatches();


    }

    /// Animates gameobjects to their new position
    private void MoveAndAnimate(IEnumerable<GameObject> movedGameObjects, int distance)
    {
        foreach (var item in movedGameObjects)
        {
            item.transform.positionTo(Constant.MoveAnimationMinDuration * distance, BottomRight +
                new Vector2(item.GetComponent<Candy>().Column * CandySize.x, item.GetComponent<Candy>().Row * CandySize.y));
        }
    }
    private AlteredCandyInfo CreateNewCandyInSpecificColumns(IEnumerable<int> columnsWithMissingCandy)
    {
        AlteredCandyInfo newCandyInfo = new AlteredCandyInfo();

        //find how many null values the column has
        foreach (int column in columnsWithMissingCandy)
        {
            var emptyItems = candies.GetEmptyItemsOnColumn(column);
            foreach (var item in emptyItems)
            {
                var go = GetRandomCandy();
                GameObject newCandy = Instantiate(go, SpawnPositions[column], Quaternion.identity)
                    as GameObject;

                newCandy.GetComponent<Candy>().Assign(item.Row, item.Column,go.GetComponent<Candy>().Type);

                if (Constant.xSize - item.Row > newCandyInfo.MaxDistance)
                    newCandyInfo.MaxDistance = Constant.xSize - item.Row;

                candies[item.Row, item.Column] = newCandy;
                newCandyInfo.AddCandy(newCandy);
            }
        }
        return newCandyInfo;
    }

    private void RemoveFromScene(GameObject item)
    {
        Destroy(item);
    }
    void StartCheckForPotentialMatches()
    {
        StopCheckForPotentialMatches();
        //get a reference to stop it later
        CheckPotentialMatchesCoroutine = CheckPotentialMatches();
        StartCoroutine(CheckPotentialMatchesCoroutine);
    }
   public void StopCheckForPotentialMatches()
    {
        if (AnimatePotentialMatchesCoroutine != null)
            StopCoroutine(AnimatePotentialMatchesCoroutine);
        if (CheckPotentialMatchesCoroutine != null)
            StopCoroutine(CheckPotentialMatchesCoroutine);
        ResetOpacityOnPotentialMatches();
    }

    private void ResetOpacityOnPotentialMatches()
    {
        if (potentialMatches != null)
            foreach (var item in potentialMatches)
            {
                if (item == null) break;

                Color c = item.GetComponent<SpriteRenderer>().color;
                c.a = 1.0f;
                item.GetComponent<SpriteRenderer>().color = c;
            }
    }
    public void DestroyAllCandy()
    {
        for (int row = 0; row < Constant.xSize; row++)
        {
            for (int column = 0; column < Constant.ySize; column++)
            {
                Destroy(candies[row, column]);
            }
        }
    }
  
    private void InstantiateAndPlaceNewCandy(int row, int column, GameObject newCandy)
    {
        GameObject go = Instantiate(newCandy,
            BottomRight + new Vector2(column * CandySize.x, row * CandySize.y), Quaternion.identity)
            as GameObject;

        //assign the specific properties
        go.GetComponent<Candy>().Assign(row, column, newCandy.GetComponent<Candy>().Type);
        candies[row, column] = go;
    }

    private void SetupSpawnPositions()
    {
        //create the spawn positions for the new shapes (will pop from the 'ceiling')
        for (int column = 0; column < Constant.ySize; column++)
        {
            SpawnPositions[column] = BottomRight
                + new Vector2(column * CandySize.x, Constant.xSize * CandySize.y);
        }
    }
    private void InitializeVariables()
    {
        score = 0;
        ShowScore();
    }

    private void IncreaseScore(int amount)
    {
        score += amount;
        ShowScore();
    }

    private void ShowScore()
    {
        ScoreText.text = "Score: " + score.ToString();
    }
    private void ShơwTime()
    {
        TimeText.text = "Time: " + (int)time;
    }
}
