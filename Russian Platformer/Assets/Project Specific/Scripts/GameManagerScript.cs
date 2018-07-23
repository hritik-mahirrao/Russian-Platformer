using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class GameManagerScript : MonoBehaviour
{
    public Platformer2DUserControl platformer2DUserControl;
    public EnemyBehaviour enemyBehaviour;
    public Transform playerHealthBar, enemyHealthBar;


    public int playerHealth;
    // Use this for initialization
    void Start()
    {
        enemyBehaviour.OnEnemyAttack += OEA;
    }

    private void OEA(object sender, System.EventArgs e)
    {
        // Debug.Log("hit");
        if (GameObject.Find("Bar").GetComponent<Transform>().localScale.x > 0)
        {
            playerHealthBar.localScale = new Vector3(GameObject.Find("Bar").GetComponent<Transform>().localScale.x - 0.006f, 1);
        }

        if (platformer2DUserControl.m_Knife || platformer2DUserControl.m_Kick)
        {
            if (enemyHealthBar.transform.localScale.x > 0)
            {
                enemyHealthBar.localScale = new Vector3(enemyHealthBar.transform.localScale.x - 0.008f, 1);
            }
        }

    }


}
