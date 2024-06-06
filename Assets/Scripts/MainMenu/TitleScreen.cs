using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    [SerializeField]
    private float delayToEnableMainMenu = 0.5f;

    [Space(5)]
    [Header("References")]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private Animator titleAnim;
    [SerializeField]
    private GameObject pressAnyKeyGO;

    private bool closed;

    private void Update()
    {
        if (Input.anyKeyDown && !closed) 
        {
            closed = true;
            CloseTitleScreen();
        }
    }

    private void CloseTitleScreen() 
    {
        StartCoroutine(CloseTitleScreenCoroutine());
    }

    private IEnumerator CloseTitleScreenCoroutine() 
    {
        pressAnyKeyGO.SetActive(false);
        titleAnim.SetTrigger("Disable");

        yield return new WaitForSeconds(delayToEnableMainMenu);

        mainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
