using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStaminaSlider : MonoBehaviour
{

    private Slider staminaSlider;
    private GameObject player;

    private void Start()
    {
        staminaSlider = GetComponent<Slider>();
    }

    public void UpdatePlayer(GameObject newPlayer)
    {
        player = newPlayer;
    }

    void Update()
    {
        PlayerController pc = player.GetComponent<PlayerController>();
        float maxStamina = pc.stamina;
        float stamina = pc.GetCurrentStamina();

        staminaSlider.minValue = 0;
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = stamina;
    }
}
