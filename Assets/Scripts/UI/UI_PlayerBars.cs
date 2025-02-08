using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerBars : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private ExpSystem expSystem;

    [SerializeField] private Image healthBar;
    [SerializeField] private Image expBar;
    
    private void Awake()
    {
        healthSystem.OnHealthUpdated += UpdateHealth;
        expSystem.OnExpChanged += UpdateExp;
    }

    private void OnDestroy()
    {
        healthSystem.OnHealthUpdated -= UpdateHealth;
        expSystem.OnExpChanged -= UpdateExp;
    }

    /// <summary>
    /// Update the healthBar when the health has changed.
    /// </summary>
    public void UpdateHealth()
    {
        healthBar.fillAmount = healthSystem.health / healthSystem.maxHealth;
    }

    /// <summary>
    /// Update the experienceBar when the experience has changed.
    /// </summary>
    public void UpdateExp()
    {
        expBar.fillAmount = expSystem.currentExp / expSystem.expThreshold;
    }
}
