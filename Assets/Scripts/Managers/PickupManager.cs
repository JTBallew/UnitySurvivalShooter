using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PickupManager : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI staminaText;
    public Image shieldImage;
    public Color activeColor = new Color(1f, 1f, 1f, 1f);
    public Color warningColor = new Color(1f, 1f, 0.75f, 1f);
    public AudioSource damageAudio;
    public AudioSource staminaAudio;
    public AudioSource shieldEquip;
    public AudioSource ammoAudio;
    public AudioSource healAudio;
    public AudioSource shieldBreak;

    private GameObject player;
    private PlayerShooting playerShooting;
    private PlayerMovement playerMovement;
    private PlayerHealth playerHealth;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerShooting = player.GetComponentInChildren<PlayerShooting>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        if (playerShooting.pickedUpPowerup)
        {
            StopCoroutine(nameof(DamageColor));
            StartCoroutine(nameof(DamageColor));
        }

        if (playerMovement.pickedUpPowerup)
        {
            StopCoroutine(nameof(StaminaColor));
            StartCoroutine(nameof(StaminaColor));
        }

        if (playerHealth.hasShield)
        {
            shieldImage.color = activeColor;
        }
        else
        {
            shieldImage.color = Color.clear;
        }
    }

    IEnumerator DamageColor()
    {
        damageText.color = activeColor;
        yield return new WaitForSeconds(7f);
        damageText.color = warningColor;
        yield return new WaitForSeconds(3f);
        damageText.color = Color.clear;
    }
    IEnumerator StaminaColor()
    {
        staminaText.color = activeColor;
        yield return new WaitForSeconds(7f);
        staminaText.color = warningColor;
        yield return new WaitForSeconds(3f);
        staminaText.color = Color.clear;
    }
}
