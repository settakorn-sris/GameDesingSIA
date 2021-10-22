using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Proyecto26;
public class RankManager : MonoBehaviour
{
    public GameObject rankData;
    public Transform rankPanel;

    public List<UserScore> userScores;
    
    public void CreateRankData()
    {
        userScores = HightScores().ToList();
        Debug.Log($" UserLeght : {userScores.Count}");
        for (int i = 0; i < userScores.Count; i++)
        {
            var row = Instantiate(rankData, rankPanel).GetComponent<RankData>();
            row.rankText.text = (i + 1).ToString();
            row.nameText.text = userScores[i].username;
            row.scoreText.text = userScores[i].score.ToString();
        }
    }
    public IEnumerable<UserScore> HightScores()
    {
        return userScores.OrderByDescending(x => x.score);
    }

    public void SetRank()
    {
        userScores = FirebaseManager.Instance.userScore;
        CreateRankData();
        //Debug.Log(userScores.Where(user => user.username == "kasidit").Select(x => x.score).ToList()[1]);
    }
    public void GetDataToCreate()
    {
        FirebaseManager.Instance.GetData();
    }
}
