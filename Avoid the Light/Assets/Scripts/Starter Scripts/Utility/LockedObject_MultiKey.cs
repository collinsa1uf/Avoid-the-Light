﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedObject_MultiKey : MonoBehaviour
{
    //"Locked" object that can be opened with an Item in the players inventory that matches certain conditions

    [Header("Type")]
    [Tooltip("the Game Object to be changed.")]
    public GameObject unlockGameObject;
    [Tooltip("Set True for affected Game Object to be activated or False to be deactivated when a key is used.")]
    public bool setActive = false;
    public bool oneTimeActivation = true;
    private bool used = false;

    [System.Serializable]
    public class KeyInfo
    {
        [Tooltip("Name of the key that is required to unlock the gameObject")]
        public string keyName = "";
        [Tooltip("ID of the key that is required to unlock the gameObject")]
        public int keyID = 0;
    }

    [Header("Key Info")] 
    [Tooltip("The keys that need to be in the player's inventory to unlock the gameObject")]
    public List<KeyInfo> neededKeys;

    private List<PlayerInventory.Item> keysToRemove = new List<PlayerInventory.Item>();

    [Header("Key Behaviour")]
    public bool useKeyName = true;
    public bool useKeyID = false;

    [Header("Development Tools")]
    public bool showDebug = false;


    //Pass amount = Keys needed
    private int passAmount = 0;

    private PlayerInventory inv;

    private void Start()
    {
        inv = FindObjectOfType<PlayerInventory>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!used)
        {
            foreach (var key in neededKeys) //Go through player inventory keys.Count times, checking for a match for each key
            {
                foreach (PlayerInventory.Item i in inv.inventory)
                {
                    if (useKeyName && i.itemName.ToLower() == key.keyName.ToLower())
                    {
                        DevDebug($"<color=cyan>Door</color>: key {i.itemName} used at {this.name}", this.gameObject);

                        passAmount++; //Increase pass amount
                        if (i.destroyOnUse)
                        {
                            keysToRemove.Add(i);
                        }
                        break;
                    }
                    if (useKeyID && i.itemID == key.keyID)
                    {
                        passAmount++;
                        if (i.destroyOnUse)
                        {
                            keysToRemove.Add(i);
                        }
                        
                        break;
                    }
                }
            }

            //Do you have all the keys?
            if (passAmount == neededKeys.Count)
            {
                DevDebug("You have all the keys!");
                unlockGameObject.SetActive(setActive);
                Behaviors();
                
            }
            else
            {
                DevDebug("You don't have all the keys...");
            }
            //Reset
            passAmount = 0;

        }
    }


    private void Behaviors()
    {
        
        //Remove Items
        foreach (var key in keysToRemove)
        {
            DevDebug($"<color=red>Remove Key</color>: {key.itemName} is being removed from inventory");
            inv.RemoveItemFromInventory(key);
            keysToRemove = new List<PlayerInventory.Item>();
        }

        //Set activation
        if (oneTimeActivation)
        {
            used = true;
        }
    }

    private void DevDebug(string str)
    {
        if (showDebug)
        {
            Debug.Log(str);
        }
    }
    private void DevDebug(string str, GameObject reference)
    {
        if (showDebug)
        {
            Debug.Log(str, reference);
        }
    }

}
