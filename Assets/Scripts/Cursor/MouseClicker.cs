using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseClicker : MonoBehaviour
{
    public UnityEvent OnClick;
    public UnityEvent OnHover;

    [SerializeField]
    private Transform rippleSpawnPosition;

    private IClickable target;

    public void Click() 
    {
        OnClick?.Invoke();

        if (target == null)
            return;

        target.OnClicked();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IClickable newTarget)) 
        {
            if (target != null)
                target.OnUnhover();

            target = newTarget;
            OnHover?.Invoke();
            target.OnHover();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (target == null)
            return;

        if (collision.TryGetComponent(out IClickable oldTarget))
        {
            if (oldTarget == target) 
            {
                target.OnUnhover();
                target = null;
            }
        }
    }

    public Transform GetRippleSpawnPosition() 
    {
        return rippleSpawnPosition;
    }
}
