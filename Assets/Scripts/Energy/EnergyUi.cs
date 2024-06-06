using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EnergyUi : MonoBehaviour
{
    public RectTransform rectTransform;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private TextMeshProUGUI energyText;

    private void OnEnable()
    {
        StartCoroutine(SetupDelegatesWithDelay());
    }

    private IEnumerator SetupDelegatesWithDelay()
    {
        yield return new WaitForSeconds(0.1f);

        EnergyManager.Instance.OnEnergyChanged += OnEnergyChanged;
    }

    private void OnDisable()
    {
        EnergyManager.Instance.OnEnergyChanged -= OnEnergyChanged;
    }

    private void OnEnergyChanged(int energyAmount)
    {
        energyText.text = (energyAmount < 10) ? "0" + energyAmount.ToString() : energyAmount.ToString();

        //anim.SetTrigger("Toggle");
    }

    public Vector3 GetUIworldPosition()
    {
        Vector3 worldPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, rectTransform.transform.position, Camera.main, out worldPosition);
        return worldPosition;
    }
}
