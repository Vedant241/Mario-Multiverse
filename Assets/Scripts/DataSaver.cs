using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Database;

[System.Serializable]
public class datattoSave
{
    [SerializeField]
    private string _username;
    public string Username
    {
        get { return _username; }
        set { _username = value; }
    }
}

public class DataSaver : MonoBehaviour
{
    public datattoSave dts;
    public string userId;
    DatabaseReference dbRef;

    private void Awake()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }
    public void Savedata()
    {
        string json = JsonUtility.ToJson(dts);
        dbRef.Child("users").Child(userId).SetRawJsonValueAsync(json);
    }
    public void LoadData()
    {

    }
}
