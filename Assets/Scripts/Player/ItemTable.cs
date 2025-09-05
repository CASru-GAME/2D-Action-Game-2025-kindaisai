using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class ItemTable : MonoBehaviour
{
    public ItemEffect[] items;
    // Start is called before the first frame update
    void Start()
    {
        items = new ItemEffect[2];

        items[0] = new ItemEffect(-1f, (playerDataStore) =>
        {
            playerDataStore.PlayerController2D.DoubleJump = true;
            items[0].isActive = true;
        }, (playerDataStore) =>
        {
            playerDataStore.PlayerController2D.DoubleJump = false;
            items[0].isActive = false;
        });
        items[1] = new ItemEffect(30f, (playerDataStore) =>
        {
            playerDataStore.PlayerController2D.isReverse = true;
            items[1].isActive = true;
        }, (playerDataStore) =>
        {
            playerDataStore.PlayerController2D.isReverse = false;
            items[1].isActive = false;
        });        
    }
}
    [System.Serializable]
    public class ItemEffect
    {   
        public bool isActive;
        public float duration;//-1なら効果時間が無限または体力回復などの持続しない効果
        public UnityAction<PlayerDataStore> activateEffect;
        public UnityAction<PlayerDataStore> deleteEffect;

        public ItemEffect(float duration,UnityAction<PlayerDataStore> activateEffect, UnityAction<PlayerDataStore> deleteEffect)
        {
            isActive = false;
            this.duration = duration;
            this.activateEffect = activateEffect;
            this.deleteEffect = deleteEffect;
        }
    }
