using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static void SetCurrentLevelId(int id)
    {
        if (id <= 0)
            id = 0;
        PlayerPrefs.SetInt("current_level_id", id);
    }

    public static int GetCurrentLevelId()
    {
        return PlayerPrefs.GetInt("current_level_id", 0);
    }

    public static void SetCurrentLevelText(int id)
    {
        PlayerPrefs.SetInt("current_level_text", id);
    }

    public static int GetCurrentLevelText()
    {
        return PlayerPrefs.GetInt("current_level_text", 0);
    }

    public static List<int> GetLevelHadGetKey()
    {
        List<int> levels = new List<int>();
        string text = PlayerPrefs.GetString("level_had_get_key_item_list", "");
        if (string.IsNullOrEmpty(text))
        {
            levels = new List<int>();
            return levels;
        }

        levels = JsonConvert.DeserializeObject<List<int>>(text);
        return levels;
    }
    public static void AddLevelHadGetKey(int level)
    {
        List<int> levels = GetLevelHadGetKey();
        if (levels.Contains(level) == false)
        {
            levels.Add(level);
            PlayerPrefs.SetString("level_had_get_key_item_list", JsonConvert.SerializeObject(levels));
        }
    }
}
