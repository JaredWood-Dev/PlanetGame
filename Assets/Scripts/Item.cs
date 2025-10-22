using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public abstract class Item : ScriptableObject
{
    /*
     * The Basic Type of Item, other items inherit from this one.
     */
    
    public string ItemName;
    public string Description;
    public Sprite Icon;
    
    public virtual void OnPickUp(CharacterAttributes attributes) {}
    public virtual void OnRemoved(CharacterAttributes attributes) {}
}
