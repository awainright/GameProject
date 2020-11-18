using System.Collections;
using UnityEngine;

// Make the class abstract as it will need to be inherited by a subclass
public abstract class Character : MonoBehaviour
{
    // Properties common to all characters
    public float maxHitPoints;
    public float startingHitPoints;

    public enum CharacterCategory
    {
        PLAYER,
        ENEMY
    }

    public CharacterCategory characterCategory;

    public virtual void KillCharacter()
    {
        //Destroys the current game objects and removes it from the scene.
        Destroy(gameObject);
    }

    // Set the character back to it's original state
    public abstract void ResetCharacter();

    //Coroutine to inflict an amount of damage to the character over a period of time
    // interval = 0 to inflict a one-time damage hit
    // interval > 0 to continuously inflict damage at the set interval of time
    public abstract IEnumerator DamageCharacter(int damage, float interval);
}

