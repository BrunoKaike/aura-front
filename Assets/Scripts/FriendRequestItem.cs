using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class FriendRequestItem : MonoBehaviour
{
    public TMP_Text username;
    public Button acceptButton;
    public Button rejectButton;
    private string requestId;
    private string server = "://localhost:3000/";
    private UnityWebRequest uwr;

    public void Initialize(string friendName, string requestId)
    {
        username.text = friendName;
        this.requestId = requestId;
        acceptButton.onClick.AddListener(() => AcceptFriend(friendName));
        rejectButton.onClick.AddListener(() => RejectFriend(friendName));
    }

    private void AcceptFriend(string friendName)
    {
        string message = @"{ ""response"" : ""yes""}";

        StartCoroutine(postRequest("http" + server + "user/friendshipRequest/"+this.requestId+"/respond", message));
    }

    private void RejectFriend(string friendName)
    {
        string message = @"{ ""response"" : ""yes""}";

        StartCoroutine(postRequest("http" + server + "user/friendshipRequest/"+this.requestId+"/respond", message));
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
              Debug.Log("Received: " + uwr.downloadHandler.text);
          }

    }
}
