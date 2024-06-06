using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionPanel : MonoBehaviour
{
    [SerializeField]
    private float closeDelay = 0.25f;
    [SerializeField]
    private TextMeshProUGUI questionText;
    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;

    public void InitQuestion(string Question, UnityEngine.Events.UnityAction Yes_Function, UnityEngine.Events.UnityAction No_Function)
    {
        yesButton.onClick.RemoveAllListeners();
        noButton.onClick.RemoveAllListeners();

        questionText.text = Question;

        yesButton.onClick.AddListener(Yes_Function);
        yesButton.onClick.AddListener(AnswerSelected);
        noButton.onClick.AddListener(No_Function);
        noButton.onClick.AddListener(AnswerSelected);
    }

    private void AnswerSelected()
    {
        StartCoroutine(ClosePanel());
    }

    private IEnumerator ClosePanel() 
    {
        yield return new WaitForSeconds(closeDelay);

        gameObject.SetActive(false);
    }
}
