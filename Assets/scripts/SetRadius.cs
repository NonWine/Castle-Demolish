using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRadius : MonoBehaviour
{

    [SerializeField] private GameObject sphere;

    private void Awake()
    {

        Camera.main.GetComponentInParent<CameraMoving>().SetLevelObjectRotation(sphere);
    }
 
}
