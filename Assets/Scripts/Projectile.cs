using UnityEngine;

public class Projectile: MonoBehaviour
{
    private GameObject shooter; // Who threw the projectile
    public float speed = 10f;
    public float rotateSpeed = 5f;

    // This is for the ThrowController
    public void SetShooter(GameObject s)
    {
        shooter = s;
    }

    private void OnTriggerEnter(Collider other)
    {
         Debug.Log("Projectile collided with: " + other.gameObject.name);
        
        // Ignore if we hit ourselves
        if (other.gameObject == shooter)
            return;

        // Check if we hit something taggable
        Taggable target = other.GetComponent<Taggable>();

        if (target != null && shooter != null)
        {
            Taggable shooterTag = shooter.GetComponent<Taggable>();

            // Only transfer tag if shooter is currently "it"
            if (shooterTag != null && shooterTag.canTagOthers)
            {
                TagManager.Instance.SetIt(other.gameObject);
            }
        }

        // Destroy projectile on hit
        Destroy(gameObject);
    }
}
