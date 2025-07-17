using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;
    public GameObject ammoPickup;
    public GameObject healthPickup;
    public GameObject damagePickup;
    public GameObject sprintPickup;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }
        enemyAudio.volume = 1 * SoundManager.Instance.sfxVolume;
        enemyAudio.Play();

        currentHealth -= amount;

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <=0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play();

        int rng = UnityEngine.Random.Range(1, 101);
        if (rng >= 11 && rng <= 25)
        {
            Instantiate(ammoPickup, new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
        }
        else if (rng >= 26 && rng <= 30)
        {
            Instantiate(healthPickup, new Vector3 (transform.position.x, 0.5f, transform.position.z), transform.rotation);
        }
        else if (rng >= 31 && rng <= 33)
        {
            Instantiate(damagePickup, new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
        }
        else if (rng >= 34 && rng <= 36)
        {
            Instantiate(sprintPickup, new Vector3(transform.position.x, 0.5f, transform.position.z), transform.rotation);
        }
    }

    public void StartSinking()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        ScoreManager.Instance.score += scoreValue;
        ScoreManager.Instance.ShowScore();
        Destroy(gameObject, 2f);
    }
}
