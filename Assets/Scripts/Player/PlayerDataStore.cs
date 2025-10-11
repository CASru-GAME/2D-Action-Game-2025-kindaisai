using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataStore : MonoBehaviour
{
    public PlayerController2D PlayerController2D { get; private set; }
    public ItemSystem ItemSystem{ get; private set; }
    public ItemTable ItemTable{ get; private set; }
    public ScoreSystem ScoreSystem { get; private set; }
    public HitPointSystem HitPointSystem { get; private set; }
    public PlayerLife playerLife{ get; private set; }

    void Start()
    {
        PlayerController2D = GetComponent<PlayerController2D>();
        ItemSystem = GetComponent<ItemSystem>();
        ItemTable = GetComponent<ItemTable>();
        HitPointSystem = GetComponent<HitPointSystem>();
        playerLife = GetComponent<PlayerLife>();
    }
}