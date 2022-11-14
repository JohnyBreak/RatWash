using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgrader
{
    public virtual void Upgrade(int buttonIndex) { }
    public void SetUpgradeButtons();
        public void InitUpgradeButtons();
}
