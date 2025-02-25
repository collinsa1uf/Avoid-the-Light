using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetHealth(float currentHealth)
    {
        slider.value = currentHealth;
    }
}
