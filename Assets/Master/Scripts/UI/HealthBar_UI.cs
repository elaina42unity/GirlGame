using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    //variables
    private CharacterStats myStats;
    [SerializeField] private Player player;

    private Slider slider;
    private void Start()
    {
        //get the components
        slider = GetComponentInChildren<Slider>();
        myStats = player.GetComponent<CharacterStats>();
    }

    private void Update()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        //get the value from character's stats
        slider.maxValue = myStats.maxHealth.GetValue();
        slider.value = myStats.currentHealth;
    }
}
