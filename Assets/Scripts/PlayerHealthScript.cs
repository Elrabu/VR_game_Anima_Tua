using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthScript : MonoBehaviour
{
    [SerializeField] private int playerHealth = 10;
    [SerializeField] private float cooldown = 0.5f;
    [SerializeField] private float cooldownTimer = 0f;
    [SerializeField] private bool dead = false;
    [SerializeField] private Heartbeat heartbeat;


    
    void Update()
    {
       cooldownTimer -= Time.deltaTime;
      // Debug.Log(cooldownTimer);
      
    }


    /*
    7 Leben Herz klopfen beginnt und schneller und lauter
    */
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6 && cooldownTimer <= 0f)
        {
            damagePlayer(1);
        if (heartbeat != null)
        {
            heartbeat.TriggerHeartbeat(Mathf.Lerp(60f, 120f, 1f - (playerHealth / 10f)));
        }
             
            cooldownTimer = cooldown;

        }
        if (playerHealth <= 0 && dead == false)
        {
            Debug.Log("You Died!");
            dead = true;
            playerHealth = 10;
            if (heartbeat != null)
                heartbeat.PlayTinitus();
            //change scene
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