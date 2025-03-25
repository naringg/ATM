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
            LoadUserData(); // 게임 시작 시 데이터 불러오기
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
            Debug.LogWarning("유저 데이터가 없습니다. 저장할 수 없습니다.");
            return;
        }

        string path = Path.Combine(Application.persistentDataPath, userData.id + ".json");
        string json = JsonUtility.ToJson(userData);
        File.WriteAllText(path, json);
        Debug.Log($"[{userData.id}] 유저 데이터 저장 완료: " + path);
    }

    public void LoadUserData()
    {
        string path = Path.Combine(Application.persistentDataPath, "userdata.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            userData = JsonUtility.FromJson<UserData>(json);
            Debug.Log("유저 데이터 불러오기 완료");
        }
      // else
       // {
            // 파일이 없을 경우 기본 데이터 생성
           // userData = new UserData("김지환", 50000, 500000);
           // SaveUserData();  // 기본 데이터도 바로 저장
           // Debug.Log("기본 유저 데이터 생성 및 저장");
       // }
    }
}
