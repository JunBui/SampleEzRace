using System.Collections;
using System.Collections.Generic;
using Modules.Systems.MenuSystem;
using UnityEngine;
using UnityEngine.UI;

public class MenuMainGame : SimpleMenu<MenuMainGame>
{
    public Image MaxSpeedProgress;
    public Image LevelProgress;

    public Text SpeedText;
    public Text LevelText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetSpeedUi(float current, float total)
    {
        MaxSpeedProgress.fillAmount = Mathf.Lerp(.362f, 1, current / total);
        SpeedText.text = current.ToString("N0");
    }
}
