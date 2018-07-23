using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    // Game Object
    public GameObject Hero;

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
    private Animator enemyAnimator;
    private bool isAlert = false;
    //public bool m_FacingRight;


    public event EventHandler OnEnemyAttack;


    // Use this for initialization
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        updatePlayer();
    }

    void updatePlayer()
    {

        float distanceFromPlayer = Vector2.Distance(transform.position, Hero.transform.position);
        if (!isAlert && distanceFromPlayer < alertDistance)
        {
            // Alert the enemy
            isAlert = true;
            enemyAnimator.SetBool("Idle", true);
            enemyAnimator.SetBool("Walk", false);
            enemyAnimator.SetBool("Run", false);
            enemyAnimator.SetBool("Trans", false);

        }
        else if (distanceFromPlayer <= meleDistance)
        {
            // Trans position
            // Mele combact
            enemyAnimator.SetBool("Idle", false);
            enemyAnimator.SetBool("Trans", true);
            enemyAnimator.SetBool("Walk", false);
            enemyAnimator.SetBool("Run", false);

            if (OnEnemyAttack != null)
            {
                OnEnemyAttack(this, EventArgs.Empty);
            }

        }
        else if (distanceFromPlayer <= walkDistance)
        {
            // Start walking towards Hero
            transform.position = Vector2.MoveTowards(transform.position, Hero.transform.position, walkSpeed);

            enemyAnimator.SetBool("Idle", false);
            enemyAnimator.SetBool("Trans", false);
            enemyAnimator.SetBool("Walk", true);
            enemyAnimator.SetBool("Run", false);
        }
        else if (isAlert)
        {
            // Run towards Hero
            transform.position = Vector2.MoveTowards(transform.position, Hero.transform.position, runSpeed);

            enemyAnimator.SetBool("Idle", false);
            enemyAnimator.SetBool("Trans", false);
            enemyAnimator.SetBool("Walk", false);
            enemyAnimator.SetBool("Run", true);
        }

        float dir = transform.position.x - Hero.transform.position.x;

        if (dir < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (dir > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        //else
        //{
        //    enemyAnimator.SetBool("Walk", false);
        //    enemyAnimator.SetBool("Run", false);
        //}

    }

    void Enemy_State_Management()
    {
        //if (Vector2.Distance(transform.position, Player.transform.position) <= lookRadiusOuter)
        //{
        //    enemy_state = Enemy_States.ChasePlayer;
        //}

        //if (Vector2.Distance(transform.position, Player.transform.position) <= lookRadiusInner)
        //{
        //    enemy_state = Enemy_States.Attack;
        //}

        //else
        //{
        //    enemy_state = Enemy_States.Patrol;
        //}

    }

    void Enemy_Movement()
    {
        //if (enemy_state == Enemy_States.Patrol)
        //{

        //    // if (Vector2.Distance(transform.position, Patrol_Points[currentWayPoint].transform.position) < 2)
        //    // {
        //    //     currentWayPoint++;
        //    // }

        //    // if (currentWayPoint >= Patrol_Points.Length - 1)
        //    // {
        //    //     currentWayPoint = 0;
        //    // }

        //    // transform.position = Vector2.MoveTowards(transform.position, Patrol_Points[currentWayPoint].transform.position, 0.3f);

        //    //transform.position = Vector2.MoveTowards(transform.position, new Vector2(Patrol_Points[currentWayPoint].transform.position.x, transform.position.y), Time.deltaTime * moveSpeed);
        //    enemyAnimatorController.SetFloat("Speed", Mathf.Abs(moveSpeed));

        //    //for (int i = 0; i < Patrol_Points.Length - 1; i++)
        //    //{
        //    //    if (Vector2.Distance(transform.position, Patrol_Points[i].transform.position) < 2)
        //    //    {

        //    //    }
        //    //}

        // If the input is moving the enemy right and the player is facing left...
        //    if (walkSpeed > 0 && !m_FacingRight)
        //    {
        //        // ... flip the player.
        //        Flip();
        //    }
        //    // Otherwise if the input is moving the enemy left and the enemy is facing right...
        //    else if (walkSpeed < 0 && m_FacingRight)
        //    {
        //        // ... flip the player.
        //        Flip();
        //    }

        //}

        //private void Flip()
        //{
        //    // Switch the way the player is labelled as facing.
        //    m_FacingRight = !m_FacingRight;

        //    // Multiply the player's x local scale by -1.
        //    Vector3 theScale = transform.localScale;
        //    theScale.x *= -1;
        //    transform.localScale = theScale;
        //}







        //void OnDrawGizmos()
        //{
        //    Gizmos.DrawWireSphere(transform.position, lookRadiusOuter);
        //    Gizmos.DrawWireSphere(transform.position, lookRadiusInner);

        //}
    }
}
