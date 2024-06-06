using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class MoneyUI : MonoBehaviour
{
    public UnityEvent OnGainMoney;

    public RectTransform rectTransform;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private TextMeshProUGUI moneyText;
    [SerializeField]
    private TextMeshProUGUI moneyTextShadow;

    private void OnEnable()
    {
        StartCoroutine(SetupDelegatesWithDelay());
    }

    private IEnumerator SetupDelegatesWithDelay()
    {
        yield return new WaitForSeconds(0.1f);

        MoneyManager.instance.OnMoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        MoneyManager.instance.OnMoneyChanged -= OnMoneyChanged;
    }

    private void OnMoneyChanged(int moneyAmount) 
    {
        moneyText.text = (moneyAmount < 10)? "0" + moneyAmount.ToString() : moneyAmount.ToString();
        moneyTextShadow.text = moneyText.text;

        anim.SetTrigger("Toggle");
        OnGainMoney?.Invoke();
    }

    public Vector3 GetUIworldPosition()
    {
        Vector3 worldPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, rectTransform.transform.position, Camera.main, out worldPosition);
        return worldPosition;
    }
}
