using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelaUsuario : MonoBehaviour
{
    public TMP_Text welcome;
    public TMP_Text letra;
    // Start is called before the first frame update
    void Start()
    {
        string nick = PlayerPrefs.GetString("SessionUsername");
        welcome.text = "Bem vindo(a), " + nick;
        letra.text = nick.ToUpper()[0] + "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
