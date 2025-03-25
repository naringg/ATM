using System.IO;
using TMPro;
using UnityEngine;

public class PopupLogin : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputId;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private GameObject loginPage;
    [SerializeField] private GameObject popupBankPage;
    [SerializeField] private GameObject errorPopup;
    [SerializeField] private TMP_Text errorMessage;

    private string SaveDirectory => Application.persistentDataPath;

    public void OnClickLogin()
    {
        string id = inputId.text.Trim();
        string password = inputPassword.text;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password))
        {
            ShowError("ID�� ��й�ȣ�� ��� �Է����ּ���.");
            return;
        }

        string filePath = Path.Combine(SaveDirectory, id + ".json");

        if (!File.Exists(filePath))
        {
            ShowError("�ش� ID�� ���� ������ �������� �ʽ��ϴ�.");
            return;
        }

        string json = File.ReadAllText(filePath);
        UserData loadedUser = JsonUtility.FromJson<UserData>(json);

        if (loadedUser.password != password)
        {
            ShowError("��й�ȣ�� �ùٸ��� �ʽ��ϴ�.");
            return;
        }

        GameManager.Instance.userData = loadedUser;
        GameManager.Instance.SetUI();

        loginPage.SetActive(false);
        popupBankPage.SetActive(true);
    }

    private void ShowError(string message)
    {
        errorMessage.text = message;
        errorPopup.SetActive(true);
    }

    public void HideError()
    {
        errorPopup.SetActive(false);
    }
}
