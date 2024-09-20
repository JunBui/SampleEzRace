using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator SkinAnim;
    public Animator SkateAnim;

    public PlayerMovement PlayerMovement;

    [Header("Vfx")]
    public GameObject JumpVfx;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        SkinAnim.SetFloat("AngleDelta",PlayerMovement.CurrentInputLeftRight);
        SkinAnim.SetFloat("DriftX",PlayerMovement.XDriftPath);
        SkinAnim.SetFloat("DriftZ",PlayerMovement.currentMoveSpeed/PlayerMovement.MaxSpeed);
        SkinAnim.SetBool("IsBoardStart", PlayerMovement.StartGainSpeed);
        SkinAnim.SetBool("RotateJump", PlayerMovement.PlayerGroundState == PlayerGroundState.Flying);
        SkateAnim.SetBool("Rotate", PlayerMovement.PlayerGroundState == PlayerGroundState.Flying);
        JumpVfx.SetActive(PlayerMovement.PlayerGroundState == PlayerGroundState.Flying);
    }
}
