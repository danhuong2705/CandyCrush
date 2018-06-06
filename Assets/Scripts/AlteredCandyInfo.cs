using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlteredCandyInfo : MonoBehaviour {

    private List<GameObject> newCandy { get; set; }
    public int MaxDistance { get; set; }

    /// Returns distinct list of altered candy

    public IEnumerable<GameObject> AlteredCandy
    {
        get
        {
            return newCandy.Distinct();
        }
    }

    public void AddCandy(GameObject go)
    {
        if (!newCandy.Contains(go))
            newCandy.Add(go);
    }

    public AlteredCandyInfo()
    {
        newCandy = new List<GameObject>();
    }
}
