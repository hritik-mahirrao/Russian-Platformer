using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform Player;
    public Transform[] Patrol_Points;

    public float maxSpeed;
    public float moveSpeed;
    int currentWayPoint = 0;

    public float lookRadiusOuter, lookRadiusInner;


    private Rigidbody2D enemyRigidBody2D;

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

    public Enemy_States enemy_state;

    // Use this for initialization
    void Start()
    {
        Player = GameObject.Find("CharacterRobotBoy").GetComponent<Transform>();
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Enemy_State_Management();
        Enemy_Movement();
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

            if (Vector2.Distance(transform.position, Patrol_Points[currentWayPoint].transform.position) < 2)
            {
                currentWayPoint++;
            }

            if (currentWayPoint >= Patrol_Points.Length - 1)
            {
                currentWayPoint = 0;
            }

            transform.position = Vector2.MoveTowards(transform.position, Patrol_Points[currentWayPoint].transform.position, 0.3f);


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
