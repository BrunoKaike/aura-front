using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
  // Start is called before the first frame update
  async void Start()
  {
    websocket = new WebSocket("ws://localhost:3000");

    /*websocket.OnOpen += () =>
    {
      Debug.Log("Connection open!");
    };

    websocket.OnError += (e) =>
    {
      Debug.Log("Error! " + e);
    };

    websocket.OnClose += (e) =>
    {
      Debug.Log("Connection closed!");
    };*/

    websocket.OnMessage += (sender, bytes) =>
    {
      Debug.Log("OnMessage!");
      string stringMessage = bytes.Data;

      if(JsonUtility.FromJson<response>(stringMessage).type == "introGame"){
        HandleEntrar(stringMessage);
      }else if(JsonUtility.FromJson<response>(stringMessage).type == "startGame"){
        HandleIniciar(stringMessage);
      }else if(JsonUtility.FromJson<response>(stringMessage).type == "atGame"){
        
      }else{
        Debug.Log("tipo de mensagem não reconhecido");
        Debug.Log(JsonUtility.FromJson<response>(stringMessage).type);
      }
    };
    // waiting for messages
    //await websocket.Connect();
  }

  void Update()
  {
    #if !UNITY_WEBGL || UNITY_EDITOR
      websocket.Send(newMessage);
    #endif
  }
    public async void SendCodigo(string name,string codigo){
        if (websocket != null)
        {
          GameManager.Instance.codigo = codigo;
          GameManager.Instance.nome = name;
          String message = @"{ ""inGame"" : ""true"", ""type"" : ""introGame"", ""nameId"" : ";
          message += @"""" + codigo + @""", ""name"" : ";
          message += @"""" + name + @""""+"}";
          Debug.Log("menssagem: "  + message);
           websocket.Send(message);
          //await websocket.SendText(@"{ ""inGame"" : ""true"", ""type"" : ""introGame"", ""position"" : ""variable"", ""idGame"": ""token"" }");
        }
    }
    public void HandleEntrar(string resposta){
      introGame data = JsonUtility.FromJson<introGame>(resposta);
      if (data.data == "success")
      {
        GameManager.Instance.playerCheckpoint = data.checkpoint;
        GameManager.Instance.Espera();
      }else{
        Debug.Log(data.data);
      }
    }
    public async void SendComecar(){
        if (websocket != null)
        {
            String message = @"{ ""inGame"" : ""true"", ""type"" : ""startGame"", ""id"" : ";
            message += @"""" + GameManager.Instance.codigo + @""" }";
            Debug.Log("menssagem: "  + message);
             websocket.Send(message);
            //await websocket.SendText(@"{ ""inGame"" : ""true"", ""type"" : ""introGame"", ""position"" : ""variable"", ""idGame"": ""token"" }");
        }
    }
    public void HandleIniciar(string resposta){
      startGame data = JsonUtility.FromJson<startGame>(resposta);
      if (data.data == GameManager.Instance.codigo)
      {
        Debug.Log("entrou no start");
        GameManager.Instance.jogadorId = data.player;
        if (data.player1 > -1)
        {
          GameManager.Instance.Inicio(data.player1);
        }
        if (data.player2 > -1)
        {
          GameManager.Instance.Inicio(data.player2);
        }
        if (data.player3 > -1)
        {
          GameManager.Instance.Inicio(data.player3);
        }
        if (data.player4 > -1)
        {
          GameManager.Instance.Inicio(data.player4);
        }
        Debug.Log("desativou");
        GameManager.Instance.TelaEspera.SetActive(false);
      }else{
        Debug.Log(data.data);
      }
    }
    public async void SendWebSocketEnterRoom()
    {
        if (websocket != null)
        {
             websocket.Send(@"{ ""inGame"" : ""true"", ""type"" : ""introGame"", ""position"" : ""variable"", ""idGame"": ""token"" }");
        }
    }
  private async void OnApplicationQuit()
  {
     websocket.Close();
  }

}