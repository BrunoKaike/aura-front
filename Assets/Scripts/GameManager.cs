using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance{
        get {
            if (_instance == null)
            {   
                Debug.LogError("GameManager is null");
            }
            return _instance;
        }
    }
    private void Awake(){
        _instance = this;
    }
  
}
