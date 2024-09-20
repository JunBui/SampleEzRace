using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDetect"))
        {
            HitPlayer();
        }
    }

    public virtual void HitPlayer()
    {
        Debug.Log(this.gameObject.name + " --- hit player");
    }
}
