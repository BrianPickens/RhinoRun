using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public static class CloudSaving
{
    [DllImport("__Internal")]
    private static extern void iCloudKV_Synchronize();

    [DllImport("__Internal")]
    private static extern void iCloudKV_SetInt(string key, int value);

    [DllImport("__Internal")]
    private static extern void iCloudKV_SetFloat(string key, float value);

    [DllImport("__Internal")]
    private static extern void iCloudKV_SetString(string key, string value);

    [DllImport("__Internal")]
    private static extern int iCloudKV_GetInt(string key);

    [DllImport("__Internal")]
    private static extern float iCloudKV_GetFloat(string key);

    [DllImport("__Internal")]
    private static extern string iCloudKV_GetString(string key);

    [DllImport("__Internal")]
    private static extern void iCloudKV_Reset();

    public static void UpdateCloudData()
    {
        iCloudKV_Synchronize();
    }

    public static void SaveCloudInt(int _value, string _saveString)
    {
        iCloudKV_SetInt(_saveString, _value);
    }

    public static void SaveCloudFloat(float _value, string _saveString)
    {
        iCloudKV_SetFloat(_saveString, _value);
    }

    public static void SaveCloudString(string _value, string _saveString)
    {
        iCloudKV_SetString(_saveString, _value);
    }

    public static int GetCloudInt(string _saveString)
    {
        int cloudInt = iCloudKV_GetInt(_saveString);
        return cloudInt;
    }

    public static float GetCloudFloat(string _saveString)
    {
        float cloudFloat = iCloudKV_GetFloat(_saveString);
        return cloudFloat;
    }

    public static string GetCloudString(string _saveString)
    {
        string cloudString = iCloudKV_GetString(_saveString);
        return cloudString;
    }

    public static void ResetCloud()
    {
        iCloudKV_Reset();
    }

}
