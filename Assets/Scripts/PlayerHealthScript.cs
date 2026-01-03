using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{
    private int playerHealth = 5;
    private float cooldown = 0.5f;
    private float cooldownTimer = 0f;
    private bool dead = false;

    void Update()
    {
       cooldownTimer -= Time.deltaTime;
      // Debug.Log(cooldownTimer);
      
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6 && cooldownTimer <= 0f)
        {
            damagePlayer(1);
            cooldownTimer = cooldown;
        }
        if (playerHealth <= 0 && dead == false)
        {
            Debug.Log("You Died!");
            dead = true;
            playerHealth = 5;
            //change szene
            SceneManager.LoadScene("RespawnScene");
        }
    }

    void damagePlayer(int damage)
    {
        if (playerHealth > 0)
        {
            playerHealth -= damage;
            Debug.Log("Current Health: " + playerHealth);
        }
    }
}