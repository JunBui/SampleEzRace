using System;
using System.Collections;
using System.Collections.Generic;
using Modules.DesignPatterns.EventManager;
using Unity.VisualScripting;
using UnityEngine;

public class AdsTmp 
{
    public static void GetReward(Action<bool> Success)
    {
        Success?.Invoke(true);
    }
}
