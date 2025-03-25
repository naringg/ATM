using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupBank : MonoBehaviour
{
    [SerializeField] GameObject Buttons;
    [SerializeField] GameObject Deposite;
    [SerializeField] GameObject WithDrawal;
    [SerializeField] GameObject SendMoney;

    [SerializeField] TMP_InputField inputSendTargetId;
    [SerializeField] TMP_InputField inputSendAmount;

    [SerializeField] private GameObject sendErrorPopup;
    [SerializeField] private TMP_Text sendErrorMessage;

    private void Start()
    {
        UserData userData = GameManager.Instance.userData;
    }

    // ========================
    // �Ա�
    // ========================
    public void deposit(int depositeMoney)
    {
        if (GameManager.Instance.userData.cash >= depositeMoney)
        {
            GameManager.Instance.userData.cash -= depositeMoney;
            GameManager.Instance.userData.balance += depositeMoney;
            GameManager.Instance.SetUI();
            GameManager.Instance.SaveUserData();
        }
    }

    public void GetInputValue(TMP_InputField inputField)
    {
        if (int.TryParse(inputField.text, out int inputValue))
        {
            deposit(inputValue);
        }
    }

    // ========================
    // ���
    // ========================
    public void Withdrawal(int withDrawalMoney)
    {
        if (GameManager.Instance.userData.balance >= withDrawalMoney)
        {
            GameManager.Instance.userData.cash += withDrawalMoney;
            GameManager.Instance.userData.balance -= withDrawalMoney;
            GameManager.Instance.SetUI();
            GameManager.Instance.SaveUserData();
        }
    }

    public void GetInputValueWithDrawal(TMP_InputField inputField)
    {
        if (int.TryParse(inputField.text, out int InputValueWithDrawal))
        {
            Withdrawal(InputValueWithDrawal);
        }
    }

    // ========================
    // �۱�
    // ========================
    public void GetInputValueSendMoney()
    {
        string targetId = inputSendTargetId.text.Trim();

        if (string.IsNullOrEmpty(targetId))
        {
            ShowSendError("�۱� ��� ID�� �Է����ּ���.");
            return;
        }

        if (!int.TryParse(inputSendAmount.text, out int amount))
        {
            ShowSendError("�۱� �ݾ��� �ùٸ��� �ʽ��ϴ�.");
            return;
        }

        TransferMoney(amount, targetId);
    }

    public void TransferMoney(int amount, string targetId)
    {
        if (GameManager.Instance.userData.balance < amount)
        {
            ShowSendError("�ܾ��� �����մϴ�.");
            return;
        }

        string path = Path.Combine(Application.persistentDataPath, targetId + ".json");
        if (!File.Exists(path))
        {
            ShowSendError("�ش� ID�� ������ �������� �ʽ��ϴ�.");
            return;
        }

        string json = File.ReadAllText(path);
        UserData targetUser = JsonUtility.FromJson<UserData>(json);

        GameManager.Instance.userData.balance -= amount;
        targetUser.balance += amount;

        GameManager.Instance.SetUI();
        GameManager.Instance.SaveUserData();
        File.WriteAllText(path, JsonUtility.ToJson(targetUser));

        Debug.Log($"�۱� �Ϸ�: {targetUser.userName}���� {amount:N0}�� �۱� �Ϸ�!");
    }

    private void ShowSendError(string message)
    {
        sendErrorMessage.text = message;
        sendErrorPopup.SetActive(true);
    }

    public void HideSendError()
    {
        sendErrorPopup.SetActive(false);
    }

    // ========================
    // UI �г� ��ȯ
    // ========================
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

    public void ActivateSendMoney()
    {
        SendMoney.SetActive(true);
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

    public void SendMoneyBackHome()
    {
        Buttons.SetActive(true);
        SendMoney.SetActive(false);
    }
}
