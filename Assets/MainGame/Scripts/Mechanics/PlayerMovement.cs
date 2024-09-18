using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using PathCreation;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerGroundState PlayerGroundState;
    public PlayerMovementState PlayerMovementState;
    public List<PathCreator> Paths;
    public EndOfPathInstruction endOfPathInstruction;
    public float LeftRightSpeed = 2f;    
    public float MaxSpeed = 5f;    
    private float moveSpeed = 5f; 
    public float Accel = 5f;   

    public Transform PlayerRotate;

    public Transform PlayerMoveLeftRight;

    public Transform MaxLeft;
    public Transform MaxRight;
    public bool reachFinalPos;
    private float currrentLeftRightValue;
    private float lastLeftRightValue;
    private float lastInputTouchX;
    private float currentInputTouchX;
    private bool isHoldingInput;
    float distanceTravelled;
    private int pathIndex;
    private bool IsMoving;
    
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
        lastLeftRightValue = currrentLeftRightValue;
        moveSpeed = 0;
        RaceTrackPath raceTrackPath = FindObjectOfType<RaceTrackPath>();
        if (raceTrackPath != null)
            Paths = raceTrackPath.Paths;
    }

    // Update is called once per frame
    void Update()
    {
        if(reachFinalPos)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            isHoldingInput = true;
            lastInputTouchX = Input.mousePosition.x;
            lastLeftRightValue = currrentLeftRightValue;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isHoldingInput = false;
        }
        if (isHoldingInput)
        {
            moveSpeed += Accel * Time.deltaTime;
        }
        else
        {
            moveSpeed -= Accel/2 * Time.deltaTime;
        }

        moveSpeed = Mathf.Clamp(moveSpeed, 0, MaxSpeed);

        if (moveSpeed > 0)
        {
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
        float offSet = currentInputTouchX - lastInputTouchX;
        offSet *= LeftRightSpeed;
        currrentLeftRightValue = Mathf.Clamp(lastLeftRightValue+offSet/Screen.width, -1, 1);
        PlayerMoveLeftRight.localPosition = new Vector3(Mathf.Lerp(MaxLeft.localPosition.x, MaxRight.localPosition.x,Remap(currrentLeftRightValue,-1,1,0,1))
            ,PlayerMoveLeftRight.localPosition.y,PlayerMoveLeftRight.localPosition.z);
        
        // lastInputTouchX = Input.mousePosition.x;
    }
    void MoveAlongPath()
    {
        if (Paths.Count == 0 || PlayerGroundState == PlayerGroundState.Flying) return; 
        
        distanceTravelled += moveSpeed * Time.deltaTime;
        
        transform.position = Paths[pathIndex].path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        PlayerRotate.rotation = Paths[pathIndex].path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        if (Paths[pathIndex].path.ReachFinal(distanceTravelled))
        {
            OnFinalPointOfCurrentPath();
        }
    }

    private void OnFinalPointOfCurrentPath()
    {
        pathIndex++;
        distanceTravelled = 0.02f;
        if(pathIndex>=Paths.Count)
            OnFinalPointOfFinalPath();
        else
        {
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
        Vector3 beginOfNextPath = Paths[pathIndex].path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        PlayerRotate.DORotateQuaternion(Paths[pathIndex].path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction),.5f).SetEase(Ease.Linear);
        
        transform.DOMove(beginOfNextPath, .5f).OnComplete((() =>
        {
            PlayerGroundState = PlayerGroundState.OnGround;
        })).SetEase(Ease.Linear);
    }
    private float Remap (float value, float from1, float to1, float from2, float to2) {
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