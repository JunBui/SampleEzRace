using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator SkinAnim;
    public Animator SkateAnim;

    public PlayerMovement PlayerMovement;

    public float currentInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (Mathf.Abs(PlayerMovement.currrentLeftRightValue - PlayerMovement.targetLeftRightValue) >= .01f)
        {
            currentInput =  (PlayerMovement.targetLeftRightValue - PlayerMovement.currrentLeftRightValue)/1.3f;
        }
        else
        {
            currentInput = Mathf.MoveTowards(currentInput, 0, 4 * Time.deltaTime);
        }
        SkinAnim.SetFloat("AngleDelta",currentInput);
        SkinAnim.SetFloat("DriftX",PlayerMovement.XDriftPath);
        SkinAnim.SetFloat("DriftZ",PlayerMovement.currentMoveSpeed/PlayerMovement.MaxSpeed);
        SkinAnim.SetBool("IsBoardStart", PlayerMovement.StartGainSpeed);
        SkinAnim.SetBool("RotateJump", PlayerMovement.PlayerGroundState == PlayerGroundState.Flying);
        SkateAnim.SetBool("Rotate", PlayerMovement.PlayerGroundState == PlayerGroundState.Flying);
    }
}
