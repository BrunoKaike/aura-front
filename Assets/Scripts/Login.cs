using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
     public string username;
    public string password;
    void Start()
    {
    }

    public void setUsername(string username){
        this.username = username;
        Debug.Log(username);
    }
    
    public void setPassword(string password){
        this.password = password;
    }

    public void sendCredentials(){
        WebTalker.Instance.Login(username, password);
    }
}
