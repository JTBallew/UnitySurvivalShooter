using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip dealthClip;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    public bool canPassiveHeal;
    public bool hasShield;

    GameObject pickupUI;
    PickupManager pickupManager;
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();
        pickupUI = GameObject.FindGameObjectWithTag("Pickup");
        pickupManager = pickupUI.GetComponent<PickupManager>();
        currentHealth = startingHealth;
        hasShield = false;
    }

    void Update()
    {
        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }

        if (damaged)
        {
            damageImage.color = flashColor;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
        healthSlider.value = currentHealth;
    }

    public void TakeDamage (int amount)
    {
        if (hasShield)
        {
            pickupManager.shieldBreak.Play();
            hasShield = false;
        }
        else
        {
            damaged = true;
            StopCoroutine(nameof(HurtHealing));
            StartCoroutine(nameof(HurtHealing));
            currentHealth -= amount;
            playerAudio.Play();
            if (currentHealth <= 0 && !isDead)
            {
                Death();
            }
        }
    }

    void Death()
    {
        isDead = true;

        playerShooting.DisableEffects();

        anim.SetTrigger("Die");
        playerAudio.clip = dealthClip;
        playerAudio.Play();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    IEnumerator HurtHealing()
    {
        canPassiveHeal = false;
        yield return new WaitForSeconds(10f);
        canPassiveHeal = true;
    }
}
