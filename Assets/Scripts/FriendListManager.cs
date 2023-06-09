using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using static TypesObjects;
using System.Collections;
using System.Text.Json;

using UnityEngine;

public static class JsonHelper
{
    public static List<T> FromJson<T>(string jsonString)
    {
        string newJsonString = "{\"items\":" + jsonString + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJsonString);
        return wrapper.items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public List<T> items;
    }
}


public class FriendListManager : MonoBehaviour
{
    public GameObject friendItemPrefab;
    public Transform friendListContainer;
    string server = "://localhost:3000/";
    
    public List<Friend> friendList = new List<Friend>();

    private void Start()
    {
        StartCoroutine(GetRequest("http" + server + "user/friends"));
        
    }

    private void CreateFriendList()
    {
        foreach (Friend friend in friendList)
        {
            GameObject friendItem = Instantiate(friendItemPrefab, friendListContainer);
            FriendItem friendItemScript = friendItem.GetComponent<FriendItem>();
            friendItemScript.Initialize(friend.nickname);
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
                string sessionJson = webRequest.downloadHandler.text;
                Debug.Log("Received: " + webRequest.downloadHandler.text);
                
                friendList = JsonHelper.FromJson<Friend>(sessionJson);

                Debug.Log("Received: " + webRequest.downloadHandler.text);
                CreateFriendList();
            }
        }
    }
}
