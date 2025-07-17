using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;
    public int totalAmmo;
    public int currentAmmo;
    public AudioClip gunshotClip;
    public AudioClip reloadClip;
    public TextMeshProUGUI ammoText;
    public bool doubleDamage;
    public bool pickedUpPowerup;
    public PauseButton pauseButton;

    private int startingAmmo = 300;
    private bool isReloading;
    private float timer;
    private Ray shootRay;
    private RaycastHit shootHit;
    private int shootableMask;
    private ParticleSystem gunParticles;
    private LineRenderer gunLine;
    private AudioSource gunAudio;
    private Light gunLight;
    private PlayerHealth playerHealth;
    private float effectsDisplayTime = .2f;

    void Awake()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
        playerHealth = GetComponentInParent<PlayerHealth>();
        totalAmmo = startingAmmo;
        currentAmmo = 30;
        isReloading = false;
        doubleDamage = false;
        pickedUpPowerup = false;
        pauseButton = GameObject.FindGameObjectWithTag("PauseButton").GetComponent<PauseButton>();
    }

    void Update()
    {
        if (pauseButton.isPaused)
        { 
            return;
        }
        timer += Time.deltaTime;
        ammoText.text = currentAmmo + " / " + totalAmmo;

        if (Input.GetButton("Fire1") && timer >= timeBetweenBullets)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }

        if (Input.GetButton("Reload") && currentAmmo < 30 && totalAmmo > 0 && !isReloading)
        {
            gunAudio.clip = reloadClip;
            gunAudio.Play();
            isReloading = true;
            StartCoroutine(nameof(Reload));
        }

        if (timer >= 10 && playerHealth.currentHealth < 100 && playerHealth.canPassiveHeal)
        {
            timer = 8;
            playerHealth.currentHealth += 5;
        }
    }
    void FixedUpdate()
    {
        if (pickedUpPowerup)
        {
            pickedUpPowerup = false;
            StopCoroutine(nameof(DamageBoost));
            StartCoroutine(nameof(DamageBoost));
        }
    }

    void Shoot()
    {
        if (currentAmmo <= 0 || isReloading)
        { 
            return;
        }
        timer = 0f;

        gunAudio.clip = gunshotClip;
        gunAudio.Play();

        gunLight.enabled = true;
        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, transform.position);

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {
            EnemyHealth enemyHealth = shootHit.collider.GetComponent<EnemyHealth>();
            if (enemyHealth !=null)
            {
                enemyHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);
        }
        else
        {
            gunLine.SetPosition(1, shootRay.origin + shootRay.direction * range);
        }

        currentAmmo--;
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1f);
        int temp = totalAmmo - (30 - currentAmmo);
        if (temp >= 0)
        {
            totalAmmo -= (30 - currentAmmo);
            currentAmmo = 30;
        }
        else if (temp < 0)
        {
            currentAmmo += totalAmmo;
            totalAmmo = 0;
        }
        isReloading = false;
    }

    IEnumerator DamageBoost()
    {
        damagePerShot = 40;
        doubleDamage = true;
        yield return new WaitForSeconds(10f);
        damagePerShot = 20;
        doubleDamage = false;
    }
}
