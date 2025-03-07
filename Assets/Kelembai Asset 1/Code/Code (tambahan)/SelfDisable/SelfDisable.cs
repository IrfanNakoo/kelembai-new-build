using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDisable : MonoBehaviour
{
    private void DisableObject()
    {
        gameObject.SetActive(false);
    }
}
