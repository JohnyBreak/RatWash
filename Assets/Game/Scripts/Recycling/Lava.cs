using GameOn.TagMaskField;
using UnityEngine;
using Zenject;

public class Lava : MonoBehaviour
{
    [SerializeField] private TagMask _collectableTag;

    //[Inject] 
    private Wallet _wallet;

    [Inject]
    private void Construct(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!_collectableTag.Contains(other.gameObject.tag)) return;
        if (!other.gameObject.TryGetComponent<Ore>(out Ore ore)) return;

        _wallet.AddMoney(ore.Settings.Price);
        ore.Recycle();
    }
}
