using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Game Object
    public Transform Hero;

    // State change distances
    public float alertDistance;
    public float walkDistance;
    public float meleDistance;

    public float runSpeed;
    public float walkSpeed;

    // States
    public Enemy_States enemy_state;
    public enum Enemy_States
    {
        //Idle,
        //Walking,
        //Running,
        Patrol,
        ChasePlayer,
        Attack,
        Fallen
    };

    private Rigidbody2D enemyRigidBody2D;
    private bool isAlert = false;
    
    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("CharacterRobotBoy").GetComponent<Transform>();
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        updatePlayer();
    }

    void updatePlayer() {

        Vector2 distanceFromPlayer = Vector2.Distance(transform.position, Player.transform.position);
        if (!isAlert && distanceFromPlayer.x < alertDistance) {
            // Alert the enemy
            isAlert = true;
        } else if (distanceFromPlayer.x <= meleDistance) {
            // Trans position
            // Mele combact
        } else if (distanceFromPlayer.x <= walkDistance) {
            // Start walking towards Hero
        } else {
            // Run towards Hero
        }
    }

    void Enemy_State_Management()
    {
        if (Vector2.Distance(transform.position, Player.transform.position) <= lookRadiusOuter)
        {
            enemy_state = Enemy_States.ChasePlayer;
        }

        if (Vector2.Distance(transform.position, Player.transform.position) <= lookRadiusInner)
        {
            enemy_state = Enemy_States.Attack;
        }

        else
        {
            enemy_state = Enemy_States.Patrol;
        }

    }

    void Enemy_Movement()
    {
        if (enemy_state == Enemy_States.Patrol)
        {

            // if (Vector2.Distance(transform.position, Patrol_Points[currentWayPoint].transform.position) < 2)
            // {
            //     currentWayPoint++;
            // }

            // if (currentWayPoint >= Patrol_Points.Length - 1)
            // {
            //     currentWayPoint = 0;
            // }

            // transform.position = Vector2.MoveTowards(transform.position, Patrol_Points[currentWayPoint].transform.position, 0.3f);


            //for (int i = 0; i < Patrol_Points.Length - 1; i++)
            //{
            //    if (Vector2.Distance(transform.position, Patrol_Points[i].transform.position) < 2)
            //    {

            //    }
            //}

        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, lookRadiusOuter);
        Gizmos.DrawWireSphere(transform.position, lookRadiusInner);

    }
}
