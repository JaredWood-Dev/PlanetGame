using System.Collections.Generic;
using Enums;
using UnityEngine;

[CreateAssetMenu(fileName = "AttributeItem", menuName = "Scriptable Objects/AttributeItem")]
public class AttributeItem : Item
{
    [System.Serializable]
    public struct AttributeChange
    {
        public Enums.Attributes attribute;
        public Enums.Operation operation;
        public float value;
    }
    
    public List<AttributeChange> attributeChanges = new List<AttributeChange>();

    public override void OnPickUp(CharacterAttributes attributes)
    {
        foreach (AttributeChange change in attributeChanges)
        {
            switch (change.operation)
            {
                case Enums.Operation.Addition:
                {
                    float val = attributes.Attributes[change.attribute] + change.value;
                    attributes.Attributes[change.attribute] = val;
                    EventManager.UpdateStats(change.attribute, val);
                    break;
                }
                case Enums.Operation.Multiplication: 
                {
                    float val = attributes.Attributes[change.attribute] * change.value;
                    attributes.Attributes[change.attribute] = val;
                    EventManager.UpdateStats(change.attribute, val);
                    break;
                }
            }
        }
    }

    //May need changes
    public override void OnRemoved(CharacterAttributes attributes)
    {
        foreach (AttributeChange change in attributeChanges)
        {
            switch (change.operation)
            {
                case Enums.Operation.Addition:
                {
                    float val = attributes.Attributes[change.attribute] - change.value;
                    attributes.Attributes[change.attribute] = val;
                    EventManager.UpdateStats(change.attribute, val);
                    break;
                }
                case Enums.Operation.Multiplication: 
                {
                    float val = attributes.Attributes[change.attribute] / change.value;
                    attributes.Attributes[change.attribute] = val;
                    EventManager.UpdateStats(change.attribute, val);
                    break;
                }
            }
        }
    }
}
