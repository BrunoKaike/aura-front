using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Login : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    void Start()
    {
    }

    public void sendCredentials(){
        WebTalker.Instance.Login(username.text, password.text);
    }
}
