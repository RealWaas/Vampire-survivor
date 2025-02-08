public class ExpCrystal : Item
{
    float expAmount = 1;

    public override void OnCollect()
    {
        Player.instance.CollectExp(expAmount);
        gameObject.SetActive(false);
    }

    public void SetExpAmount(float _expAmount) => expAmount = _expAmount;
}
