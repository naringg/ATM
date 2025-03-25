using System.IO;
using TMPro;
using UnityEngine;

public class PopupSignUp : MonoBehaviour
{
    [SerializeField] private GameObject PopupLogIn;

    [SerializeField] private GameObject SignUpPage;
    [SerializeField] private TMP_InputField inputId;
    [SerializeField] private TMP_InputField inputPassword;
    [SerializeField] private TMP_InputField inputPasswordConfirm;
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_Text ErrorInfo;
    [SerializeField] private GameObject Checkinfo;



    private string SaveDirectory => Application.persistentDataPath;

    public void OnClickSignUp()
    {
        string id = inputId.text;
        string password = inputPassword.text;
        string passwordConfirm = inputPasswordConfirm.text;
        string name = inputName.text;

        if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(password) ||
            string.IsNullOrEmpty(passwordConfirm) || string.IsNullOrEmpty(name))
        {
            Checkinfo.SetActive(true);
            ErrorInfo.text ="��� �ʵ带 �Է����ּ���.";
            return;
        }

        if (password != passwordConfirm)
        {
            Checkinfo.SetActive(true);
            ErrorInfo.text = ("��й�ȣ�� ���� ��ġ���� �ʽ��ϴ�.");
            return;
        }

        string filePath = Path.Combine(SaveDirectory, id + ".json");

        if (File.Exists(filePath))
        {
            Checkinfo.SetActive(true);
            ErrorInfo.text = ("�̹� �����ϴ� ID�Դϴ�.");
            return;
        }

        UserData newUser = new UserData(id, password, name, 50000, 500000);
        string json = JsonUtility.ToJson(newUser);
        File.WriteAllText(filePath, json);
        ErrorInfo.text = ("ȸ������ ����!.");
        Debug.Log("ȸ������ ����! ���� ������ ���� �Ϸ�: " + filePath);
    }

    public void Checkinfofalse()
    {
        Checkinfo.SetActive(false);
    }
    public void SignUpPagefalse()
    {
        SignUpPage.SetActive(false);
        PopupLogIn.SetActive(true);
    }
    public void SignUpPagetrue()
    {
        SignUpPage.SetActive(true);
        PopupLogIn.SetActive(false);
    }
}
