using UnityEngine;

public class SprintPickupScript : MonoBehaviour
{
    GameObject player;
    PlayerMovement playerMovement;
    GameObject pickupUI;
    PickupManager pickupManager;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponentInChildren<PlayerMovement>();
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
            pickupManager.staminaAudio.Play();
            playerMovement.pickedUpPowerup = true;
            Destroy(gameObject);
        }
    }
}
