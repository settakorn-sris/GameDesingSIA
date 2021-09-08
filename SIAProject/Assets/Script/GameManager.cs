using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private EnemyCharecter enemy;
    [SerializeField] private GameObject boss;

    [SerializeField] private int enemyInThisRound = 5;

    [SerializeField] private int timeToBuy;

    private int Round = 1;
    private int countEnemySpawnInround;
    private float xPosition;
    private float zPosition;

    [SerializeField] private PlayerCharecter player;
    [SerializeField] private int playerHp;
    [SerializeField] private float playerSpeed;

    //[SerializeField] private PoolingOBJ bulletPooling;
    //[SerializeField] private PlayerBullet Typebullet;

    
    Vector3 playerSpawnPosition;
    //public event Action OnStart;



    private void Awake()
    {
        SpawnPlayer();
    }
    void Start()
    {
        HideMouse();
    }

    void Update()
    {
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
                enemy.Init(100, 10);
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

}
