using System;
using System.Collections;
using System.Collections.Generic;
using Modules.ThirdParties.EditorButton;
using PathCreation;
using UnityEngine;

public class TrackPathComponent : MonoBehaviour
{
    
    //Racetrack -> trackpath component -> Path
    public PathCreator PathCreator;

    public List<Transform> OverridePathsByTransform;

    public List<Vector3> Paths;

    private void Awake()
    {
        Paths = GetPaths();
    }

    public List<Vector3> GetPaths()
    {
        List<Vector3> returnVector3 = new List<Vector3>();
        if (OverridePathsByTransform.Count > 0)
        {
            foreach (var pathTrans in OverridePathsByTransform)
            {
                Vector3 tmpV3 = new Vector3();
                tmpV3 = pathTrans.position;
                returnVector3.Add(tmpV3);
            }
            return returnVector3;
        }

        for (int i = 0; i < PathCreator.path.localPoints.Length; i++)
        {
            Vector3 tmpV3 = new Vector3();
            tmpV3 = PathCreator.path.localPoints[i];
            returnVector3.Add(tmpV3);
        }
        return returnVector3;
    }

    [EditorButton]
    public void EditorGetPath()
    {
        Paths = GetPaths();
    }
}
