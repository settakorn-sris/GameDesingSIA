using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSpawnEnemy : MonoBehaviour
{
    [SerializeField] private int countSpawn = 30;

    private GameManager gm;

    private EnemyCharecter[] enemy;

    private Queue<EnemyCharecter> enemyQueue = new Queue<EnemyCharecter>();

    private int typeEnemyCount; 

    private void Awake()
    {
        gm = GameManager.Instance;

        getEnemytype(gm.Enemy);

        typeEnemyCount = enemy.Length;

        InstanceEnemy(enemy, countSpawn);

    }

    //Get type
    private void getEnemytype(EnemyCharecter[] getEnemy)
    {
        enemy = getEnemy;
    }


    //Create Enemy
    private void InstanceEnemy(EnemyCharecter[] enemyToSpawn,int enemyCountSpawn)
    {
        for(var i=0;i>=enemyCountSpawn;i--)
        {
            var enemy = Instantiate(enemyToSpawn[typeEnemyCount], transform.position, Quaternion.identity);
            enemyQueue.Enqueue(enemy);
            enemy.gameObject.SetActive(false);

            typeEnemyCount--;

            if(typeEnemyCount < 0)
            {
                typeEnemyCount = 0;
            }
        }

    }

   
    //return Enemy
    private void SentEnemy(float x,float z,int hp,int damage,int scoreInRound,float speed)
    {
        if(enemyQueue.Count>0)
        {
            var enemy = enemyQueue.Dequeue();
            enemy.Init(hp, speed, damage,scoreInRound);
            enemy.transform.position = new Vector3(x, 0, z);
            enemy.gameObject.SetActive(true);
        }
        else
        {
            int i = Random.Range(0, typeEnemyCount);
            var enemySurplus = Instantiate(enemy[0], new Vector3(x, 0, z), Quaternion.identity);
            enemySurplus.Init(hp, speed, damage,scoreInRound);

        }


    }

    //Get in pool
    private void GetEnemyToPool(EnemyCharecter enemy)
    {
        enemyQueue.Enqueue(enemy);
        enemy.gameObject.SetActive(false);
    }
}
