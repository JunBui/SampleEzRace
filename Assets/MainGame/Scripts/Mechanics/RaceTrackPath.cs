using System.Collections;
using System.Collections.Generic;
using Modules.GameplayHelpers.Commons;
using Modules.ThirdParties.EditorButton;
using UnityEngine;

public class RaceTrackPath : MonoBehaviour
{
    public Transform PathCreate;
    public List<Transform> Paths;

    [EditorButton]
    public void GetPathAgain()
    {
        Paths = new List<Transform>();
        foreach (Transform child in transform)
        {
            Paths.Add(child.transform);
        }
    }
    [EditorButton]
    public void CreatePahtPoint()
    {
        transform.RemoveAllChildsEditor();
        Paths = new List<Transform>();
        foreach (Transform child in PathCreate)
        {
            GameObject tmpGo = Instantiate(child.gameObject, this.transform);
            tmpGo.transform.position = child.position;
            Paths.Add(tmpGo.transform);
        }
    }
}
