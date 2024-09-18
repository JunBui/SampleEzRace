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
        if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") > .1f)
            {
                currentInput = Mathf.MoveTowards(currentInput, 1, 7 * Time.deltaTime);
            }
            else if (Input.GetAxis("Mouse X") < -.1f)
            {
                
                currentInput = Mathf.MoveTowards(currentInput, -1, 7 * Time.deltaTime);
            }
            else
            {
                currentInput = Mathf.MoveTowards(currentInput, 0, 4 * Time.deltaTime);
            }
        }
        else
        {
            currentInput = Mathf.MoveTowards(currentInput, 0, 7 * Time.deltaTime);
        }
        SkinAnim.SetFloat("AngleDelta",currentInput);
        SkinAnim.SetFloat("DriftX",PlayerMovement.XDriftPath);
        SkinAnim.SetFloat("DriftZ",PlayerMovement.currentMoveSpeed/PlayerMovement.MaxSpeed);
        SkinAnim.SetBool("IsBoardStart", PlayerMovement.StartGainSpeed);
        SkinAnim.SetBool("RotateJump", PlayerMovement.PlayerGroundState == PlayerGroundState.Flying);
        SkateAnim.SetBool("Rotate", PlayerMovement.PlayerGroundState == PlayerGroundState.Flying);
    }
}
