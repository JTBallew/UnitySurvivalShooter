using UnityEngine;
using TMPro;

public class AmmoManager : MonoBehaviour
{
    GameObject player;
    PlayerShooting playerShooting;
    TextMeshProUGUI ammoText;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerShooting = player.GetComponent<PlayerShooting>();
        ammoText = GetComponent<TextMeshProUGUI>();
        ammoText.text = "20 / 300";
    }

    void Update()
    {
        ammoText.text = playerShooting.currentAmmo + " / " + playerShooting.totalAmmo;
    }
}
