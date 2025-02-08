using DG.Tweening;
using UnityEngine;

public abstract class Item : MonoBehaviour, ICollectable
{
    public abstract void OnCollect();

    public void OnMagnet()
    {
        DOTween.To(
            () => transform.position - Player.instance.transform.position, // Value getter
            x => transform.position = x + Player.instance.transform.position, // Value setter
            Vector3.zero,
            1);
    }
}
