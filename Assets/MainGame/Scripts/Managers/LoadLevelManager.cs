using System.Collections;
using System.Collections.Generic;
using Modules.DesignPatterns.EventManager;
using Modules.DesignPatterns.Singleton;
using Modules.ThirdParties.EditorButton;
using UnityEngine;

public class LoadLevelManager : SingletonMono<LoadLevelManager>
{
    public List<GameObject> Levels;
    public LevelObject CurrentLevel;
    public int currentLevelId;
    private static int lastLevelId = -1;

    private void Start()
    {
        SpawnLevel();
    }

    private void SpawnLevel()
    {
        int LevelIndex = SaveManager.GetCurrentLevelId();
        LevelIndex = Mathf.Clamp(LevelIndex, 0, Levels.Count - 1);
        if (LevelIndex >= Levels.Count)
        {
            if (lastLevelId == -1)
                LevelIndex = Random.Range(5, Levels.Count);
            else
                LevelIndex = lastLevelId;
        }

        lastLevelId = LevelIndex;
        currentLevelId = LevelIndex;
        GameObject level = Instantiate(Levels[LevelIndex]);
        CurrentLevel = level.GetComponent<LevelObject>();
        level.transform.position = Vector3.zero;
        EventManager.Instance.TriggerEvent(new GameEvents.OnSpawnLevelComplete());
    }

    [EditorButton]
    public void NextLevel()
    {
        lastLevelId = -1;
        int LevelIndex = SaveManager.GetCurrentLevelId();
        LevelIndex++;
        int index = Mathf.Clamp(LevelIndex, 0, Levels.Count);
        SaveManager.SetCurrentLevelId(index);
        SaveManager.SetCurrentLevelText(SaveManager.GetCurrentLevelText() + 1);
        GameManager.Instance.RestartGame();
    }
}
