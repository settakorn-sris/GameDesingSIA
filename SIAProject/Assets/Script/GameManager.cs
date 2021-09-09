using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RawImage Panel;
    [SerializeField] private Button button;

    [SerializeField] private EnemyCharecter enemy;
    [SerializeField] private GameObject boss;

    [SerializeField] private int enemyInThisRound = 5;

    [SerializeField] private int timeToBuy;

    [SerializeField] private ScoreManager scoreManager;

    private int Round = 1;
    private int countEnemySpawnInround;
    private float xPosition;
    private float zPosition;

    [SerializeField] private PlayerCharecter player;
    [SerializeField] private int playerHp;
    [SerializeField] private float playerSpeed;

    
    [SerializeField] private int enemyHp;
    [SerializeField] private float enemySpeed;


    //[SerializeField] private PoolingOBJ bulletPooling;
    //[SerializeField] private PlayerBullet Typebullet;

    
    Vector3 playerSpawnPosition;
    //public event Action OnStart;



    private void Awake()
    {
       
    }
    void Start()
    {
        button.onClick.AddListener(StartGame);
    }

    void Update()
    {
       
        //CheckEnemyAndPlayerInScene();
        
    }

    private void StartGame()
    {
        HideMouse();
        Panel.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        SpawnPlayer();
        SpawnEnemy();
    }
    private void HideMouse()
    {
        Cursor.visible = false;
    }

    private void SpawnPlayer()
    {
        Instantiate(player, new Vector3(0,1,0), Quaternion.identity);
        player.Init(playerHp,playerSpeed);
    }


    private void SpawnEnemy()
    {
        float timeCount = 0;
       while(countEnemySpawnInround < enemyInThisRound)
        {
            xPosition = UnityEngine.Random.Range(-12, 13);
            zPosition = UnityEngine.Random.Range(-11, 13);

            timeCount += Time.deltaTime;

            if (timeCount > 1)
            {
                Instantiate(enemy, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
                enemy.Init(enemyHp, enemySpeed);
                timeCount = 0;
                countEnemySpawnInround++;
            }   
          
        }

       //if all enemy is die 
       // SpawnBoss();

    }

    private void SpawnBoss()
    {
        enemyInThisRound += 2;

        //UpgradeItem() //if Boss Is Die
    }

    private void UpgradeItem()
    {
        float timeCount = 0;

        timeCount += Time.deltaTime;
        if (timeCount < timeToBuy)
        {

            //SpawnEnemy();
        }

    }

    private void BossIsDie()
    {
        Round++;
    }

    private void CheckEnemyAndPlayerInScene()
    {
        var enemyCheck = GameObject.FindGameObjectsWithTag("Enemy");
        var playerCheck = GameObject.FindGameObjectsWithTag("Player");
        
      
        if (enemyCheck.Length == 0 || playerCheck.Length == 0)
        {
            Panel.gameObject.SetActive(true);
            button.gameObject.SetActive(true);
            Cursor.visible = true;
            foreach (var enemy in enemyCheck)
            {
                Destroy(enemy);
            }
            foreach (var player in playerCheck)
            {
                Destroy(player);
            }

        }
        
    }
   
}
