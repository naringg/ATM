using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]private UserData userData;
    

    // Start is called before the first frame update
    void Start()
    {
        userData = new UserData("±Ë¡ˆ»Ø",50000,500000);
    }

}
