using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class UserScore 
{
    public string username;
    public int score;
    public int round;
    public UserScore(string username, int score, int round)
    {
        this.username = username;
        this.score = score;
        this.round = round;
    }
}
