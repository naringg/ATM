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
            ErrorInfo.text ="모든 필드를 입력해주세요.";
            return;
        }

        if (password != passwordConfirm)
        {
            Checkinfo.SetActive(true);
            ErrorInfo.text = ("비밀번호가 서로 일치하지 않습니다.");
            return;
        }

        string filePath = Path.Combine(SaveDirectory, id + ".json");

        if (File.Exists(filePath))
        {
            Checkinfo.SetActive(true);
            ErrorInfo.text = ("이미 존재하는 ID입니다.");
            return;
        }

        UserData newUser = new UserData(id, password, name, 50000, 500000);
        string json = JsonUtility.ToJson(newUser);
        File.WriteAllText(filePath, json);
        ErrorInfo.text = ("회원가입 성공!.");
        Debug.Log("회원가입 성공! 유저 데이터 저장 완료: " + filePath);
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
