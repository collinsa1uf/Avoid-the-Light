﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    [Tooltip("The max health the player can have")]
    public int maxHealth = 100;

    [Tooltip("The current health the player has")]
    public int currentHealth;

    [Tooltip("If you're using segmented health, this is gameObject that holds your health icons as its children")]
    public GameObject HealthBarSegments;

    [Tooltip("This is the Bar of health that you use if you're doing non-segmented health")]
    public GameObject HealthBar;

    private List<GameObject> Hearts = new List<GameObject>();//List of the GameObject hearts that you are using. These need to be in order

    private List<GameObject> TempHearts = new List<GameObject>();

    [Header("Properties")]
    [Tooltip("This is if you're using segmented health (like in legend of zelda)")]
    public bool isSegmented = false;

    [Tooltip("This makes it so you can have health over the maximum amount. CURRENTLY DOES NOT WORK WITH IS_SEGMENTED")]
    public bool allowOverHealing = false;

    [Tooltip("If you actually want to use a healthbar or not")]
    public bool useHealthBar = false;


    void Start()
    {
       SetUpHealth();
    }

    public void SetUpHealth()
    {
        if (isSegmented)
        {
            Hearts.Clear();
            TempHearts.Clear();
            foreach (Transform child in HealthBarSegments.transform)
            {
                child.gameObject.GetComponent<Image>().color = Color.white;//This makes the color to white, you can make this a public variable if you want to change it
                Hearts.Add(child.gameObject);
                TempHearts.Add(child.gameObject);
            }
            currentHealth = TempHearts.Count;
        }
        else
        {
            if (HealthBar)
            {
                HealthBar.GetComponent<Image>().type = Image.Type.Filled;
                HealthBar.GetComponent<Image>().fillMethod = (int)Image.FillMethod.Horizontal;
                HealthBar.GetComponent<Image>().fillOrigin = (int)Image.OriginHorizontal.Left;
                currentHealth = maxHealth;
                UpdateHealthBar();
            }
        }
    }

    public void DecreaseHealth(int value)//This is the function to use if you want to decrease the player's health somewhere
    {
        if (isSegmented)
        {
            SegmentedHealthDecrease(value);
            return;
        }
        currentHealth -= value;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
        UpdateHealthBar();
    }

    public void IncreaseHealth(int value)//This is the function to use if you want to increase the player's heath somewhere
    {
        if (isSegmented)
        {
            SegmentedHealthIncrease(value);
            return;
        }
        currentHealth += value;
        if (!allowOverHealing && currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateHealthBar();
    }

    private void SegmentedHealthDecrease(int value)//Helper function
    {
        if (value > TempHearts.Count)
        {
            value = TempHearts.Count;
        }
        for (int i = 0; i < value; i++)
        {
            TempHearts[currentHealth - 1].GetComponent<Image>().color = Color.black;
            TempHearts.RemoveAt(TempHearts.Count - 1);
            currentHealth--;
        }

        if (TempHearts.Count == 0)
        {
            currentHealth = 0;
        }
    }

    private void SegmentedHealthIncrease(int value)//Helper function
    {
        if (value + TempHearts.Count > Hearts.Count)
        {
            value = Hearts.Count - TempHearts.Count;
        }

        for (int i = 0; i < value; i++)
        {
            var temp = Hearts[currentHealth];
            temp.GetComponent<Image>().color = Color.white;
            TempHearts.Add(temp);
            currentHealth++;
        }
    }

    public void ResetHealth()//Resets health back to normal
    {
        if (isSegmented)
        {
            for (int i = 0; i < Hearts.Count; i++)
            {
                Hearts[i].GetComponent<Image>().color = Color.white;
            }

            TempHearts.Clear();

            foreach (var VARIABLE in Hearts)
            {
                TempHearts.Add(VARIABLE);
            }
            currentHealth = TempHearts.Count;
        }
        else
        {
            currentHealth = maxHealth;
            UpdateHealthBar();
        }
    }

    void UpdateHealthBar()//Updates the health bar according to the new health amounts
    {
        if (useHealthBar)
        {
            float fillAmount = (float)currentHealth / maxHealth;
            if (fillAmount > 1)
            {
                fillAmount = 1.0f;
            }

            HealthBar.GetComponent<Image>().fillAmount = fillAmount;
        }
    }

    //This is where we handle the place where the health is dealth with
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out DamageDealer damageValues))
        {
            if (damageValues.damageType == DamageDealer.DamageType.Enemy ||
                damageValues.damageType == DamageDealer.DamageType.Environment)
            {
                DecreaseHealth(damageValues.DamageValue);
                if (currentHealth == 0)
                {
                    TimeToDie();
                }
            }
        }
        if (collision.gameObject.TryGetComponent(out HealValue healingValue))
        {
            IncreaseHealth(healingValue.HealAmount);
            if (healingValue.DestroyOnContact)
            {
                Destroy(collision.gameObject);
            }
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.gameObject.TryGetComponent(out DamageDealer damageValues))
        {
            if (damageValues.damageType == DamageDealer.DamageType.Enemy ||
                damageValues.damageType == DamageDealer.DamageType.Environment)
            {
                DecreaseHealth(damageValues.DamageValue);
                if (currentHealth == 0)
                {
                    TimeToDie();
                }
            }
        }
        if (collision.gameObject.TryGetComponent(out HealValue healingValue))
        {
            IncreaseHealth(healingValue.HealAmount);
            if (healingValue.DestroyOnContact)
            {
                Destroy(collision.gameObject);
            }
        }

    }

    public void TimeToDie()
    {
        if (gameObject.TryGetComponent(out ThirdPersonMovement movement))
        {
            if (movement.anim != null)
            {
                StartCoroutine(deathTime(movement.anim));
            }
        }
    }

    public IEnumerator deathTime(Animator anim)
    {
        Debug.Log("Time to die");
        if (TryGetComponent(out PlayerAudio audio))
        {
            audio.DeathSource.Play();
        }
        yield return new WaitForSeconds(1f);
        if (GameObject.FindGameObjectWithTag("GameManager").TryGetComponent(out GameManager manager))
        {
            manager.Respawn(gameObject);
        }
        ResetHealth();
    }
}
