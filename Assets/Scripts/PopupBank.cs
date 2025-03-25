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
    // 입금
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
    // 출금
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
    // 송금
    // ========================
    public void GetInputValueSendMoney()
    {
        string targetId = inputSendTargetId.text.Trim();

        if (string.IsNullOrEmpty(targetId))
        {
            ShowSendError("송금 대상 ID를 입력해주세요.");
            return;
        }

        if (!int.TryParse(inputSendAmount.text, out int amount))
        {
            ShowSendError("송금 금액이 올바르지 않습니다.");
            return;
        }

        TransferMoney(amount, targetId);
    }

    public void TransferMoney(int amount, string targetId)
    {
        if (GameManager.Instance.userData.balance < amount)
        {
            ShowSendError("잔액이 부족합니다.");
            return;
        }

        string path = Path.Combine(Application.persistentDataPath, targetId + ".json");
        if (!File.Exists(path))
        {
            ShowSendError("해당 ID의 유저가 존재하지 않습니다.");
            return;
        }

        string json = File.ReadAllText(path);
        UserData targetUser = JsonUtility.FromJson<UserData>(json);

        GameManager.Instance.userData.balance -= amount;
        targetUser.balance += amount;

        GameManager.Instance.SetUI();
        GameManager.Instance.SaveUserData();
        File.WriteAllText(path, JsonUtility.ToJson(targetUser));

        Debug.Log($"송금 완료: {targetUser.userName}에게 {amount:N0}원 송금 완료!");
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
    // UI 패널 전환
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
