using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D)), DisallowMultipleComponent]
public class Health : MonoBehaviour {

    public int health = 1;
    public UnityEvent onHit, onDeath;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the other object has a Damage component
        // use a copy of that component to get the damage value
        if (collision.TryGetComponent(out Damage damageComponent))
        {
            health -= damageComponent.damage;
            onHit?.Invoke();

            if (health <= 0)
                onDeath?.Invoke();
        }
    }

    public void Destroy() => Destroy(gameObject);
    public void DestroyRoot() => Destroy(transform.root.gameObject);
}