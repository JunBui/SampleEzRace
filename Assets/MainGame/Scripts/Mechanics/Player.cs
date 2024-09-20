using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator SkinAnim;
    public Animator SkateAnim;
    public float DizzyTime = 1;

    public PlayerMovement PlayerMovement;

    public bool IsGetHit;

    [Header("Vfx")]
    public GameObject JumpVfx;

    public List<MeshRenderer> MeshRenderers;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        //Set Animation
        SkinAnim.SetFloat("AngleDelta",PlayerMovement.CurrentInputLeftRight/2);
        SkinAnim.SetFloat("DriftX",PlayerMovement.XDriftPath);
        SkinAnim.SetFloat("DriftZ",PlayerMovement.currentMoveSpeed/PlayerMovement.MaxSpeed);
        SkinAnim.SetBool("IsBoardStart", PlayerMovement.StartGainSpeed);
        SkinAnim.SetBool("RotateJump", PlayerMovement.PlayerGroundState == PlayerGroundState.Flying);
        SkateAnim.SetBool("Rotate", PlayerMovement.PlayerGroundState == PlayerGroundState.Flying);
        SkateAnim.SetBool("Dizzy", IsGetHit);
        
        JumpVfx.SetActive(PlayerMovement.PlayerGroundState == PlayerGroundState.Flying);
    }

    public void PlayerGetHit(float hitForce)
    {
        if (IsGetHit == false)
        {
            PlayerMovement.PlayerGetHit(hitForce);
            IsGetHit = true;
            StartCoroutine(ResetPlayerMesh());
            DOVirtual.DelayedCall(DizzyTime, (() =>
            {
                IsGetHit = false;
            }));
        }
    }

    IEnumerator ResetPlayerMesh()
    {
        //     Vector3 PushDir = new Vector3(0, MaxPushUp*2, 0);
        //     AddForcePoint.velocity = PushDir * PushPower;
        for (int i = 0; i < 5; i++)
        {
            foreach (var meshRenderer in MeshRenderers)
            {
                meshRenderer.gameObject.SetActive(!meshRenderer.gameObject.activeInHierarchy);
            }
            yield return new WaitForSeconds(.3f);
        } 
        foreach (var meshRenderer in MeshRenderers)
        {
            meshRenderer.gameObject.SetActive(true);
        }
        yield return null;
    }
}
