using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    UnityEvent onHit;
    UnityEvent onDeath;
    public int health = 1;

    void OnTriggerEnter(Collider other) 
    {
        // If the other object has a Damage component
        // use a copy of that component to get the damage value
        if (other.TryGetComponent<Damage>(out Damage damageComponent))
        {
            onHit.Invoke();
            health -= damageComponent.damage;

            if (health <= 0)
            {
                onDeath.Invoke();
            }
        }
    }
}