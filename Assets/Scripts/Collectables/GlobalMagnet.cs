public class GlobalMagnet : Collectable
{
    public override void OnCollect()
    {
        ItemMagnet.AttractAllCrystals();
        gameObject.SetActive(false);
    }
}
