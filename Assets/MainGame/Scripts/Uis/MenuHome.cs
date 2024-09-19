using System.Collections;
using System.Collections.Generic;
using Modules.Systems.MenuSystem;
using UnityEngine;
using UnityEngine.UI;

public class MenuHome : SimpleMenu<MenuHome>
{
    public Button PlayButton;
    // Start is called before the first frame update
    void Start()
    {
        PlayButton.onClick.AddListener((() =>
        {
            GameManager.Instance.StartGame();
            Hide();
        }));
    }
}
