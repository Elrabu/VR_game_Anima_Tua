using UnityEngine;

public class PlayerHealthScript : MonoBehaviour
{
    private int playerHealth = 10;
    private float cooldown = 0.5f;
    private float cooldownTimer = 0f;

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
    }

    void damagePlayer(int damage)
    {
        playerHealth -= damage;
        Debug.Log("Current Health: " + playerHealth);
    }
}