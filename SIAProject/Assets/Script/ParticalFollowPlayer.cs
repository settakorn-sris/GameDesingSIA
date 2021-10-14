using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalFollowPlayer : MonoBehaviour
{
    [SerializeField] private Charecter follow;
    [SerializeField] float time;
    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, time);
        transform.position = new Vector3(follow.transform.position.x, 1.5f, follow.transform.position.z);
     
    }

    public void GetPlayer(Charecter player)
    {
        follow = player;
    }
}
