using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    // reference to health bar prefab
    public HealthBar healthBarPrefab;

    // a copy of the health bar prefab
    HealthBar healthBar;

    // Start is called before the first fram update
    public void Start()
    {
        // start the player off with starting hit point value
        hitPoints.value = startingHitPoints;

        // get a copy of the health bar prefab and store a reference to it
        healthBar = Instantiate(healthBarPrefab);

        // set the health bar's character property to this character so it can retrieve the maxHitPoints
        healthBar.character = this;
    }

    public Text WinText;
    public Button RestartButton;
    bool keyFlag = false;
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

                bool shouldDisappear = false;

                switch (hitObject.itemType)
                {
                    case Item.ItemType.TREASURE:
                        gameWon();
                        break;

                    case Item.ItemType.HEALTH:
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;

                    case Item.ItemType.KEY:
                        keyFlag = true;
                        break;

                    default:
                        break;
                }
                if (shouldDisappear)
                {
                    // Hide the game object in the scene to give the illusion of picking up
                    collision.gameObject.SetActive(false);
                }
                
            }
        }
    }

    public void gameWon(){
        WinText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Gate") && keyFlag == true)
        {
            collision.gameObject.GetComponent<Animator>().SetBool("KeyObtained",true);

            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            keyFlag = false;
        }
    }

    public bool AdjustHitPoints(int amount)
    {
        // Don't increase above the max amount
        if (hitPoints.value < maxHitPoints)
        {
            hitPoints.value = hitPoints.value + amount;
            print("Adjusted hitpoints by: " + amount + ". New value: " + hitPoints);
            return true;
        }

        // Return false if hit points is at max and can't be adjusted
        return false;
    }
}
