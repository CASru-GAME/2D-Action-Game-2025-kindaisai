using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ItemSystem : MonoBehaviour
{
    [SerializeField] PlayerDataStore playerDataStore;

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.gameObject.GetComponent<Item>();
        if (item != null)
        {
            if (playerDataStore.ItemTable.items[item.ID].duration == -1.0f)
            {
                foreach (ItemEffect itemEffect in playerDataStore.ItemTable.items)
                {
                    if (itemEffect.isActive && itemEffect.duration == -1.0f)
                    itemEffect.deleteEffect(playerDataStore);
                }                
            }

            playerDataStore.ItemTable.items[item.ID].activateEffect(playerDataStore);
            if (playerDataStore.ItemTable.items[item.ID].duration != -1.0f) StartCoroutine(CallDeleteEffect(item.ID, playerDataStore.ItemTable.items[item.ID].duration));

            item.Suicide();
        }
    }

    IEnumerator CallDeleteEffect(int ID,float duration)
    {
        yield return new WaitForSeconds(duration);
        playerDataStore.ItemTable.items[ID].deleteEffect(playerDataStore);
    }
}