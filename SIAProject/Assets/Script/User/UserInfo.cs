using UnityEngine;
using System;
using UnityEngine.UI;
[Serializable]
public class UserInfo 
{
    public string email;
    public string username;
    public int score;
    public int round;
    public string localId;

    public UserInfo(string email,string username, int score , int round)
    {
        this.email = email;
        this.username = username;
        this.score = score;
        this.round = round;
        localId = FirebaseManager.localId;
    }

}
