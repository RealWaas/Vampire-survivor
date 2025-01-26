using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private HealthSystem healthSystem;
    [SerializeField] private Image healthBar;
    
    private void Start()
    {
        healthSystem.OnHealthUpdated += UpdateHealth;
    }

    private void OnDestroy()
    {
        healthSystem.OnHealthUpdated -= UpdateHealth;
    }

    public void UpdateHealth()
    {
        healthBar.fillAmount = (float)healthSystem.health / (float)healthSystem.maxHealth;
    }
}
