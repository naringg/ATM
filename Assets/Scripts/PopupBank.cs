using System.IO;
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
            GameManager.Instance.SaveUserData(); // 저장

        }
    }
    public void GetInputValue(TMP_InputField inputField)
    {
        // 입력 필드의 값을 가져와 정수로 변환
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
            GameManager.Instance.SaveUserData(); // 저장

        }
    }
    public void GetInputValueWithDrawal(TMP_InputField inputField)
    {
        // 입력 필드의 값을 가져와 정수로 변환
        if (int.TryParse(inputField.text, out int InputValueWithDrawal))
        {
            Withdrawal(InputValueWithDrawal);
        }
    }

    public void TransferMoney(int amount, string targetId)
    {
        if (string.IsNullOrEmpty(targetId))
        {
            Debug.LogWarning("송금 대상 ID를 입력해주세요.");
            return;
        }

        if (GameManager.Instance.userData.balance < amount)
        {
            Debug.LogWarning("잔액이 부족합니다.");
            return;
        }

        string path = Path.Combine(Application.persistentDataPath, targetId + ".json");
        if (!File.Exists(path))
        {
            Debug.LogWarning("해당 ID의 유저가 존재하지 않습니다.");
            return;
        }

        string targetJson = File.ReadAllText(path);
        UserData targetUser = JsonUtility.FromJson<UserData>(targetJson);

        GameManager.Instance.userData.balance -= amount;
        targetUser.balance += amount;

        GameManager.Instance.SetUI();
        GameManager.Instance.SaveUserData();
        File.WriteAllText(path, JsonUtility.ToJson(targetUser));

        Debug.Log($"송금 완료: {targetUser.userName}에게 {amount:N0}원 송금 완료!");
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
