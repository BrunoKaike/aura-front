using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static TypesObjects;
using System.Collections;

public class FriendListRequestManager : MonoBehaviour
{
    public GameObject friendItemPrefab;
    public Transform friendListContainer;
    string server = "://localhost:3000/";
    
    private List<FriendRequestReceiver> friendRequestList = new List<FriendRequestReceiver>();

    private void Start()
    {
        StartCoroutine(GetRequest("http" + server + "user/pendingFriendshipRequests"));
        
    }

    private void CreateFriendList()
    {
        foreach (FriendRequestReceiver friendRequest in friendRequestList)
        {
            GameObject friendItem = Instantiate(friendItemPrefab, friendListContainer);
            FriendRequestItem friendItemScript = friendItem.GetComponent<FriendRequestItem>();
            friendItemScript.Initialize(friendRequest.sender, friendRequest.requestId);
        }
    }

    IEnumerator GetRequest(string uri) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri)) {
            // Request and wait for the desired page.
            webRequest.SetRequestHeader("Content-Type", "application/json");
            Debug.Log(PlayerPrefs.GetString("SessionID"));
            webRequest.SetRequestHeader("aura-auth", PlayerPrefs.GetString("SessionID"));
            yield return webRequest.SendWebRequest();
 
            if (webRequest.isNetworkError) {
                Debug.Log("Error While Getting: " + webRequest.error);
            }
            else {
                string friendRequestsJson = webRequest.downloadHandler.text;
                
                DataObject dataObject = JsonUtility.FromJson<DataObject>(friendRequestsJson);

                // Get the list of receiver objects
                FriendRequestReceiver[] receiverList = dataObject.receiver;

                foreach (var ro in receiverList)
                {
                    friendRequestList.Add(ro);
                }

                Debug.Log("Received: " + webRequest.downloadHandler.text);
                CreateFriendList();
            }
        }
    }
}
