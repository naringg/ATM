using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UserData
{
    public string userName;
    public int cash;
    public int balance;

    public UserData(string userName, int cash, int balance)
    {
        this.userName = userName;
        this.cash = cash;
        this.balance = balance;
    }
}
