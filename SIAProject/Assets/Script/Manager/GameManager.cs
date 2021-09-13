using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum Wave{
    ENEMY,
    BOSS,
    BUY,

}
public class GameManager : Singleton<GameManager>
{
    [SerializeField] private RawImage Panel;
    [SerializeField] private Button button;

    [SerializeField] private EnemyCharecter enemy;
    [SerializeField] private EnemyCharecter boss;

    [SerializeField] private int enemyInThisRound = 5;

    //Wave
    private Wave wave = Wave.ENEMY;

    [SerializeField] private int timeToBuy;

    [SerializeField] private ScoreManager scoreManager;

    private int Round = 1;
    private int countEnemySpawnInround = 0;
    private float xPosition;
    private float zPosition;

    [SerializeField] private PlayerCharecter player;
    [SerializeField] private int playerHp;
    [SerializeField] private float playerSpeed;

    
    [SerializeField] private int enemyHp;
    [SerializeField] private float enemySpeed;


    //public event Action OnReStart;



    private void Awake()
    {
        //OnReStart += RestartGame;
    }
    void Start()
    {
        //button.onClick.AddListener(StartGame);
        StartGame();
    }

    void Update()
    {
        GameLoop();   
    }

    public void StartGame()
    {
        HideMouse();
        scoreManager.Rescore();
        SpawnPlayer();
    }
    private void HideMouse()
    {
        Cursor.visible = false;
    }

  
    private void GameLoop()
    {
        
        var enemyCheck = GameObject.FindGameObjectsWithTag("Enemy");
        var bossCheck = GameObject.FindGameObjectsWithTag("Boss");

        if (wave==Wave.ENEMY && bossCheck.Length == 0 )
        {
            SpawnEnemy();
            wave = Wave.BOSS;

        }
        else if(wave==Wave.BOSS && bossCheck.Length==0 && enemyCheck.Length == 0)
        {
            SpawnBoss();
            Debug.Log("Boss round");
        }
        else if(wave==Wave.BUY && bossCheck.Length==0)
        {
            //UpgradeItem();
            Debug.Log("BUY");
        }
    }

    private void SpawnPlayer()
    {
        Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);
        player.Init(playerHp, playerSpeed);
        //OnReStart += player.IsDie;
    }

    private void SpawnEnemy()
    {
        float timeCount = 0;
        while (countEnemySpawnInround <= enemyInThisRound)
        {
            xPosition = UnityEngine.Random.Range(-12, 13);
            zPosition = UnityEngine.Random.Range(-11, 13);

            timeCount = UnityEngine.Random.Range(1, 5);
            timeCount -= Time.deltaTime;
           
            if (timeCount > 1)
            {
                Instantiate(enemy, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
                Debug.Log("Enemy round");
                enemy.Init(enemyHp, enemySpeed);
                timeCount = 0;

            }
            countEnemySpawnInround++;
        }
        
    }

    private void SpawnBoss()
    {
        var bossCheck = GameObject.FindGameObjectsWithTag("Boss");
        if(bossCheck.Length ==0)
        {
            Instantiate(boss, new Vector3(-8, 0, 13), Quaternion.identity);
            boss.Init(1000, 1);
        }
        //UpgradeItem() //if Boss Is Die
        wave = Wave.BUY;
    }

    private void UpgradeItem()
    {
        //Buy Panel SetActive(true)
        float timeCount = 0;

        timeCount += Time.deltaTime;
        if (timeCount == timeToBuy)
        {
            //Buy Panel SetActive(false)
            wave = Wave.ENEMY; 
        }
 
    }
    private void GameReset()
    {
        SceneManager.LoadScene("Game");
    }

    //For Mockup
    //private void RestartGame()
    //{
    //    var enemyCheck = GameObject.FindGameObjectsWithTag("Enemy");
    //    Panel.gameObject.SetActive(true);
    //    button.gameObject.SetActive(true);
    //    Cursor.visible = true;
    //    foreach (var enemy in enemyCheck)
    //    {
    //        Destroy(enemy);
    //    }
    //}

    //IEnumerator wave()
    //{
    //    waveEnd = false;
    //    for(var i=0;i<enemyInThisRound;i++)
    //    {
    //        SpawnEnemy();
    //        yield return new WaitForSeconds(spawnRate);
    //    }

    //    SpawnBoss();

    //    enemyHp += 10;

    //    yield return new WaitForSeconds(timeBetweenWave);
    //    waveEnd = true;
    //}

}
