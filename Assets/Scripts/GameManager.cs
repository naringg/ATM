using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    public UserData userData;

    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text cash;
    [SerializeField] private TMP_Text balance;

    private string SavePath => Path.Combine(Application.persistentDataPath, "userdata.json");

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            LoadUserData(); // ���� ���� �� ������ �ҷ�����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SetUI();
    }

    public void SetUI()
    {
        name.text = userData.userName;
        cash.text = $"{userData.cash:N0}";
        balance.text = $"{userData.balance:N0}";
    }

    public void SaveUserData()
    {
        if (userData == null || string.IsNullOrEmpty(userData.id))
        {
            Debug.LogWarning("���� �����Ͱ� �����ϴ�. ������ �� �����ϴ�.");
            return;
        }

        string path = Path.Combine(Application.persistentDataPath, userData.id + ".json");
        string json = JsonUtility.ToJson(userData);
        File.WriteAllText(path, json);
        Debug.Log($"[{userData.id}] ���� ������ ���� �Ϸ�: " + path);
    }

    public void LoadUserData()
    {
        string path = Path.Combine(Application.persistentDataPath, "userdata.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            userData = JsonUtility.FromJson<UserData>(json);
            Debug.Log("���� ������ �ҷ����� �Ϸ�");
        }
      // else
       // {
            // ������ ���� ��� �⺻ ������ ����
           // userData = new UserData("����ȯ", 50000, 500000);
           // SaveUserData();  // �⺻ �����͵� �ٷ� ����
           // Debug.Log("�⺻ ���� ������ ���� �� ����");
       // }
    }
}
