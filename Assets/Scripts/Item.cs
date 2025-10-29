using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public abstract class Item : ScriptableObject
{
    /*
     * The Basic Type of Item, other items inherit from this one.
     */
    
    public string ItemName;
    public string Description;
    public Sprite Icon;
    [NonSerialized] public CharacterAttributes Owner;
    
    public virtual void OnPickUp(CharacterAttributes attributes) {}
    public virtual void OnRemoved(CharacterAttributes attributes) {}
}
