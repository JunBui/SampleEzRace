using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float HitForce;

    private void Start()
    {
        PostStart();
        throw new NotImplementedException();
    }

    public virtual void PostStart()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDetect"))
        {
            Player hitPlayer = other.GetComponent<Player>();
            if(hitPlayer!=null)
                HitPlayer(hitPlayer);
        }
    }

    public virtual void HitPlayer(Player player)
    {
        Debug.Log(this.gameObject.name + " --- hit player");
        player.PlayerGetHit(HitForce);
    }
}
