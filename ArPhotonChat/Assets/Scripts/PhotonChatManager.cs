using UnityEngine;
using Photon.Pun;
using Photon.Chat;
using ExitGames.Client.Photon;
using UnityEngine.UI;

public class PhotonChatManager : MonoBehaviour, IChatClientListener
{
    [Header("Chat")]

    ChatClient chatClient;

    [SerializeField] string userUD;

    [SerializeField] Text chatText;
    [SerializeField] InputField textMessage;
    // [SerializeField] InputField textUserName;

    [Header("Login")]
    [SerializeField] InputField userName;
    [SerializeField] GameObject panelChat;
    [SerializeField] GameObject panelLogin;



    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log($"{level},{ message}");
    }

    public void OnChatStateChange(ChatState state)
    {
        Debug.Log(state);
    }

    public void OnConnected()
    {
        //chatText.text += "\n Вы подключились к чату";
        chatClient.Subscribe(" Ar Chat");
    }

    public void OnDisconnected()
    {
        chatClient.Unsubscribe(new string[] { "globalChat" });
    }

    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        for (int i = 0; i < senders.Length; i++)
        {
            chatText.text += $"\n {senders[i]}: {messages[i]}";
            //[{ channelName}]
        }
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        throw new System.NotImplementedException();
    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            chatText.text += channels[i];
            // $"Вы подключены {channels[i]}";
        }
    }

    public void OnUnsubscribed(string[] channels)
    {
        for (int i = 0; i < channels.Length; i++)
        {
            chatText.text += channels[i];
            // $"Вы отключены {channels[i]}";
        }
    }

    public void OnUserSubscribed(string channel, string user)
    {
        chatText.text += $"Пользователь {user} подключился к {channel}";
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        chatText.text += $"Пользователь {user} отключился от {channel}";
    }

    void Start()
    {
        chatClient = new ChatClient(this);
        //1
        
       
    }


    void Update()
    {
        chatClient.Service();
    }

    public void SendButton()
    {
        //2
        //if(textUserName.text == "")
        {
            chatClient.PublishMessage(" Ar Chat", textMessage.text);
        }
    }

    public void LoginButton()
    {
        panelChat.SetActive(true);
        panelLogin.SetActive(false);
        userUD = userName.text;
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(userUD));
    }
}
