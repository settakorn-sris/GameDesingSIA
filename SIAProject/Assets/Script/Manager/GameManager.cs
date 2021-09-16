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
    [Header("Game Setting")]
    [SerializeField] private RawImage Panel;
    [SerializeField] private Button button;

    [SerializeField] private EnemyCharecter enemy;
    [SerializeField] private EnemyCharecter boss;

    [SerializeField] private int enemyInThisRound = 5;

    //Wave
    private Wave wave = Wave.ENEMY;
    //BuY
    [Header("Buy")]
    [SerializeField] private GameObject buyPanel;
    [SerializeField] private float timeToBuy;

    [SerializeField] private ScoreManager scoreManager;
    private float timeCount;
    private int Round = 1;
    private int countEnemySpawnInround = 0;
    private float xPosition;
    private float zPosition;

    [Header("Player")]
    [SerializeField] private PlayerCharecter player;
    [SerializeField] private int playerHp;
    [SerializeField] private float playerSpeed;
    public float FireRate;

    [Header("Enemy")]
    [SerializeField] private int enemyHp;
    [SerializeField] private float enemySpeed;
    [SerializeField] private int enemyDamage;

    [Header("Boss")]
    [SerializeField] private int bossHp;
    [SerializeField] private float bossSpeed;
    [SerializeField] private int bossDamage;
    //public event Action OnReStart;

    private void Awake()
    {
        //OnReStart += RestartGame;
        scoreManager = ScoreManager.Instance;
        StartGame();
    }
   
    void Update()
    {
        GameLoop();
       
    }

    public void StartGame()
    {
        scoreManager.Rescore();
        SpawnPlayer();
        wave = Wave.ENEMY;
    }
  
    private void GameLoop()
    {
        print(wave + "!!");
        var enemyCheck = GameObject.FindGameObjectsWithTag("Enemy");
        var bossCheck = GameObject.FindGameObjectsWithTag("Boss");

        if (wave==Wave.ENEMY)
        {
            SpawnEnemy();
            

        }
        else if(wave==Wave.BOSS && bossCheck.Length==0 && enemyCheck.Length == 0)
        {
            SpawnBoss();
            Debug.Log("Boss round");
        }
        else if(wave==Wave.BUY && bossCheck.Length==0)
        {
            UpgradeItem();
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
        while (countEnemySpawnInround < enemyInThisRound)
        {
            xPosition = UnityEngine.Random.Range(-12, 13);
            zPosition = UnityEngine.Random.Range(-11, 13);

            timeCount = UnityEngine.Random.Range(1, 5);
            timeCount -= Time.deltaTime;
           
            if (timeCount > 1)
            {
                Debug.Log("Enemy round");

                enemy.Init(enemyHp, enemySpeed,enemyDamage);
                Instantiate(enemy, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
                
                timeCount = 0;

            }
            countEnemySpawnInround++;
        }
        wave = Wave.BOSS;
        AddEnemyInAnotherRound();
        countEnemySpawnInround = 0;
        
    }
    private void AddEnemyInAnotherRound()
    {
        var increaseEnemy = UnityEngine.Random.Range(1, 2);
        enemyInThisRound += increaseEnemy;
    }

    private void SpawnBoss()
    {
        var bossCheck = GameObject.FindGameObjectsWithTag("Boss");
        if(bossCheck.Length ==0)
        {
            boss.Init(bossHp,bossSpeed,bossDamage);
            Instantiate(boss, new Vector3(-8, 0, 13), Quaternion.identity);
        }
        //UpgradeItem() //if Boss Is Die
        wave = Wave.BUY;
        timeCount = timeToBuy;
        Round++;
    }
    private void AddHPAndDamage(int hp,int damage,float speed)
    {
        var increaseHp = UnityEngine.Random.Range(10,20);
        var increaseDamage = UnityEngine.Random.Range(2, 4);
        var increaseSpeed = UnityEngine.Random.Range(0, 2);
        hp += increaseHp;
        damage += increaseDamage;
        speed += increaseSpeed;

    }
    private void UpgradeItem()
    {
        //Buy Panel SetActive(true)
       
        timeCount -= Time.deltaTime;
        buyPanel.gameObject.SetActive(true);
        print(timeCount);
        if (timeCount > 0) return;
        //Buy Panel SetActive(false)
        buyPanel.gameObject.SetActive(false);
        
        wave = Wave.ENEMY;          
        
 
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
