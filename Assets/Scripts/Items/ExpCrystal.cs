using UnityEngine;

public class ExpCrystal : MonoBehaviour, ICollectable
{
    float expAmount = 1;

    public void OnCollect()
    {
        Player.instance.CollectExp(expAmount);
        Destroy(gameObject);
    }

    public void SetExpAmount(float _expAmount) => expAmount = _expAmount;
}
