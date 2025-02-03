using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Money : MonoBehaviour
{
    public event EventHandler<int> CoinsChange;
    [SerializeField] private int coins;
    public int Coins { get => coins; set => coins = value; }
    public void Add(int Coins)
    {
        coins += Coins;
        CoinsChange?.Invoke(this, coins);
    }
    public void Sub(int Coins)
    {
        coins -= Coins;
        CoinsChange?.Invoke(this, coins);
    }
}
