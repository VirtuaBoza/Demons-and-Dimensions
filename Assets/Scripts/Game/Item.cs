using System;
using UnityEngine;

public class Item
{
    protected int id;
    protected string itemType;
    protected string title;
    protected string slug;
    protected Sprite sprite;
    
    public Item()
    {
        id = -1;
        itemType = "Empty";
        title = string.Empty;
        slug = string.Empty;
        sprite = new Sprite();
    }

    protected Item(int id, string itemType, string title, string slug)
    {
        this.id = id;
        this.itemType = itemType;
        

        this.title = title;
        this.slug = slug;
        sprite = Resources.Load<Sprite>("Sprites/Items/" + slug);
    }

    public int ID { get { return id; } }
    public ItemType? ItemType
    {
        get
        {
            if (!Enum.IsDefined(typeof(ItemType), itemType))
            {
                Debug.LogWarning("Item constructor did not recognize itemType.");
                return null;
            }
            else
            {
                return (ItemType)Enum.Parse(typeof(ItemType), itemType, true);
            }
        }
    }
    public string Title { get { return title; } }
    public string Slug { get { return slug; } }
    public Sprite Sprite { get { return sprite; } }
}