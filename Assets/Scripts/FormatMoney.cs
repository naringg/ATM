using UnityEngine;
using TMPro;

public class MoneyFormatter : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText; 
    [SerializeField] private int defaultMoney = 0; 

    void Start()
    {
        SetMoney(defaultMoney);
    }

    public void SetMoney(int amount)
    {
        if (moneyText != null)
            moneyText.text = $"{amount:N0}"; //단위 적용
    }
}
