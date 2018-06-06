using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchesInfo : MonoBehaviour {

    private List<GameObject> matchedCandies;

    /// Returns distinct list of matched candy
    public IEnumerable<GameObject> MatchedCandy
    {
        get
        {
            return matchedCandies.Distinct();
        }
    }

    public void AddObject(GameObject obj)
    {
        if (!matchedCandies.Contains(obj))
            matchedCandies.Add(obj);
    }

    public void AddObjectRange(IEnumerable<GameObject> objs)
    {
        foreach (var item in objs)
        {
            AddObject(item);
        }
    }

    public MatchesInfo()
    {
        matchedCandies = new List<GameObject>();
     
    }

}
