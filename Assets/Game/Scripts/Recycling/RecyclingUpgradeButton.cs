using GameOn.TagMaskField;
using UnityEngine;
using Zenject;

public class RecyclingUpgradeButton : MonoBehaviour, IGroundButton
{
    [SerializeField] private TagMask _playerTag;
    //[SerializeField] 
    [SerializeField, Min(0)] private int _upgradePrice = 0;
    private int _index;

    //[SerializeField] 
    private RecyclingUpgrader _recyclerUpgrader;

    public int GetPrice()
    {
        return _upgradePrice;
    }

    [Inject]
    private void Construct(RecyclingUpgrader upgrader, BuyCanvas buyCanvas)
    {
        _recyclerUpgrader = upgrader;
    }

    public void Init(int index)
    {
        _index = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        HandlePressButton(other);
    }

    public void HandlePressButton(Collider other)
    {
        if (!_playerTag.Contains(other.gameObject.tag)) return;
        _recyclerUpgrader.TryUpgrade(_index);
    }
}
