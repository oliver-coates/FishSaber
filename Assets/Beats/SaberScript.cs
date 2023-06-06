using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SaberScript : MonoBehaviour
{
    public GameManager gameManager;
    public ActionBasedController XRController;

    private void OnCollisionEnter(Collision other) {        
        
        if (other.gameObject.CompareTag("Beat"))
        {
            // Apply haptic feedback

            XRController.SendHapticImpulse(0.5f, 0.1f);

            gameManager.Hit();

            Destroy(other.gameObject);
        }
    }
}
