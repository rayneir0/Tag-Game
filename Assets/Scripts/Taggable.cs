using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taggable : MonoBehaviour
{
    public bool canTagOthers = false;
    public int hitsReceived = 0;

    private void Update()
    {
        // Checks who is it, and assigns a boolean to them
        if (TagManager.Instance.IsIt(gameObject))
        {
            canTagOthers = true;
            
        }
        else
        {
            canTagOthers = false;
        }
    }

    public void OnHitByProjectile(GameObject shooter)
    {
        // Only transfer if shooter is "it"
        Taggable shooterTaggable = shooter.GetComponent<Taggable>();

        if (shooterTaggable != null && shooterTaggable.canTagOthers)
        {
            TagManager.Instance.SetIt(gameObject);
            
        }
    }
}
