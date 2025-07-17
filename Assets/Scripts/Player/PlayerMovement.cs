using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float startingStamina = 100f;
    public float currentStamina;
    public bool canSprint;
    public Slider staminaSlider;
    public Image fill;
    public Color noStaminaColor = new Color(0.5882353f, 0.1960784f, 0.1960784f, 1f);
    public Color fullStaminaColor = new Color(0f, 1f, 1f, 1f);
    public bool infiniteStamina;
    public bool pickedUpPowerup;

    private Vector3 movement;
    private Animator anim;
    private Rigidbody playerRigidbody;
    private int floorMask;
    private float camRayLength = 100f;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        anim = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        currentStamina = startingStamina;
        canSprint = true;
        infiniteStamina = false;
        pickedUpPowerup = false;
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        bool s = Input.GetButton("Sprint");

        Move(h, v);
        Turning();
        Animating(h, v);
        Sprint(s, h, v);

        if (pickedUpPowerup)
        {
            pickedUpPowerup = false;
            StopCoroutine(nameof(StaminaBoost));
            StartCoroutine(nameof(StaminaBoost));
        }
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidbody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h !=0f || v !=0f ;
        anim.SetBool("IsWalking", walking);
    }

    void Sprint(bool s, float h, float v)
    {
        if (s && canSprint && (h != 0 || v != 0))
        {
            speed = 8f;
            if (!infiniteStamina)
            {
                currentStamina -= 30 * Time.deltaTime;
            }
        }
        else
        {
            speed = 6f;
            if (currentStamina < 100)
            {
                currentStamina += 10 * Time.deltaTime;
            }
        }

        if (currentStamina <= 0)
        {
            canSprint = false;
            fill.color = noStaminaColor;
        }
        else if (currentStamina >= 100)
        {
            canSprint = true;
            fill.color = fullStaminaColor;
        }
        staminaSlider.value = currentStamina;
    }

    IEnumerator StaminaBoost()
    {
        infiniteStamina = true;
        currentStamina = 100;
        yield return new WaitForSeconds(10f);
        infiniteStamina = false;
    }
}
