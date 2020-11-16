using UnityEngine;

public class Player : Character
{
    // Called when player's collider touches an "Is Trigger" collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Retrieve the game object that the player collided with, and check the tag
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            // Grab a reference to the Item (scriptable object) inside the Consumable class and assign it to hitObject
            // Note: at this point it is a coin, but later may be other types of CanBePickedUp objects
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            // Check for null to make sure it was successfully retrieved, and avoid potential errors
            if (hitObject != null)
            {
                // debugging
                print("it: " + hitObject.objectName);

                switch (hitObject.itemType)
                {
                    case Item.ItemType.TREASURE:
                        break;

                    case Item.ItemType.HEALTH:
                        AdjustHitPoints(hitObject.quantity);
                        break;

                    case Item.ItemType.KEY:
                        break;

                    default:
                        break;
                }

                // Hide the game object in the scene to give the illusion of picking up
                collision.gameObject.SetActive(false);
            }
        }
    }

    public void AdjustHitPoints(int amount)
    {
        if( hitPoints == maxHitPoints )
        {
            print("At maximum health of: " + maxHitPoints);
        } 
        else
        {
            hitPoints = hitPoints + amount;
            print("Adjusted hitpoints by: " + amount + ". New value: " + hitPoints);
        }
    }
}
