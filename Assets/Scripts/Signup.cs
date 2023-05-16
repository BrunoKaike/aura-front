using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Signup : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_InputField email;
    public TMP_InputField nickname;
    void Start()
    {
    }

    public void sendCredentials(){
        WebTalker.Instance.Signup(username.text, password.text, email.text, nickname.text);
    }
}
