using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsScreen;
    [SerializeField]
    private QuestionPanel questionPanel;

    public void StartGame() 
    {
        LevelLoader.instance.LoadNextLevel();
    }

    public void OpenCreditsScreen() 
    {
        creditsScreen.SetActive(true);
    }

    public void CloseCreditsScreen() 
    {
        creditsScreen.SetActive(false);
    }

    public void OpenExitQuestion() 
    {
        questionPanel.gameObject.SetActive(true);
        questionPanel.InitQuestion("Close the game?", CloseTheGame, CloseQuestionPanel);
    }

    private void CloseTheGame() 
    {
        Application.Quit();
    }

    private void CloseQuestionPanel() { }
}
