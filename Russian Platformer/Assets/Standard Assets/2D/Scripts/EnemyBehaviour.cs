using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;


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
    PlatformerCharacter2D platformerCharacter2D;

    private Rigidbody2D enemyRigidBody2D;
    private Animator enemyAnimator;
    private bool isAlert = false;
    private string[] states = new string[] { "Idle", "Walk", "Run", "Trans", "Attack" };
    private float attackTimer;
    private float attackCoolDownTime = 3; //Seconds
    private bool isAlive = true;

    // Use this for initialization
    void Start()
    {
        enemyAnimator = GetComponent<Animator>();
        enemyRigidBody2D = GetComponent<Rigidbody2D>();
        platformerCharacter2D = GameObject.Find("CharacterRobotBoy").GetComponent<PlatformerCharacter2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive && Hero.GetComponent<PlatformerCharacter2D>().isHeroAlive)
        {
            updatePlayer();
        }
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

            if (this.isAlive && !PlatformerCharacter2D.EnemyInRange.Contains(this.transform))
            {
                PlatformerCharacter2D.EnemyInRange.Add(this.transform);
                Debug.Log("Added : " + PlatformerCharacter2D.EnemyInRange[PlatformerCharacter2D.EnemyInRange.Count - 1].name);
                //transform.GetComponent<SpriteRenderer>().color = Color.red;
            }
            resetAnimatorState("Trans");

            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCoolDownTime)
            {
                resetAnimatorState("Attack");
                attackTimer = 0;

                if (OnEnemyAttack != null)
                {
                    OnEnemyAttack(this, EventArgs.Empty);
                }
            }

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

        if (distanceFromPlayer >= meleDistance)
        {
            if (PlatformerCharacter2D.EnemyInRange.Contains(this.transform))
            {
                Debug.Log("Removed : " + PlatformerCharacter2D.EnemyInRange[PlatformerCharacter2D.EnemyInRange.Count - 1].name);
                PlatformerCharacter2D.EnemyInRange.Remove(this.transform);
                //transform.GetComponent<SpriteRenderer>().color = Color.white;
            }
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
        for (int i = 0; i < states.Length; i++)
        {
            enemyAnimator.SetBool(states[i], false);
        }

        if (currentState != "")
        {
            enemyAnimator.SetBool(currentState, true);
        }

    }

    public void GotAttacked()
    {
        Transform result = this.transform.Find("HealthBar").Find("Bar");

        if (result)
        {
            result.localScale = new Vector3(result.localScale.x - 0.1f, result.localScale.y, result.localScale.z);
        }

        if (result.localScale.x < 0)
        {
            isAlive = false;
            PlatformerCharacter2D.EnemyInRange.Remove(this.transform);
            resetAnimatorState("");
            enemyAnimator.SetTrigger("Dead");
            transform.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(TimeBeforeDestroy(2f));
        }

    }

    public void AttackOnHero()
    {
        Hero.GetComponent<PlatformerCharacter2D>().GotAttacked();
    }

    public IEnumerator TimeBeforeDestroy(float timeBeforeJump)
    {
        yield return new WaitForSeconds(timeBeforeJump);
        Destroy(this.gameObject);
    }

}

