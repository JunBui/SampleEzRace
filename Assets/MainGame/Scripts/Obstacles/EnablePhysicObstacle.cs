using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePhysicObstacle : Obstacle
{
    public Rigidbody rb;
    public override void PostStart()
    {
        base.PostStart();
        rb.isKinematic = true;
    }

    public override void HitPlayer(Player player)
    {
        base.HitPlayer(player);
        rb.isKinematic = false;
    }
}
