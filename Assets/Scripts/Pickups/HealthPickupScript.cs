using UnityEngine;

public class HealthPickupScript : MonoBehaviour
{
    GameObject player;
    PlayerHealth playerHealth;
    GameObject pickupUI;
    PickupManager pickupManager;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        pickupUI = GameObject.FindGameObjectWithTag("Pickup");
        pickupManager = pickupUI.GetComponent<PickupManager>();
    }

    void Update()
    {
        transform.Rotate(0f, 0.2f, 0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            if (playerHealth.currentHealth >= 100)
            {
                pickupManager.shieldEquip.Play();
                playerHealth.hasShield = true;
            }
            else
            {
                pickupManager.healAudio.Play();
                playerHealth.currentHealth += 20;
            }
            Destroy(gameObject);
        }
    }
}
