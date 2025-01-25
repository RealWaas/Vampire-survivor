using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
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
        healthBar.fillAmount = healthSystem.health / healthSystem.maxHealth;
    }
}
