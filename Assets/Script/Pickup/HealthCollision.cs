using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollision : MonoBehaviour
{
    [SerializeField]
    private int _healthValue = 25;

    public int GetHealthValue()
    {
        return _healthValue;
    }
}
