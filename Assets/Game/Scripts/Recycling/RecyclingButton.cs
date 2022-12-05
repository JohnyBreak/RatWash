using GameOn.TagMaskField;
using UnityEngine;
using Zenject;

public class RecyclingButton : MonoBehaviour, IGroundButton
{
    [SerializeField] private TagMask _playerTag;

    private RecyclingStorage _recycling;

    [Inject]
    private void Construct(RecyclingStorage recycling)
    {
        _recycling = recycling;
    }

    private void OnTriggerEnter(Collider other)
    {
        HandlePressButton(other);
    }

    public void HandlePressButton(Collider other)
    {
        if (!_playerTag.Contains(other.gameObject.tag)) return;
        _recycling.StartRecycling();
    }

    public int GetPrice()
    {
        return 0;
    }
}
