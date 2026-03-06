using UnityEngine;

public class Button : Interactable
{
   //A button is an object the player can simply interact

   public TriggerObject targetObject;
   public bool onePress;
   public Sprite pressedSprite;
   
   public override void Interact()
   {
       targetObject.Trigger();
       GetComponent<SpriteRenderer>().sprite = pressedSprite;
       
       //TODO: IMPLEMENT MORE ROBUST USAGE
       DefaultSprite = pressedSprite;
   }
}
