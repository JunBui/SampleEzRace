using System.Collections;
using System.Collections.Generic;
using Modules.ThirdParties.EditorButton;
using UnityEngine;

public class RoadFinish : MonoBehaviour
{
    public TrackPathComponent PreviousTrack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [EditorButton]
    public void ToFinishPoint()
    {
        this.transform.position = PreviousTrack.Paths[PreviousTrack.Paths.Count-1];
    }
}
