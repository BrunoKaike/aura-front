using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections;

public class FriendItem : MonoBehaviour
{
    public TMP_Text username;

    public void Initialize(string friendName)
    {
        username.text = friendName;
    }

}
