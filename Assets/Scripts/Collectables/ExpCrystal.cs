public class ExpCrystal : Collectable
{
    float expAmount = 1;

    private void Awake()
    {
        ItemMagnet.OnGlobalMagnet += OnCollect;
    }

    private void OnDestroy()
    {
        ItemMagnet.OnGlobalMagnet -= OnCollect;
    }
    public override void OnCollect()
    {
        Player.instance.CollectExp(expAmount);
        gameObject.SetActive(false);
    }

    public void SetExpAmount(float _expAmount) => expAmount = _expAmount;
}
