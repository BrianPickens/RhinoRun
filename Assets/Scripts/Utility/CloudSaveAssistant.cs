﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CloudSaveAssistant : MonoBehaviour
{

    private struct SaveInfo
    {
        public string saveData;
        public string saveString;
    }

    private bool saving;

    private string loadedData;
    public string LoadedData
    {
        get { return loadedData; }
    }

    private bool loading;
    public bool Loading
    {
        get { return loading; }
    }

    private List<SaveInfo> saveAttempts = new List<SaveInfo>();

    public void CreateNewCloudSaveAttempt(string _saveData, string _saveString)
    {
        SaveInfo newSave = new SaveInfo();
        newSave.saveData = _saveData;
        newSave.saveString = _saveString;
        saveAttempts.Add(newSave);
        if (!saving)
        {
            StartCoroutine(SaveRoutine());
        }
    }

    private IEnumerator SaveRoutine()
    {
        saving = true;
        for (int i = 0; i < saveAttempts.Count; i++)
        {
            //Debug.Log("attempting cloud save");
            //get the last save string
            string lastSave = CloudSaving.GetCloudString(saveAttempts[i].saveString);
            ///set the new string to cloud
            CloudSaving.SaveCloudString(saveAttempts[i].saveData, saveAttempts[i].saveString);
            //tell it to update
            CloudSaving.UpdateCloudData();
            //set time out time on save
            float timeOutSaveTime = 10f;

            //now as long as the cloud string is equal to what it was before we sent the new string, then it hasn't updated
            while (CloudSaving.GetCloudString(saveAttempts[i].saveString) == lastSave)
            {
                //Debug.Log("saving to cloud");
                timeOutSaveTime -= Time.deltaTime;
                if (timeOutSaveTime <= 0)
                {
                    //Debug.Log("cloud save failed");
                    break;
                }
                yield return null;
            }

        }
        //Debug.Log("save ending");
        saveAttempts.Clear();
        saving = false;
    }

    public void LoadCloudSaveData(string _saveString)
    {
        loading = true;
        StartCoroutine(LoadRoutine(_saveString));
    }

    private IEnumerator LoadRoutine(string _saveString)
    {

        string tempLoadString = "loading";
        string loadedDataString = tempLoadString;
        //updated data
        CloudSaving.UpdateCloudData();
        //grab the new data
        loadedDataString = CloudSaving.GetCloudString(_saveString);
        //set time out time
        float timeOutLoadTime = 4f;

        //while the loaded data is eqaup to our temp string, the data hasn't loaded
        while (loadedDataString == tempLoadString)
        {
           // Debug.Log("loading");
            timeOutLoadTime -= Time.deltaTime;
            if (timeOutLoadTime <= 0)
            {
                //Debug.Log("cloud Load failed");
                break;
            }
            yield return null;
        }
        loadedData = loadedDataString;
        loading = false;

    }
}
