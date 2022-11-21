
using UnityEngine;

public interface IGroundButton
{
    public void HandlePressButton(Collider other);
    public int GetPrice();
}
