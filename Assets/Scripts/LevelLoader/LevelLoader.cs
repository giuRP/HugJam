using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;

    [SerializeField]
    private Animator sceneTransition;

    private void Awake()
    {
        instance = this;
    }

    public void LoadLevel(int level)
    {
        StartCoroutine(LoadLevelCoroutine(level));
    }

    public void ReloadLevel()
    {
        StartCoroutine(LoadLevelCoroutine(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevelCoroutine(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadLevelCoroutine(int level)
    {
        sceneTransition.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(level);
    }
}
