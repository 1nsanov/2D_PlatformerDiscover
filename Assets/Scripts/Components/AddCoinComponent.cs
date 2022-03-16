using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCoinComponent : MonoBehaviour
{
    [SerializeField] private int _numberCouins;
    private HeroPlayer _heroPlayer;

    private void Start()
    {
        _heroPlayer = FindObjectOfType<HeroPlayer>();
    }

    public void Add()
    {
        _heroPlayer.AddCoins(_numberCouins);
    }
}
