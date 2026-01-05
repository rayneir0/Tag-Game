using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowController : MonoBehaviour
{   
    // References
    private Animator animator;
    public GameObject pokeball;
    [SerializeField] private Transform attackPoint;
    public Transform cameraTransform;

    // Shooting variables
    public float shootForce = 3f;
    public float timeBetweenThrowing = 0.5f;
    public float projectileSpeed = 20f;
    public float maxShootDistance = 500f;

    //Enemy Settings
    private Transform target;
    public Vector3 aimOffset = new Vector3(0, 0.5f, 0);
    public Transform playerHand;

    // Flag to prevent spamming throws
    public bool allowInvoke = true;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Set target for enemy -> Player ignores this
        if (gameObject.CompareTag("Enemy"))
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
            else
                Debug.LogWarning("No Player found for Enemy to target!");
        }
    }
    void Update()
    {
        // If its a player, handle the input -> enemy ignores this
        if (gameObject.CompareTag("Player"))
            HandleInput();

    }
    private void HandleInput()
    {
        // Get right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            //Invoke resetThrow function
            if (allowInvoke)
            {
                // Throw animation
                animator.SetTrigger("Throw");
                ShootProjectile();
                Invoke("ResetThrow", timeBetweenThrowing);
                allowInvoke = false;
            }

        }
    }

    // Public so Enemy can use this function instead of the HandleInput Function -> Player ignores this
    public void Shoot()
    {
        if (!allowInvoke) return;

        animator.SetTrigger("Throw");
        ShootProjectile();
        Invoke("ResetThrow", timeBetweenThrowing);
        allowInvoke = false;
    }

    void ShootProjectile()
    {
        Vector3 direction;

        // Opponent target
        if (target != null)
        {
            Rigidbody characterRb = target.GetComponent<Rigidbody>();
            Vector3 characterVelocity;
            if (characterRb != null)
            {
                characterVelocity = characterRb.velocity;
            }
            else
            {
                characterVelocity = Vector3.zero;
            }

            Vector3 toCharacter = target.position - attackPoint.position;
            float distance = toCharacter.magnitude;
            float t = distance / shootForce;

            // Lead multipler to predict target movement
            float leadMultiplier = 5f + distance / 2f;
            Vector3 predictedPosition = target.position + characterVelocity * t * leadMultiplier;
            
            // Adjusting for aim offset
            Vector3 aimPoint = predictedPosition + aimOffset;

            direction = (aimPoint - attackPoint.position).normalized;
        }
        else
        {
            // Player aiming: using a raycast from the center of the screen
            RaycastHit hit;
            Vector3 targetPoint;
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenter);
                if (Physics.Raycast(ray, out hit, maxShootDistance))
                {
                    targetPoint = hit.point;
                }
                else
                {
                    // Fallback if nothing is hit
                    targetPoint = cameraTransform.position + cameraTransform.forward * maxShootDistance;
                }
                //Calculate direction from attackPoint to targetPoint
                direction = (targetPoint - attackPoint.position).normalized;
        }

        // Instantiate projectile at the attack point
        GameObject currProjectile = Instantiate(pokeball, attackPoint.position, Quaternion.LookRotation(direction));

        // Assign it to the shooter so projectile knows who fired it
        currProjectile.GetComponent<Projectile>().SetShooter(gameObject);

        currProjectile.GetComponent<Rigidbody>().AddForce(direction.normalized * shootForce, ForceMode.Impulse);

        // Destroy after 5 seconds
        Destroy(currProjectile, 5f);
    }
        
    private void ResetThrow()
    {
        allowInvoke = true;
    }
}
