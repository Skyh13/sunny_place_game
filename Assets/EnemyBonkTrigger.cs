using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBonkTrigger : MonoBehaviour
{

    EnemyMovement enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        enemyMovement = GetComponentInParent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.gameObject.CompareTag("ground") || c.gameObject.CompareTag("player")) {
            enemyMovement.ReverseDirection();
        }
    }
}
