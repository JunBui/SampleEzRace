using System.Collections;
using System.Collections.Generic;
using Modules.DesignPatterns.EventManager;
using UnityEngine;

public class GameEvents 
{
    public class OnPlayerReachFinish : IEventParameterBase
    {
    }
    public class OnPlayerGrabObject: IEventParameterBase
    {
    }
    public class OnPlayerDeath : IEventParameterBase
    {
    }
    public class OnPlay: IEventParameterBase
    {
    }
    public class OnSpawnLevelComplete : IEventParameterBase
    {
            
    }
    public class OnSpawnPlayerComplete: IEventParameterBase
    {
            
    }
    public class BackFromShopEvent: IEventParameterBase
    {
            
    }
    public class OpenShopEvent: IEventParameterBase
    {
            
    }

    public class OnClickUpgradeRoomItem : IEventParameterBase
    {
    }

    public class OnClickChooseDecorationRoom : IEventParameterBase
    {
    }
}
