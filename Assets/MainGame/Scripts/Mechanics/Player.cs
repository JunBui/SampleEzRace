using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator SkinAnim;
    public Animator SkateAnim;

    public PlayerMovement PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        SkinAnim.SetFloat("AngleDelta",PlayerMovement.Remap(PlayerMovement.XoffSet/Screen.width,0,1,-1,1));
        // SkinAnim.SetFloat("AngleDelta",Input.GetAxis("Mouse X")*3);
        SkinAnim.SetFloat("DriftX",PlayerMovement.XDriftPath);
        SkinAnim.SetFloat("DriftZ",PlayerMovement.currentMoveSpeed/PlayerMovement.MaxSpeed);
        SkinAnim.SetBool("IsBoardStart", PlayerMovement.StartGainSpeed);
        SkinAnim.SetBool("RotateJump", PlayerMovement.PlayerGroundState == PlayerGroundState.Flying);
        SkateAnim.SetBool("Rotate", PlayerMovement.PlayerGroundState == PlayerGroundState.Flying);
    }
}
