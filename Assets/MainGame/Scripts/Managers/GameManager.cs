using System.Collections;
using System.Collections.Generic;
using Modules.DesignPatterns.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMono<GameManager>
{
    public GameObject EndingCamera;
    public GameObject speedEffect;
    private ParticleSystem speedEffectParticles;
    public GameState GameState = GameState.HomePreview;

    public PlayerMovement PlayerMovement;
    // Start is called before the first frame update
    void Start()
    {
        MenuHome.Show();
        speedEffectParticles = speedEffect.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameState == GameState.Running)
        {
            speedEffectParticles.emissionRate = (PlayerMovement.currentMoveSpeed*3);
            if (MenuMainGame.Instance != null)
            {
                MenuMainGame.Instance.SetSpeedUi(PlayerMovement.currentMoveSpeed,PlayerMovement.MaxSpeed);
            }
        }
    }

    public void StartGame()
    {
        if (GameState == GameState.HomePreview)
        {
            MenuMainGame.Show();
            GameState = GameState.Running;
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void WinGame()
    {
        if (GameState == GameState.Running)
        {
            GameState = GameState.Wining;
            EndingCamera.SetActive(true);
            Debug.Log("Win game");
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