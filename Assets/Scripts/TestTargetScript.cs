using UnityEngine;

public class TestTargetScript : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float health = 100f;
    private Animation animator;

    private void Start()
    {
        animator = GetComponent<Animation>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            animator.Stop();
            animator.Play("Death");
            Destroy(gameObject);
        }
    }
}
