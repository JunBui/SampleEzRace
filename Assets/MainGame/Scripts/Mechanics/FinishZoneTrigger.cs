using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerDetect"))
        {
            GameManager.Instance.WinGame();
        }
    }
}
