using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragControable : MonoBehaviour, IShoot
{
    public void Controllable(string name,Button Flag)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
           
                if (hit.transform.gameObject.layer == 6 && Flag.interactable == false)
                {
                    StartCoroutine(name,hit.point);
                }
        }
    }

}
