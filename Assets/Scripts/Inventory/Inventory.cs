﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public static Inventory instance;

    public List<Item> items = new List<Item>();
    public int space = 20;

    public delegate void OnItemChanged();
    public OnItemChanged OnItemChangedCallback;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found.");
        }
        instance = this;
    }

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(item);

            if (OnItemChangedCallback != null)
                OnItemChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (OnItemChangedCallback != null)
            OnItemChangedCallback.Invoke();
    }
}
