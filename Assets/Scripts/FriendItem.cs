using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FriendItem : MonoBehaviour
{
    public TMP_Text username;
    public Button acceptButton;
    public Button rejectButton;

    public void Initialize(string friendName)
    {
        username.text = friendName;
        acceptButton.onClick.AddListener(() => AcceptFriend(friendName));
        rejectButton.onClick.AddListener(() => RejectFriend(friendName));
    }

    private void AcceptFriend(string friendName)
    {
        // TODO
    }

    private void RejectFriend(string friendName)
    {
        // TODO
    }
}
