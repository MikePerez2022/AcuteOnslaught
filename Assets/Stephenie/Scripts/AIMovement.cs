using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour 
{
    [SerializeField] public ShootingManager Sm;
    public Weapon w;
    public GameObject player;
    public float speed;
    public float maxSpeed;

    private float distance;
    private float time;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        maxSpeed = speed;
    }
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // angle to turn towards player
    
        //Update position and rotation to move to and look at player
        transform.position = Vector2.MoveTowards(this.transform.position,player.transform.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        
        if (distance <= 13)
        {
            Sm.Shoot(true);
            speed = 0;
            Console.WriteLine("shooting bullets");
        }
        else
        {
            speed = maxSpeed;
            Sm.Shoot(false);
            Console.WriteLine("moving to player");
        }
    }
        
    
}
