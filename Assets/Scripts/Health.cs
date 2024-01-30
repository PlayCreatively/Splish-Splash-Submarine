using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour {

    public UnityEvent onHit;
    public UnityEvent onDeath;
    public int health = 1;

    void OnTriggerEnter(Collider other) 
    {
        // If the other object has a Damage component
        // use a copy of that component to get the damage value
        if (other.TryGetComponent<Damage>(out Damage damageComponent))
        {
            Debug.Log(name + " hit by " + other.name);
            onHit.Invoke();
            health -= damageComponent.damage;

            if (health <= 0)
            {
                onDeath.Invoke();
                Destroy(gameObject);
            }
        }
    }
}