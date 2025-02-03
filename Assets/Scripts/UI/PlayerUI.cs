using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
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

    public void UpdateHealth()
    {
        healthBar.fillAmount = healthSystem.health / healthSystem.maxHealth;
    }

    public void UpdateExp()
    {
        expBar.fillAmount = expSystem.currentExp / expSystem.expThreshold;
    }
}
