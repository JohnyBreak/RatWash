using GameOn.TagMaskField;
using UnityEngine;
using Zenject;

public class WasherUpgradeButton : MonoBehaviour, IGroundButton
{
    [SerializeField] private TagMask _playerTag;
    //[SerializeField] 
    [SerializeField, Min(0)] private int _upgradePrice = 0;
    private int _index;

    //[SerializeField] 
    private WasherUpgrader _washerUpgrader;

    public int GetPrice() 
    {
        return _upgradePrice;
    }

    [Inject]
    private void Construct(WasherUpgrader upgrader, BuyCanvas buyCanvas)
    {
           _washerUpgrader = upgrader;
    }

    public void Init( int index)
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
        _washerUpgrader.TryUpgrade(_index);
    }
}
