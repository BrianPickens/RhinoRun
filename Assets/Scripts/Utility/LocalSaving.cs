using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LocalSaving
{

    public static void SaveLocalInt(int _value, string _saveString)
    {
        PlayerPrefs.SetInt(_saveString, _value);
    }

    public static void SaveLocalFloat(float _value, string _saveString)
    {
        PlayerPrefs.SetFloat(_saveString, _value);
    }

    public static void SaveLocalBool(bool _value, string _saveString)
    {
        PlayerPrefs.SetInt(_saveString, _value ? 1 : 0);
    }

    public static void SaveLocalString(string _value, string _saveString)
    {
        PlayerPrefs.SetString(_saveString, _value);
    }

    public static int GetLocalInt(string _saveString, int _defaultValue = 0)
    {
        int localInt = PlayerPrefs.GetInt(_saveString, _defaultValue);
        return localInt;
    }

    public static float GetLocalFloat(string _saveString, float _defaultValue = 0f)
    {
        float localFloat = PlayerPrefs.GetFloat(_saveString, _defaultValue);
        return localFloat;
    }

    public static bool GetLocalBool(string _saveString, int _defaulValue = 0)
    {
        bool localBool = PlayerPrefs.GetInt(_saveString, _defaulValue) == 1 ? true : false;
        return localBool;
    }

    public static string GetLocalString(string _saveString, string _defaultValue = "")
    {
        string localString = PlayerPrefs.GetString(_saveString, _defaultValue);
        return localString;
    }

    public static void ResetLocalData()
    {
        PlayerPrefs.DeleteAll();
    }

}
