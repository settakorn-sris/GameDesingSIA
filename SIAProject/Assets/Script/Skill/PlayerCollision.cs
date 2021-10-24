using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private Stunt stunt;
    private float getOldSpeed = 0;

    private void Update()
    {

        transform.position += new Vector3(0, -1, 0) * Time.deltaTime;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag== "Enemy" || other.gameObject.tag == "Boss")
        {
            
            var enemy = other.gameObject.GetComponent<EnemyCharecter>();
            getOldSpeed = enemy.Speed;
            enemy.OnStunt();

         

        }
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log(getOldSpeed);
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            var enemy = other.gameObject.GetComponent<EnemyCharecter>();
            enemy.Speed = getOldSpeed;
            enemy.ExitStunt(getOldSpeed);

        }
    }




}
