using UnityEngine;

public class DamagePickupScript : MonoBehaviour
{
    GameObject player;
    PlayerShooting playerShooting;
    GameObject pickupUI;
    PickupManager pickupManager;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerShooting = player.GetComponentInChildren<PlayerShooting>();
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
            pickupManager.damageAudio.Play();
            playerShooting.pickedUpPowerup = true;
            Destroy(gameObject);
        }
    }
}
