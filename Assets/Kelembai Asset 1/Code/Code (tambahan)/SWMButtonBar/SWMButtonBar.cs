using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SWMButtonBar : MonoBehaviour
{
    [SerializeField] private GameObject objectToActivate;     // Object to activate
    [SerializeField] private GameObject objectToDeactivate1;  // First object to deactivate
    [SerializeField] private GameObject objectToDeactivate2;  // Second object to deactivate
    [SerializeField] private UnityEvent onActivationComplete; // Event called when activation is complete

    // Method to deactivate and activate objects
    public void ToggleActivation()
    {
        // Ensure previous objects are deactivated
        if (objectToDeactivate1 != null)
        {
            objectToDeactivate1.SetActive(false);
        }

        if (objectToDeactivate2 != null)
        {
            objectToDeactivate2.SetActive(false);
        }

        // Activate new object
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
            Debug.Log($"{nameof(SWMButtonBar)}: {objectToActivate.name} is now active.");
        }

        // Trigger any assigned UnityEvent
        onActivationComplete?.Invoke();
    }
}