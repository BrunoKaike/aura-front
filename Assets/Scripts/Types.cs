using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Session
{
    public string sessionId;
}

[System.Serializable]
public class DataObject
{
    public FriendRequestReceiver[] receiver;
}

[System.Serializable]
public class FriendRequestReceiver
{
    public string sender;
    public DateTime sentAt;
    public string requestId;
}

[System.Serializable]
public class Friend
{
    public DateTime createdAt;
    public DateTime updatedAt;
    public string nickname;
}

public class TypesObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
