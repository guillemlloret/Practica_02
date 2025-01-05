using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invisibility : MonoBehaviour
{
    public bool IsVisible;

    void Start()
    {
        IsVisible = true;
    }

    public void SetInvisible()
    {
        IsVisible = false;
    }
    public void SetVisible()
    {
        IsVisible = true;
    }
}
