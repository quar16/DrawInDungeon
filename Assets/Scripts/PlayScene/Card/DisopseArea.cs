using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisopseArea : MonoBehaviour
{
    private void OnMouseEnter()
    {
        All.Manager().card.Dispose = true;
    }

    private void OnMouseExit()
    {
        All.Manager().card.Dispose = false;
    }
}
