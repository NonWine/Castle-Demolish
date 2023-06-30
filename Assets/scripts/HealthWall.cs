using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthWall : MonoBehaviour
{
    // Start is called before the first frame update\
    public static HealthWall Instance { get; private set; }
    [SerializeField] private float _health;
    private void Awake()
    {
        Instance = this;
    }
    public float GetHealth()
    {
        return _health;
    }
}
