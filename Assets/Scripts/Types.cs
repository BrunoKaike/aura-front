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
public class FriendRequest
{
    public int id;
    public string senderId;
    public string receiverId;
    public string status;
    public DateTime createdAt;
    public DateTime updatedAt;
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
