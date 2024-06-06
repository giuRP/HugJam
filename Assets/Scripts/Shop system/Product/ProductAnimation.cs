using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductAnimation : MonoBehaviour
{

    private Animator anim;
    // Start is called before the first frame update
    void OnEnable()
    {
        anim = GetComponent<Animator>();    
    }

    public void SetNewAnimator(ProductUpgradeStats productUpgradeStats)
    {
        anim.runtimeAnimatorController = productUpgradeStats.animController;
    }
}
