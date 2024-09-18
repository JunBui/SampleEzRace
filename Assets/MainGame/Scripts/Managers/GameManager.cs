using System.Collections;
using System.Collections.Generic;
using Modules.DesignPatterns.Singleton;
using UnityEngine;

public class GameManager : SingletonMono<GameManager>
{
    
    public GameObject speedEffect;
    private ParticleSystem speedEffectParticles;
    public GameState GameState = GameState.HomePreview;

    public PlayerMovement PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        speedEffectParticles = speedEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (GameState == GameState.Running)
        {
            speedEffectParticles.emissionRate = (PlayerMovement.currentMoveSpeed*3);

        }
    }
}


public enum GameState
{
    HomePreview,
    Running,
    Pause,
    Wining,
    Lose
}