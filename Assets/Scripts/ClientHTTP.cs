/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClientHTTP : MonoBehaviour
{

    const string LOGIN = "http://localhost:3000/user/login";
    const string SIGNUP = "http://localhost:3000/user/signup";
    const string GETFRIENDS = "http://localhost:3000/user/friends";
    const string GETFRIENDREQUESTS = "http://localhost:3000/user/pendingFriendshipRequests";

    UnityWebRequest uwr;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login(string username,string senha){

          string message = @"{ ""username"" : " + "\"" + username + "\",";
          message += @"""password"" : """ + senha + "\"}";

          StartCoroutine(postRequest(LOGIN, message));
                    
    }

    public void Signup(string username,string senha, string email, string nickname){

          string message = @"{ ""username"" : " + "\"" + username + "\",";
          message += @"""password"" : """ + senha + "\",";
          message += @"""email"" : """ + email + "\",";
          message += @"""nickname"" : """ + nickname + "\"}";
          

          StartCoroutine(postRequest(SIGNUP, message));
          
    }

    IEnumerator postRequest(string url, string json){

          uwr = new UnityWebRequest(url,"POST");
          byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
          Debug.Log(System.Text.Encoding.UTF8.GetString(jsonToSend));
          uwr.uploadHandler = new UploadHandlerRaw(jsonToSend) as UploadHandler;
          uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
          uwr.SetRequestHeader("Content-Type", "application/json");
          uwr.SetRequestHeader("aura-auth", PlayerPrefs.GetString("SessionID"));

          yield return uwr.SendWebRequest();

          if (uwr.isNetworkError)
          {
              Debug.Log("Error While Sending: " + uwr.error);
          }
          else
          {
              if(url == LOGIN){

                string sessionJson = uwr.downloadHandler.text;
                Session data = JsonUtility.FromJson<Session>(sessionJson);
                Debug.Log("Valor da variável no JSON: " + data.sessionId);
                string sessionID = data.sessionId; 
                PlayerPrefs.SetString("SessionID", sessionID); 
                PlayerPrefs.Save();

              } else if(url == GETFRIENDS){

              }

              Debug.Log("Received: " + uwr.downloadHandler.text);
          }

    }


}*/
