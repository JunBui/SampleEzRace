using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathCreation;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    public PlayerGroundState PlayerGroundState;
    public PlayerMovementState PlayerMovementState;
    public RaceTrack RaceTrack;
    public EndOfPathInstruction endOfPathInstruction;
    public float LeftRightSpeed = 2f;    
    public float LeftRightAccel = 2f;    
    public float MaxSpeed = 5f;    
    public float currentMoveSpeed = 5f; 
    public float Accel = 5f;   

    public Transform PlayerMoveLeftRight;

    public Transform MaxLeft;
    public Transform MaxRight;
    [HideInInspector]
    public bool reachFinalPos;
    [HideInInspector]
    public float currrentLeftRightValue;
    private float startLeftRightValue;
    [HideInInspector]
    public float targetLeftRightValue;
    private float lastInputTouchX;
    private float currentInputTouchX;
    private bool isHoldingInput;
    private int distanceTravelled;
    private int pathIndex;
    [HideInInspector]
    public bool IsMoving;
    [HideInInspector]
    public bool StartGainSpeed;
    [HideInInspector]
    public float XoffSet;
    [HideInInspector]
    public float XDriftPath;

    private float startGainSpeedValue;
    // Start is called before the first frame update
    void Start()
    {
        PlayerGroundState = PlayerGroundState.OnGround;
        IsMoving = false;
        Application.targetFrameRate = 60;
        pathIndex = 0;
        distanceTravelled = 0;
        reachFinalPos = false;
        isHoldingInput = false;
        currrentLeftRightValue = 0;
        startLeftRightValue = currrentLeftRightValue;
        currentMoveSpeed = 0;
        RaceTrack raceTrack = FindObjectOfType<RaceTrack>();
    }

    // Update is called once per frame
    void Update()
    {
        if(reachFinalPos)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            startGainSpeedValue = currentMoveSpeed;
            StartGainSpeed = true;
            isHoldingInput = true;
            lastInputTouchX = Input.mousePosition.x;
            startLeftRightValue = currrentLeftRightValue;
        }
        if (Input.GetMouseButtonUp(0))
        {
            StartGainSpeed = false;
            isHoldingInput = false;
        }
        if (isHoldingInput)
        {
            currentMoveSpeed += Accel * Time.deltaTime;
        }
        else
        {
            currentMoveSpeed -= Accel/2 * Time.deltaTime;
        }

        currentMoveSpeed = Mathf.Clamp(currentMoveSpeed, 0, MaxSpeed);

        if (currentMoveSpeed > 0)
        {
            if (startGainSpeedValue+MaxSpeed/3<= currentMoveSpeed || currentMoveSpeed>= MaxSpeed*3/4)
                StartGainSpeed = false;
            IsMoving = true;
        }
        else
        {
            IsMoving = false;
        }
        MoveAlongPath();
        MoveLeftRight();
    }

    void MoveLeftRight()
    {
        currentInputTouchX = Input.mousePosition.x;
        XoffSet = currentInputTouchX - lastInputTouchX;
        XoffSet *= LeftRightSpeed;
        targetLeftRightValue = Mathf.Clamp(startLeftRightValue + XoffSet / Screen.width, -1, 1);
        currrentLeftRightValue = Mathf.MoveTowards(currrentLeftRightValue,targetLeftRightValue,LeftRightAccel*Time.deltaTime);
        PlayerMoveLeftRight.localPosition = new Vector3(Mathf.Lerp(MaxLeft.localPosition.x, MaxRight.localPosition.x,Remap(currrentLeftRightValue,-1,1,0,1))
            ,PlayerMoveLeftRight.localPosition.y,PlayerMoveLeftRight.localPosition.z);
    }
    void MoveAlongPath()
    {
        if (RaceTrack.Tracks.Count == 0 || PlayerGroundState == PlayerGroundState.Flying) return;

        Vector3 nextPoint = RaceTrack.Tracks[pathIndex].Paths[distanceTravelled];
        Vector3 direction = nextPoint - transform.position;
        direction.Normalize();
        transform.position = Vector3.MoveTowards(transform.position,nextPoint,currentMoveSpeed*Time.deltaTime);
        Vector3 firstCloseDis = transform.position;
        firstCloseDis.y = 0;
        Vector3 secondCloseDis = nextPoint;
        secondCloseDis.y = 0;
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion newRotation = Quaternion.Slerp(transform.rotation, targetRotation, 45 * Time.fixedDeltaTime);
            transform.rotation = newRotation;
        }
        if (Vector3.Distance(firstCloseDis, secondCloseDis) < 0.1f)
        {
            distanceTravelled += 1;
            if (distanceTravelled>= RaceTrack.Tracks[pathIndex].Paths.Count)
            {
                OnFinalPointOfCurrentPath();
            }
        }
        //move to point
    }

    private void OnFinalPointOfCurrentPath()
    {
        pathIndex++;
        distanceTravelled = 0;
        if(pathIndex>=RaceTrack.Tracks.Count)
            OnFinalPointOfFinalPath();
        else
        {
            if(RaceTrack.Tracks[pathIndex].FlyingPath)
                Fly();
        }
        
    }

    private void OnFinalPointOfFinalPath()
    {
        reachFinalPos = true;
    }

    public void Fly()
    {
        PlayerGroundState = PlayerGroundState.Flying;
        Vector3 beginOfNextPath = RaceTrack.Tracks[pathIndex].Paths[0];
        transform.DOMove(beginOfNextPath, .85f).OnComplete((() =>
        {
            PlayerGroundState = PlayerGroundState.OnGround;
        })).SetEase(Ease.Linear);
    }
    public float Remap (float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
    public enum PlayerGroundState
    {
        Flying,
        OnGround
    }

    public enum PlayerMovementState
    {
        DriftLeft,
        DriftRight,
    }