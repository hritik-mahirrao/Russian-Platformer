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
    private Animator enemyAnimatorController;


    private bool m_FacingRight = true;  // For determining which way the enemy is currently facing.


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
        enemyAnimatorController = GetComponent<Animator>();
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

            enemyRigidBody2D.velocity = new Vector2(moveSpeed * maxSpeed, enemyRigidBody2D.velocity.y);

            //transform.position = Vector2.MoveTowards(transform.position, new Vector2(Patrol_Points[currentWayPoint].transform.position.x, transform.position.y), Time.deltaTime * moveSpeed);
            enemyAnimatorController.SetFloat("Speed", Mathf.Abs(moveSpeed));

            //for (int i = 0; i < Patrol_Points.Length - 1; i++)
            //{
            //    if (Vector2.Distance(transform.position, Patrol_Points[i].transform.position) < 2)
            //    {

            //    }
            //}

            // If the input is moving the enemy right and the player is facing left...
            if (moveSpeed > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
            // Otherwise if the input is moving the enemy left and the enemy is facing right...
            else if (moveSpeed < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }

        }
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, lookRadiusOuter);
        Gizmos.DrawWireSphere(transform.position, lookRadiusInner);

    }
}
