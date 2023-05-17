using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static TypesObjects;
using WebSocketSharp;

public class response 
{
    public string type;
}
public class introGame 
{
    public string type;
    public string data;
    public int checkpoint;
}
public class startGame 
{
    public string type;
    public string data;
    public int player1;
    public int player2;
    public int player3;
    public int player4;
    public int player;
}
public class WebTalker : MonoBehaviour
{
    private static WebTalker _instance;
    UnityWebRequest uwr;
    public static WebTalker Instance{
        get {
            if (_instance == null)
            {   
                Debug.LogError("WebTalker is null");
            }
            return _instance;
        }
    }
    private void Awake(){
        _instance = this;
    }
  WebSocket websocket;
  string newMessage;
  string server = "://localhost:3000/";
  // Start is called before the first frame update
   void Start()
  {
    websocket = new WebSocket("ws" +server);
    websocket.OnOpen += (sender, args) =>
    {
      Debug.Log("Open!");
    };

    websocket.OnClose += (sender, args) =>
    {
      Debug.Log("Closed!");
    };

    websocket.OnMessage += (sender, bytes) =>
    {
      Debug.Log("OnMessage!");
      string stringMessage = bytes.Data;

      if(JsonUtility.FromJson<response>(stringMessage).type == "introGame"){
        HandleEntrar(stringMessage);
      }else if(JsonUtility.FromJson<response>(stringMessage).type == "atGame"){
        
      }else{
        Debug.Log("tipo de mensagem não reconhecido");
        Debug.Log(JsonUtility.FromJson<response>(stringMessage).type);
      }
    };
    // waiting for messages
     websocket.Connect();

  }

  void Update()
  {
    /*#if !UNITY_WEBGL || UNITY_EDITOR
      websocket.Send(newMessage);
    #endif*/
  }

    public void Login(string username,string senha){

          //websocket.Origin ="ws" + server + "user/login";
          String message = @"{ ""username"" : " + "\"" + username + "\",";
          message += @"""password"" : """ + senha + "\"}";

          StartCoroutine(postRequest("http" + server + "user/login", message));
          
          //websocket.Send(message);
          //await websocket.SendText(@"{ ""inGame"" : ""true"", ""type"" : ""introGame"", ""position"" : ""variable"", ""idGame"": ""token"" }");
    }

    public void Signup(string username,string senha, string email, string nickname){

          //websocket.Origin ="ws" + server + "user/login";
          String message = @"{ ""username"" : " + "\"" + username + "\",";
          message += @"""password"" : """ + senha + "\",";
          message += @"""email"" : """ + email + "\",";
          message += @"""nickname"" : """ + nickname + "\"}";
          

          StartCoroutine(postRequest("http" + server + "user/signup", message));
          
          //websocket.Send(message);
          //await websocket.SendText(@"{ ""inGame"" : ""true"", ""type"" : ""introGame"", ""position"" : ""variable"", ""idGame"": ""token"" }");
    }

    IEnumerator postRequest(string url, string json){

          uwr = new UnityWebRequest(url,"POST");
          byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
          Debug.Log(System.Text.Encoding.UTF8.GetString(jsonToSend));
          uwr.uploadHandler = new UploadHandlerRaw(jsonToSend) as UploadHandler;
          uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
          uwr.SetRequestHeader("Content-Type", "application/json");

          yield return uwr.SendWebRequest();

          if (uwr.isNetworkError)
          {
              Debug.Log("Error While Sending: " + uwr.error);
          }
          else
          {
              if(url == "http" + server + "user/login"){
                
                string sessionJson = uwr.downloadHandler.text;
                Session data = JsonUtility.FromJson<Session>(sessionJson);
                Debug.Log("Valor da variável no JSON: " + data.sessionId);
                if(data.sessionId != ""){
                  string sessionID = data.sessionId; 
                  PlayerPrefs.SetString("SessionID", sessionID); 
                  PlayerPrefs.Save();
                }

              }
              Debug.Log("Received: " + uwr.downloadHandler.text);
          }

    }

    public void HandleEntrar(string resposta){
      introGame data = JsonUtility.FromJson<introGame>(resposta);
      if (data.data == "success")
      {
      //  GameManager.Instance.playerCheckpoint = data.checkpoint;
      //  GameManager.Instance.Espera();
      }else{
        Debug.Log(data.data);
      }
    }
    public  void SendComecar(){
        if (websocket != null)
        {
            String message = @"{ ""inGame"" : ""true"", ""type"" : ""startGame"", ""id"" : ";
         //   message += @"""" + GameManager.Instance.codigo + @""" }";
            Debug.Log("menssagem: "  + message);
             websocket.Send(message);
            //await websocket.SendText(@"{ ""inGame"" : ""true"", ""type"" : ""introGame"", ""position"" : ""variable"", ""idGame"": ""token"" }");
        }
    }
    public  void SendWebSocketEnterRoom()
    {
        if (websocket != null)
        {
             websocket.Send(@"{ ""inGame"" : ""true"", ""type"" : ""introGame"", ""position"" : ""variable"", ""idGame"": ""token"" }");
        }
    }
  private  void OnApplicationQuit()
  {
     websocket.Close();
  }

}