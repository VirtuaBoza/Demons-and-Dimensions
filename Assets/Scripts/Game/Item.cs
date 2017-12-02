using System;
using UnityEngine;

public class Item
{
    protected int id;
    protected string itemType;
    protected string title;
    protected string spriteName;
    
    public Item()
    {
        id = -1;
        itemType = "Empty";
        title = string.Empty;
        spriteName = string.Empty;
    }

    protected Item(int id, string itemType, string title, string spriteName)
    {
        this.id = id;
        this.itemType = itemType;
        this.title = title;
        this.spriteName = spriteName;
    }

    public int ID { get { return id; } }
    public ItemType ItemType
    {
        get
        {
            if (!Enum.IsDefined(typeof(ItemType), itemType))
            {
                Debug.LogWarning("Item constructor did not recognize itemType.");
                return ItemType.Empty;
            }
            else
            {
                return (ItemType)Enum.Parse(typeof(ItemType), itemType, true);
            }
        }
    }
    public string Title { get { return title; } }
    public Sprite Sprite { get { return Resources.Load<Sprite>("Sprites/Items/" + spriteName); } }

    public override string ToString()
    {
        return title;
    }
}