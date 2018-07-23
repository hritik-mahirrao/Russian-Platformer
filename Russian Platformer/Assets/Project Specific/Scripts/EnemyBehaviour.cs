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

    public event EventHandler OnEnemyAttack;

    
    private Rigidbody2D enemyRigidBody2D;
    private Animator enemyAnimator;
    private bool isAlert = false;
    private string[] states = new string[] {"Idle", "Walk", "Run", "Trans", "Attack"};
    //public bool m_FacingRight;


    


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
            
            resetAnimatorState("Idle");

        }
        else if (distanceFromPlayer <= meleDistance)
        {
            if(enemyAnimator.GetBool("Attack"))
            {
                resetAnimatorState("Trans");  
            }

            int chance = UnityEngine.Random.Range(1,100);
            if(chance == 1) {
                resetAnimatorState("Attack");
                if (OnEnemyAttack != null)
                {
                    OnEnemyAttack(this, EventArgs.Empty);
                }
            }

            // if (OnEnemyAttack != null)
            // {
            //     OnEnemyAttack(this, EventArgs.Empty);
            // }

        }
        else if (distanceFromPlayer <= walkDistance)
        {
            // Start walking towards Hero
            transform.position = Vector2.MoveTowards(transform.position, Hero.transform.position, walkSpeed);

            resetAnimatorState("Walk");
        }
        else if (isAlert)
        {
            // Run towards Hero
            transform.position = Vector2.MoveTowards(transform.position, Hero.transform.position, runSpeed);

            resetAnimatorState("Run");
        }

        // Flip character
        float dir = transform.position.x - Hero.transform.position.x;

        if (dir < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (dir > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

    }

    void resetAnimatorState(string currentState) 
    {
        for (int i = 0; i < states.Length; i++){
            enemyAnimator.SetBool(states[i], false);
        }
        enemyAnimator.SetBool(currentState, true);
    }
}
