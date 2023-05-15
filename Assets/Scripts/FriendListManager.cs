using System.Collections.Generic;
using UnityEngine;

public class FriendListManager : MonoBehaviour
{
    public GameObject friendItemPrefab;
    public Transform friendListContainer;
    
    private List<string> friendList = new List<string>()
    {
        "Amigo 1",
        "Amigo 2",
        "Amigo 3"
    };

    private void Start()
    {
        CreateFriendList();
    }

    private void CreateFriendList()
    {
        foreach (string friendName in friendList)
        {
            GameObject friendItem = Instantiate(friendItemPrefab, friendListContainer);
            FriendItem friendItemScript = friendItem.GetComponent<FriendItem>();
            friendItemScript.Initialize(friendName);
        }
    }
}
