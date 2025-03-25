using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    [SerializeField] GameObject Buttons;
    [SerializeField] GameObject Deposite;
    [SerializeField] GameObject WithDrawal;
    private void Start()
    {
        UserData userData = GameManager.Instance.userData;
    }

    public void deposit(int depositeMoney)
    {
        if(GameManager.Instance.userData.cash >= depositeMoney)
        {
            GameManager.Instance.userData.cash -= depositeMoney;
            GameManager.Instance.userData.balance += depositeMoney;
            GameManager.Instance.SetUI();
            GameManager.Instance.SaveUserData(); // ����

        }
    }
    public void GetInputValue(TMP_InputField inputField)
    {
        // �Է� �ʵ��� ���� ������ ������ ��ȯ
        if (int.TryParse(inputField.text, out int inputValue))
        {
            deposit(inputValue);
        }
    }
    public void Withdrawal(int withDrawalMoney)
    {
        if (GameManager.Instance.userData.balance >= withDrawalMoney)
        {
            GameManager.Instance.userData.cash += withDrawalMoney;
            GameManager.Instance.userData.balance -= withDrawalMoney;
            GameManager.Instance.SetUI();
            GameManager.Instance.SaveUserData(); // ����

        }
    }
    public void GetInputValueWithDrawal(TMP_InputField inputField)
    {
        // �Է� �ʵ��� ���� ������ ������ ��ȯ
        if (int.TryParse(inputField.text, out int InputValueWithDrawal))
        {
            Withdrawal(InputValueWithDrawal);
        }
    }


    public void ActivatePopupDeposite()
    {
        Deposite.SetActive(true);
        Buttons.SetActive(false);
    }
    public void ActivateWithDrawal()
    {
        WithDrawal.SetActive(true);
        Buttons.SetActive(false);
    }
    public void DepositeBackHome()
    {
        Buttons.SetActive(true);
       Deposite.SetActive(false);
    }
    public void WithdrawalBackHome()
    {
        Buttons.SetActive(true);
        WithDrawal.SetActive(false);
    }

}
