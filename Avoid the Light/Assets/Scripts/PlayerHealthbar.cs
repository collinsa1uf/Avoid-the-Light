using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class PlayerHealthbar : MonoBehaviour
{
    public static float health = 100f;
    private bool isBeingDamaged = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }     

    public static void DamagePlayer()
    {
        health -= 1f;
    }

    public static void Regen()
    {

    }
}
