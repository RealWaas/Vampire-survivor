using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    float healAmount = 25;

    public void OnCollect()
    {
        Player.instance.GainHeal(healAmount);
        gameObject.SetActive(false);
    }
}
