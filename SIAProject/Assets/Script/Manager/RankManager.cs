using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using Proyecto26;
public class RankManager : MonoBehaviour
{
    [SerializeField] private GameObject rankLeaderData;
    [SerializeField] private GameObject rankUserData;
    [SerializeField] private Transform rankPanel;
    [SerializeField] private Transform rankPanelUser;

    private UserInfo userInfo;
    private List<UserScore> userScores;
    
    public void CreateRankLeader()
    {
        userScores = HightScores().ToList();
        Debug.Log($" UserLeght : {userScores.Count}");
        for (int i = 0; i < userScores.Count; i++)
        {
            var row = Instantiate(rankLeaderData, rankPanel).GetComponent<RankData>();
            row.rankText.text = (i + 1).ToString();
            row.nameText.text = userScores[i].username;
            row.scoreText.text = userScores[i].score.ToString();
        }
    }
    public void CreateUserRank()
    {
        var row = Instantiate(rankUserData, rankPanelUser).GetComponent<RankData>();
        row.nameText.text = userInfo.username;
        row.scoreText.text = userInfo.score.ToString();
    }
    public IEnumerable<UserScore> HightScores()
    {
        return userScores.OrderByDescending(x => x.score);
    }

    public void SetRankLeader()
    {
        userScores = FirebaseManager.Instance.userScore;
        CreateRankLeader();
        //Debug.Log(userScores.Where(user => user.username == "kasidit").Select(x => x.score).ToList()[1]);
    }
    public void SetUserRank()
    {
        userInfo = FirebaseManager.Instance.userInfo;
        CreateUserRank();
    }
    public void GetDataToCreate()
    {
        FirebaseManager.Instance.GetData();
    }
    
}
