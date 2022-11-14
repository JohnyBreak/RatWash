using GameOn.TagMaskField;
using UnityEngine;
using Zenject;

public class WasherButton : MonoBehaviour, IGroundButton
{
    [SerializeField] private TagMask _playerTag;

    private WasherStorage _washer;

    [Inject]
    private void Construct(WasherStorage washer)
    {
        _washer = washer;
    }

    private void OnTriggerEnter(Collider other)
    {
        HandlePressButton(other);
    }

    public void HandlePressButton(Collider other)
    {
        if (!_playerTag.Contains(other.gameObject.tag)) return;
        _washer.StartWash();
    }
}
