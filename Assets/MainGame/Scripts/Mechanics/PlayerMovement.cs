using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private List<Transform> Paths;
    public float moveSpeed = 5f;    // Speed in units per second
    private int currentWaypointIndex = 0;

    public Transform PlayerRotate;

    public Transform PlayerMoveLeftRight;

    public Transform MaxLeft;
    public Transform MaxRight;
    private float currrentLeftRightValue;
    private float lastInputTouchX;
    private float currentInputTouchX;
    
    // Start is called before the first frame update
    void Start()
    {
        currrentLeftRightValue = 0;
        RaceTrackPath raceTrackPath = FindObjectOfType<RaceTrackPath>();
        Paths = raceTrackPath.Paths;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastInputTouchX = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0))
        {
            MoveAlongPath();
            MoveLeftRight();
        }
    }

    void MoveLeftRight()
    {
        currentInputTouchX = Input.mousePosition.x;
        float offSet = currentInputTouchX - lastInputTouchX;
        currrentLeftRightValue = Mathf.Clamp(currrentLeftRightValue+offSet/Screen.width, -1, 1);
        PlayerMoveLeftRight.localPosition = Vector3.Lerp(MaxLeft.position, MaxRight.position,Remap(currrentLeftRightValue,-1,1,0,1));
    }
    void MoveAlongPath()
    {
        if (Paths.Count == 0) return;  // No waypoints set

        // Get the target waypoint
        Transform targetWaypoint = Paths[currentWaypointIndex];

        // Calculate the direction to the next waypoint
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;

        // Calculate the distance to move this frame
        float step = moveSpeed * Time.deltaTime;

        // Move the player towards the waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);
        PlayerRotate.LookAt(targetWaypoint.position);
        // Check if the player has reached the waypoint
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            // Move to the next waypoint
            currentWaypointIndex++;
            if (currentWaypointIndex >= Paths.Count)
            {
                OnFinalPoint();
            }
        }
    }

    private void OnFinalPoint()
    {
        
    }
    private float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
