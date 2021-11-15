using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaleeBoss : Boss
{
    float numForChangeState;
    float normalSpeed;

    protected override void Awake()
    {
        base.Awake();
        normalSpeed = Speed;
      
    }
    public override void LowHp()
    {
        base.LowHp();
       
    }
    public override void NormalState()
    {
        ///BuG
        //if(numForChangeState== -15) Speed = normalSpeed;

        //numForChangeState += Time.deltaTime;
        //if (numForChangeState <= 15) return;
        //Speed = 20;
        //numForChangeState = 0;
        //Debug.Log("AAA");

        Speed = 20;
        base.NormalState();
    }
    public override void StateTwo()
    {
        Speed = normalSpeed;
        base.StateTwo();
        
    }

}
