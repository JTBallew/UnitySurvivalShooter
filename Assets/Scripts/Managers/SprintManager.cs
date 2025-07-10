using UnityEngine;

public class SprintManager : MonoBehaviour
{
    public AudioClip noStaminaClip;
    public AudioClip fullStaminaClip;

    GameObject player;
    PlayerMovement playerMovement;
    AudioSource sprintAudio;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        sprintAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!playerMovement.canSprint)
        {
            if (playerMovement.currentStamina <= 0)
            {
                sprintAudio.clip = noStaminaClip;
                sprintAudio.Stop();
                sprintAudio.Play();
            }
            else if (playerMovement.currentStamina >= 99)
            {
                sprintAudio.clip = fullStaminaClip;
                sprintAudio.Stop();
                sprintAudio.Play();
            }
        }
    }
}
