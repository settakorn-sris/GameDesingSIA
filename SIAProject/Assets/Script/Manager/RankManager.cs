using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using TMPro;
public class RankManager : MonoBehaviour
{
    [Header("Rank LeaderBoard")]
    [SerializeField] private GameObject rankLeaderData;
    [SerializeField] private GameObject rankUserData;
    [SerializeField] private Transform rankPanel;
    [SerializeField] private Transform rankPanelUser;

    [Space]
    [Header("OBJ LeaderBoard")]
    [SerializeField] private GameObject leaderBoard;

    private UserInfo userInfo;
    private List<UserScore> userScores;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
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
    public void OpenLeaderBoard()
    {
        Instantiate(leaderBoard);
        //leaderBoard.SetActive(true);
    }
    
}
