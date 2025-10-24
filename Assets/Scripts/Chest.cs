using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactible
{
    public bool opened = false;
    public GameObject nullItem;
    public List<Item> itemPool = new List<Item>();
    public Sprite openImage;
    
    public override void Interact(GameObject player)
    {
        if (!opened)
        {
            //Set the image to the ope sprite
            GetComponent<SpriteRenderer>().sprite = openImage;

            int itemChosen = Random.Range(0, itemPool.Count);
            GameObject itemToSpawn =
                Instantiate(nullItem, transform.position + (transform.up * 2), Quaternion.identity);
            itemToSpawn.GetComponent<Collectible>().itemData = itemPool[itemChosen];
            
            opened = true;
        }
    }
}
