using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth;
    public float environmentDamagePerSecond;


    float currentHealth;

    bool inTheSun = false;

    PlayerBottomTrigger bottomTrigger;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        bottomTrigger = GetComponentInChildren<PlayerBottomTrigger>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!inTheSun) {
            currentHealth -= (environmentDamagePerSecond * Time.deltaTime);
        }

        UpdateHealthBar();

        if (currentHealth <= 0)  {
            GameManager.Instance.GameOver();
        }
    }

    void UpdateHealthBar()
    {
        UIHealthBar.instance.SetValue((float)currentHealth / maxHealth);
    }

    public void TakeDamage(int damagePoints)
    {
        currentHealth -= damagePoints;
    }

    void OnCollisionEnter2D (Collision2D c) {
        if (c.gameObject.tag == "enemy" && !bottomTrigger.GetIsTriggered())
        {
            TakeDamage(1);
        }
    }

    void OnTriggerEnter2D (Collider2D c) {
        if (c.gameObject.tag == "sunnySpot") {
            inTheSun = true;
        }
    }

    void OnTriggerExit2D (Collider2D c) {
        if (c.gameObject.tag == "sunnySpot") {
            inTheSun = false;
        }
    }
}
