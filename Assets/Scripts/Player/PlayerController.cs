using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private MouseClicker mouseClicker;

    public void MouseClick(InputAction.CallbackContext value)
    {
        if (value.performed) 
        {
            ObjectPooler.Instance.SpawnFromPool("ClickRipple", mouseClicker.GetRippleSpawnPosition().position, Quaternion.identity);
            mouseClicker.Click();
        }
    }
}
