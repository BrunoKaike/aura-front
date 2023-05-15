using UnityEngine;
using UnityEngine.UI;

public class FriendItem : MonoBehaviour
{
    public Text friendNameText;
    public Button acceptButton;
    public Button rejectButton;

    public void Initialize(string friendName)
    {
        friendNameText.text = friendName;
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
