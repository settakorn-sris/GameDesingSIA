using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharecter : Charecter
{
    public Skill Skill;
    private float timeCountSkill = 0;
    [SerializeField] private PlayerBullet bulletType;
    [SerializeField] private PoolingOBJ bullet;
    //private Animator animator;
    private bool CanUseSkill;
    public bool CanGetDamage = true;
    public GameObject CheckCollisionForSkill;
   
    public GameManager GM;
    //public void GetBullet(PoolingOBJ bullet)
    //{
    //    this.bullet = bullet;
    //}
    public void Init(int hp, float speed, Skill skill)
    {
        base.Init(hp, speed);
        Skill = skill;
    }
    private void Awake()    
    {
        GM = GameManager.Instance;
        bullet.GetBulletType(bulletType);
        //animator = GetComponent<Animator>();
        CanUseSkill = true;
    }

    protected override void Update()
    {
        base.Update();

        CoolDown();

        if (Input.GetKeyDown(KeyCode.R))
        {
            UseSkill();
        }
    }
    public void Attack()
    {
        bullet.GetPool();
        //animator.SetTrigger("ATK");
        //Instantiate(bulletType, bullet.transform.position, Quaternion.identity);
    }
    public void ChangeBullet()
    {
        //For Change Bullet in pool
    }

    public void Healing(int hp)
    {
        Hp = hp;
        print(Hp);
    }

    private void UseSkill()
    {
        if (!CanUseSkill) return;
        
        Skill.AboutSkill(this);
        print("Skill");
        CanUseSkill = false;
        timeCountSkill = Skill.CoolDownSkill;
        
    }

    private void CoolDown()
    {
        if (!CanUseSkill)
        {
            timeCountSkill -= Time.deltaTime;
            if (timeCountSkill <= 0)
            {
                print("Completed");
                CanUseSkill = true;
            }
        } 
        
    }
    public override void TakeDamage(int damage)
    {
        if (!CanGetDamage) return;
        base.TakeDamage(damage);
    }

    public override void IsDie()
    {

        Debug.Log("Die");
        //animator.SetTrigger("Dying");
    }

}
