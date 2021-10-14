using UnityEngine;
using System;
using UnityEngine.UI;
[Serializable]
public class UserInfo 
{
    public int rank;
    public string username;
    public int score;
    public string localId;
    public UserInfo(int rank, string username, int score)
    {
        this.rank = rank;
        this.username = username;
        this.score = score;
        localId = FirebaseManager.localId;
    }
    
}
