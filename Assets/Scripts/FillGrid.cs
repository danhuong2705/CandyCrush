using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillGrid : MonoBehaviour {
    public int xSize, ySize;
    private Item[,] items;
    private Item currentlySelectedItem;
    private float delayBetweenMatches = 0.1f;
    public static int minItemsForMatch = 3;
    [SerializeField]
    private Transform CandySpace;
    [SerializeField]
    private GameObject multiColor;
    [SerializeField]
    private float candyWidth=1f;
    [SerializeField]
    private GameObject[] candies;
    private bool canPlay;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text scoreGameOver;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private GameObject GameOverPanel;
    [SerializeField]
    private Canvas mainCanvas;
    private AudioSource audio;
    private float time;
    private bool isRunning;
    public int score;
    public static FillGrid instance;
    int count;

    // Use this for initialization
    void Start() {    
            audio = GetComponent<AudioSource>();
            
    }
     public void Init()
    {
        score = 0;
        UpdateScore();
        canPlay = true;
        GetCandies();
        FillGrids();
        ClearGrid();
        time = 20f;
        Item.OnMouseOverItemEventHandler += OnMouseOverItem;
    }
   
    void Awake()
    {
        instance = this;
    }
    void Update()
    {
      
        if (mainCanvas.gameObject.activeInHierarchy)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                timeText.text = (int)time + "";
            }
            if (time <= 0)
            {
                if (isRunning == false)
                {
                    SetActive(false);
                    GameOverPanel.SetActive(true);
                    scoreGameOver.text = score + "";
                }
              
               
            }
        }
        
      
    }
    public void SetActive(bool st)
    {
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
                items[i, j].gameObject.SetActive(st);

            }
        }
    }
    void OnDisable()
    {
        Item.OnMouseOverItemEventHandler -= OnMouseOverItem;
    }
    void UpdateScore()
    {     
        scoreText.text =  score+"";
    }

    void FillGrids()
    {
        items = new Item[xSize, ySize];
        for(int i = 0; i < xSize; i++)
        {
            for(int j = 0; j < ySize; j++)
            {
              items[i,j] =  InstantiateCandy(i, j);
            }
        }
    }
    void CountMatch()
    {
        count = 0;
        for(int i = 0; i < xSize; i++)
        {
            for(int j = 0; j < ySize; j++)
            {
                MatchInfo matchInfo = GetMatchInformation(items[i,j]);
                if (matchInfo.validMatch)
                {
                    count++;
                }
            }
        }
        Debug.Log(count);
        
    }
    void ReFill()
    {

        //bool[,] check = null;
        //Item[,] tmp = null;
        //int x = 0;
        //int y = 0;
        //for (int i = 0; i < xSize; i++)
        //{
        //    for (int j = 0; j < ySize; j++)
        //    {
        //        items[i, j].gameObject.SetActive(false);
        //    }
        //}
        //for (int i = 0; i < xSize; i++)
        //{
        //    for (int j = 0; j < ySize; j++)
        //    {
        //        check[i, j] = false;
        //        tmp[i, j] = items[i, j];
        //        items[i, j] = null;

        //    }
        //}
        //check[-1, -1] = true;
        int x = 0;
        int y = 0;
        for (int i = 0; i < xSize; i++)
        {
            for (int j = 0; j < ySize; j++)
            {
             
                while ((x==i && y==j))
                {
                    x = Random.Range(0, xSize);
                    y = Random.Range(0, ySize);
                    Swap(items[i, j], items[x, y]);
                 //   check[x, y] = true;
                }

            }
        }
       
    }
    public void DestroyAll()
    {
        for(int i = 0; i < xSize; i++)
        {
            for(int j = 0; j < ySize; j++)
            {
                Destroy(items[i, j].gameObject);
            }
        }
    }
    void ClearGrid()
    {
        for(int i = 0; i < xSize; i++)
        {
            for(int j = 0; j < ySize; j++)
            {
                MatchInfo matchInfo = GetMatchInformation(items[i,j]);
                if (matchInfo.validMatch)
                {               
                    Destroy(items[i, j].gameObject);
                    items[i, j] = InstantiateCandy(i, j);
                    j--;

                }
            }
        }
    }
    Item InstantiateCandy(int x,int y)
    {
        Vector3 Postion;
        GameObject randdomCandy = candies[Random.Range(0, candies.Length)];
        Item newCandy = ((GameObject)Instantiate(randdomCandy, new Vector3(x * candyWidth, y, 0), Quaternion.identity, CandySpace)).GetComponent<Item>();
        Postion.x = x * candyWidth;
        Postion.y = y;
        Postion.z = 0;
        newCandy.transform.position = Postion;
        newCandy.OnItemPostionChanged(x, y);
        return newCandy;
    }
    void OnMouseOverItem(Item item)
    {
        if(currentlySelectedItem == item || !canPlay)
        {
            return;
        }
        if(currentlySelectedItem == null)
        {
            currentlySelectedItem = item;
        }
        else
        {
            float xDiff = Mathf.Abs(currentlySelectedItem.X - item.X);
            float yDiff = Mathf.Abs(currentlySelectedItem.Y - item.Y);
            if (xDiff + yDiff == 1)
            {
                StartCoroutine(TryMatch(currentlySelectedItem, item));
               
            }
            else
            {
                Debug.Log("Cant swap!");
               
            }
            currentlySelectedItem = null;
        }
        
       
    }
    IEnumerator TryMatch(Item a,Item b)
    {
        isRunning = true;
        canPlay = false;

        yield return new WaitForSeconds(delayBetweenMatches);
        yield return StartCoroutine(Swap(a, b));
        MatchInfo matchA = GetMatchInformation(a);
        MatchInfo matchB = GetMatchInformation(b);


        if ((!matchA.validMatch) && (!matchB.validMatch))
        {

            yield return StartCoroutine(Swap(a, b));
            yield break;
        }
        if (matchA.validMatch)
        {
            matchType(matchA.match.Count);
            UpdateScore();
            yield return StartCoroutine(DestroyItems(matchA.match));
            yield return new WaitForSeconds(delayBetweenMatches);
            yield return StartCoroutine(UpdateGridAfterMatch(matchA));

        }
        else if (matchB.validMatch)
        {
            matchType(matchB.match.Count);
            UpdateScore();

            yield return StartCoroutine(DestroyItems(matchB.match));
            yield return new WaitForSeconds(delayBetweenMatches);
            yield return StartCoroutine(UpdateGridAfterMatch(matchB));

        }

        currentlySelectedItem = null;
        canPlay = true;
        isRunning = false;
    }

    IEnumerator UpdateGridAfterMatch(MatchInfo match)
    {
       
        if (match.matchStartingY == match.matchEndingY)
        {
            for (int i = match.matchStartingX; i <= match.matchEndingX; i++)
            {
                for (int j = match.matchStartingY; j < ySize - 1; j++)
                {
                    Item upperIdex = items[i, j + 1];
                    Item current = items[i, j];
                    items[i, j] = upperIdex;
                    items[i, j + 1] = current;
                    items[i, j].OnItemPostionChanged(items[i, j].X, items[i, j].Y - 1);
                }
                items[i, ySize - 1] = InstantiateCandy(i, ySize - 1);
            }
        }
        else if (match.matchEndingX == match.matchStartingX)
        {
            int matchHeight = 1 + (match.matchEndingY - match.matchStartingY);
            for (int j = match.matchStartingY + matchHeight; j <= ySize - 1; j++)
            {
                Item lowerIndex = items[match.matchStartingX, j - matchHeight];
                Item current = items[match.matchStartingX, j];
                items[match.matchStartingX, j - matchHeight] = current;
                items[match.matchStartingX, j] = lowerIndex;
            }
            for (int j = 0; j < ySize - matchHeight; j++)
            {
                items[match.matchStartingX, j].OnItemPostionChanged(match.matchStartingX, j);
            }
            for (int i = 0; i < match.match.Count; i++)
            {
                items[match.matchStartingX, (ySize - 1) - i] = InstantiateCandy(match.matchStartingX, (ySize - 1) - i);
            }
        }
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                MatchInfo matchInfo = GetMatchInformation(items[x, y]);
                if (matchInfo.validMatch)
                {
                    matchType(matchInfo.match.Count);
                    UpdateScore();
                    yield return new WaitForSeconds(delayBetweenMatches);
                    yield return StartCoroutine(DestroyItems(matchInfo.match));
                    yield return new WaitForSeconds(delayBetweenMatches);
                    yield return StartCoroutine(UpdateGridAfterMatch(matchInfo));

                }
            }
        }

    }
    IEnumerator DestroyItems(List<Item> listItem)
    {
        foreach(Item i in listItem)
        {
            yield return StartCoroutine(i.transform.Scale(Vector3.zero,0.05f));
            Destroy(i.gameObject);
        }
    }
    IEnumerator Swap(Item x,Item y)
    {
        ChangeRigidbodyStatus(false);
        float movDuration = 0.1f;
        Vector3 xPosition = x.transform.position;
        StartCoroutine(x.transform.Move(y.transform.position, movDuration));
        StartCoroutine(y.transform.Move(xPosition, movDuration));
        yield return new WaitForSeconds(movDuration);
        SwapIndices(x, y);
        ChangeRigidbodyStatus(true);
    }
    void SwapIndices(Item a,Item b)
    {
        Item itemX = items[a.X, a.Y];
        items[a.X, a.Y] = b;
        items[b.X, b.Y] = itemX;
        int bOldX = b.X;
        int bOldY = b.Y;
        b.OnItemPostionChanged(a.X, a.Y);
        a.OnItemPostionChanged(bOldX, bOldY);
    }
    List<Item> SearchHorizontally(Item item)
    {
        List<Item> hItems = new List<Item> { item };
        int left = item.X - 1;
        int right = item.X + 1;
        while(left >= 0 && items[left,item.Y]!=null && items[left,item.Y].id == item.id)
        {
            hItems.Add(items[left, item.Y]);
            left--;
        }
        while(right < xSize && items[right,item.Y]!=null && items[right,item.Y].id == item.id)
        {
            hItems.Add(items[right, item.Y]);
            right++;
        }
        return hItems;
    }
    List<Item> SearchVertically(Item item)
    {
        List<Item> vItems = new List<Item> { item };
        int lower = item.Y - 1;
        int upper = item.Y + 1;
        while(lower >=0 && items[item.X,lower] !=null && items[item.X,lower].id == item.id)
        {
            vItems.Add(items[item.X, lower]);
            lower--;
        }
        while(upper < ySize && items[item.X,upper] !=null &&items[item.X,upper].id == item.id)
        {
            vItems.Add(items[item.X, upper]);
            upper++;
        }
        return vItems;
            
    }
    MatchInfo GetMatchInformation(Item item)
    {
        MatchInfo m = new MatchInfo();
        m.match = null;
        List<Item> hMatch = SearchHorizontally(item);
        List<Item> vMatch = SearchVertically(item);
        if(hMatch.Count >= minItemsForMatch && hMatch.Count > vMatch.Count)
        {
            m.matchStartingX = GetMinimumX(hMatch);
            m.matchEndingX = GetMaximumX(hMatch);
            m.matchStartingY = m.matchEndingY = hMatch[0].Y;
            m.match = hMatch;
        }
        else if(vMatch.Count >= minItemsForMatch)
        {
            m.matchStartingY = GetMinimumY(vMatch);
            m.matchEndingY = GetMaximumY(vMatch);
            m.matchStartingX = m.matchEndingX = vMatch[0].X;
            m.match = vMatch;
        }
        return m;
    }

    int GetMinimumX(List<Item> listItem)
    {
        float[] indices = new float[listItem.Count];
        for(int i=0;i< indices.Length; i++)
        {
            indices[i] = listItem[i].X;
        }
        return (int)Mathf.Min(indices);
    }
    int GetMaximumX(List<Item> listItem)
    {
        float[] indices = new float[listItem.Count];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = listItem[i].X;
        }
        return (int)Mathf.Max(indices);
    }

    int GetMinimumY(List<Item> listItem)
    {
        float[] indices = new float[listItem.Count];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = listItem[i].Y;
        }
        return (int)Mathf.Min(indices);
    }

    int GetMaximumY(List<Item> listItem)
    {
        float[] indices = new float[listItem.Count];
        for (int i = 0; i < indices.Length; i++)
        {
            indices[i] = listItem[i].Y;
        }
        return (int)Mathf.Max(indices);
    }
    void GetCandies()
    {

        for (int i = 0; i < candies.Length; i++)
        {
            candies[i].GetComponent<Item>().id = i;
        }
    }
    void ChangeRigidbodyStatus(bool status)
    {
        foreach(Item  item in items)
        {
            item.GetComponent<Rigidbody2D>().isKinematic = !status;
        }
    }
    void matchType(int n)
    {

        switch (n)
        {
            case 3:
                score += 10;
                break;
            case 4:
                score += 15;
                break;
            case 5:
                score += 25;
                break;
                 }

        audio.Play();
    }
}
