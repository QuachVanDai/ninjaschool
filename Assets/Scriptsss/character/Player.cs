﻿using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private static Player _Instance;
    public string Name;
    public int Level;
    [ReadOnly]public float MaxHp;
    public float CurrHp;
    public float MaxMp;
    public float CurrMp;
    public float PercentExp;
    public int Gold;
    public int MinDamage;
    public int MaxDamage;

    public SetPlayer SetPlayer;

    public PlayerEffect PlayerEffect;
    public static Player Instance { get => _Instance; }

    protected  void Awake()
    {
        if (Player._Instance != null) Debug.LogError("Only 1 Player allow to exist");
        Player._Instance = this;
    }
    public void Start()
    {
        LoadComponent();
    }
    protected void LoadComponent()
    {
        SetPlayer = new SetPlayer();
        Level = 1;
        MinDamage = SetPlayer.getDamePlayerDictionary(Level).Item1;
        MaxDamage = SetPlayer.getDamePlayerDictionary(Level).Item2;
        MaxHp = SetPlayer.getHPPlayerDictionary()[Level];
        CurrHp = SetPlayer.getHPPlayerDictionary()[Level];
        MaxMp = SetPlayer.getHPPlayerDictionary()[Level];
        CurrMp = SetPlayer.getMPPlayerDictionary()[Level];
    }
    public Player GetCharacter()
    {
        return this;
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "potion")
        {
            PickUpItems pickUpItems = collision.gameObject.GetComponent<PickUpItems>();
            try
            {
                MpSO MpSO = (MpSO)pickUpItems.slot.getItemSO();
                InventoryUpdate.Instance.UpdateMP(pickUpItems.slot, 1);
            }
            catch
            {
                HpSO HpSO = (HpSO)pickUpItems.slot.getItemSO();
                InventoryUpdate.Instance.UpdateHP(pickUpItems.slot, 1);
            }
            
        }
       else if (collision.gameObject.tag == "money")
        {
            PickUpItems pickUpItems = collision.gameObject.GetComponent<PickUpItems>();
            MoneySO money = (MoneySO)pickUpItems.slot.getItemSO();
            Gold += money.Xu;
            InventoryUpdate.Instance.UpdateGold(Gold);
        }
    }

}
