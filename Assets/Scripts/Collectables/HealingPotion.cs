using UnityEngine;

public class HealingPotion : Collectable
{
    float healAmount = 25;

    public override void OnCollect()
    {
        Player.instance.GainHeal(healAmount);
        gameObject.SetActive(false);
    }
}
