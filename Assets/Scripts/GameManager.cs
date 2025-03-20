using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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
                _instance = new GameObject("CharacerManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    public UserData userData;
    [SerializeField] private TMP_Text name;
    [SerializeField] private TMP_Text cash;
    [SerializeField] private TMP_Text balance;


    // Start is called before the first frame update
    void Start()
    {
        userData = new UserData("±Ë¡ˆ»Ø", 50000, 500000);
        SetUI();
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetUI()
    {
        name.text = userData.userName;
        cash.text = $"{userData.cash:N0}";
        balance.text = $"{userData.balance:N0}";
    }

}