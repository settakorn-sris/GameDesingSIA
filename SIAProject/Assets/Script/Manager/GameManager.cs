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
    [SerializeField] private RawImage gamePanel;
    [SerializeField] private Button puseButton;
    [SerializeField] private Button resumeButton;

    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private EnemyCharecter[] enemy;
    [SerializeField] private EnemyCharecter boss;

    [SerializeField] private int enemyInThisRound = 5;
    private float timeForEnemySpawn;
    [SerializeField] private GameObject[] map;


    //BuY
    [Header("Buy")]
    [SerializeField] private GameObject buyPanel;
    [SerializeField] private float timeToBuy;
    [SerializeField] private Button buyHealingButton;
    [SerializeField] private TextMeshProUGUI healthPrice;
    [SerializeField] private ScoreManager scoreManager;

    //[SerializeField] private int skillPrice = 3;
    private int randomSkillIndex = 0;
    [SerializeField] private int healingPrice = 3;

    private float timeCount;
    private int round = 1;
    private int countEnemySpawnInround = 0;
    private float xPosition;
    private float zPosition;
    private int indexForRandomEnemy;
   

    [Header("Player")]
    [SerializeField] private PlayerCharecter player;
    [SerializeField] private int playerHp;
    [SerializeField] private float playerSpeed;
    [SerializeField] private Skill[] playerSkill; 
    private PlayerCharecter playerInScene;
    
    [Header("Bullet")]
    public float FireRate;
    public int BulletDamage;
    public int BulletSpeed;

    [Header("Enemy Base")]
    [SerializeField] private int enemyHp;
    [SerializeField] private float enemySpeed;
    [SerializeField] private int enemyDamage;

    #region For get Base Enemy 

    public EnemyCharecter[] Enemy
    {
        get
        {
            return enemy;
        }
    }

    public int Hp
    {
        get
        {
            return enemyHp;
        }
    }

    public float Speed
    {
        get
        {
            return enemySpeed;
        }
    }

    public int Damage
    {
        get
        {
            return enemyDamage;
        }
    }
    #endregion

    [Header("Enemy bomb")]
    [SerializeField] private int bombDamage;

    #region For get Enemy bomb property

    public int GetBombDamage
    {
        get
        {
            return bombDamage;
        }
    }

    #endregion

    [Header("Boss")]
    [SerializeField] private int bossHp;
    [SerializeField] private float bossSpeed;
    [SerializeField] private int bossDamage;
    public int HpForBossHealing;
    public int minianAmount;
   // public EnemyCharecter MinianOfBoss;

    [Header("For Skill")]
    public GameObject CheckSkillCollision;  
    //Wave
    private Wave wave;
   

    public delegate void SlowSkillActive(float speed);
    public event SlowSkillActive OnSlow;
    
    //public event Action OnReStart;

    private void Awake()
    {
        puseButton.onClick.AddListener(StopGame);
        resumeButton.onClick.AddListener(ResumeGame);
        buyHealingButton.onClick.AddListener(BuyHealing);
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
        RoundReset();
        SpawnPlayer();
        wave = Wave.ENEMY;
        timeForEnemySpawn = 1;
        healthPrice.text = $": {healingPrice}";
    }
  
    private void GameLoop()
    {
        var enemyCheck = GameObject.FindGameObjectsWithTag("Enemy");
        var bossCheck = GameObject.FindGameObjectsWithTag("Boss");

        if (wave==Wave.ENEMY)
        {
            SpawnEnemy();
        }
        else if(wave==Wave.BOSS && bossCheck.Length==0 && enemyCheck.Length == 0)
        {
            SpawnBoss();
        }
        else if(wave==Wave.BUY && bossCheck.Length==0 && enemyCheck.Length == 0) //wave==Wave.BUY && bossCheck.Length==0
        {
            UpgradeItem();
        }
    }

    private void SpawnPlayer()
    {
        playerInScene= Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);
        playerInScene.Init(playerHp, playerSpeed,playerSkill[0]);
        //OnReStart += player.IsDie;
    }

    private void SpawnEnemy()
    {

        while (countEnemySpawnInround < enemyInThisRound)
        {
            xPosition = UnityEngine.Random.Range(-12, 13);
            zPosition = UnityEngine.Random.Range(-11, 13);
            indexForRandomEnemy = UnityEngine.Random.Range(0, enemy.Length);
            print(indexForRandomEnemy);
            //print(timeForEnemySpawn);
            timeForEnemySpawn -= Time.deltaTime;
            //print(timeForEnemySpawn);

            if (timeForEnemySpawn >= 0) return;
            
            Debug.Log("Enemy round");
            
            enemy[indexForRandomEnemy].Init(enemyHp, enemySpeed, enemyDamage);
            Instantiate(enemy[indexForRandomEnemy], new Vector3(xPosition, 0, zPosition), Quaternion.identity);
            
            timeForEnemySpawn = UnityEngine.Random.Range(1,3);
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
        randomSkillIndex = UnityEngine.Random.Range(0, playerSkill.Length); // For random skill
        
        timeCount -= Time.deltaTime;
        
        buyPanel.gameObject.SetActive(true);
        
        print(timeCount);
        if (timeCount > 0) return;
        //Buy Panel SetActive(false)
        buyPanel.gameObject.SetActive(false);

        timeForEnemySpawn = 1;
        wave = Wave.ENEMY;
        RoundSetting(1);
    }
    private void RoundSetting(int round)
    {
        this.round += round;
        roundText.text = $"ROUND:{this.round}";
    }
    private void RoundReset()
    {
        round = 1;
        roundText.text = $"ROUND:{this.round}";
    }
    private void StopGame()
    {
        gamePanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }
    private void ResumeGame()
    {
        gamePanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    //Buy Function
    private void BuyHealing()
    {
        if (scoreManager.Score < healingPrice) return;
        scoreManager.MinusScore(healingPrice);
        playerInScene.Healing(playerHp);
        buyPanel.SetActive(false);
        timeCount = 0;

    }

    private void BuySkill()
    {
        if (scoreManager.Score < playerSkill[randomSkillIndex].SkillPrice) return;
        scoreManager.MinusScore(playerSkill[randomSkillIndex].SkillPrice);
        player.Skill = playerSkill[randomSkillIndex];
        // buyPanel.SetActive(false);
    }

    #region "For Skill"

    public void TimeSlow(float speed)
    {
        OnSlow(speed);
    }


    #endregion



    private void GameReset()
    {
        SceneManager.LoadScene("Game");
    }

    private void RandomMap()
    {
        //must have setActive false of old map before use this method
        int randomMap = UnityEngine.Random.Range(0,map.Length);
        map[randomMap].gameObject.SetActive(true);
        map[randomMap].transform.position = new Vector3(0, 0, 0);

    }

    public float ReturnEnemySpeed(float speed)
    {
        return speed = enemySpeed;
    }
    public float GetEnemySpeed
    {
        get
        {
            return enemySpeed;
        }
    }
    public int GetBossHp
    {
        get
        {
            return bossHp;
        }
    }

    //private void CreatMap()
    //{
    //    int i = 0;
    //    while(i<=map.Length)
    //    {

    //    }
    //}

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
