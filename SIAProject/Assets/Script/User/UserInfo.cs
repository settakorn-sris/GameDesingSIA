using UnityEngine;
using System;
using UnityEngine.UI;
[Serializable]
public class UserInfo 
{
    public string username;
    public int score;
    public string localId;

    public UserInfo(string username, int score)
    {
        this.username = username;
        this.score = score;
        localId = FirebaseManager.localId;
    }

}
