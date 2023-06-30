using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SupportGravity : MonoBehaviour
{
    [SerializeField] private  float health;

    private Rigidbody _rb;
    private MeshCollider coll;

    private void Start()
    {
      
        coll = GetComponent<MeshCollider>();
        _rb = GetComponent<Rigidbody>();
    }

    public void EnablePhysics(Vector3 force, int damage)
    {

        health -= damage;
        if(health <= 0)
        {
            gameObject.tag = "Untagged";
            _rb.freezeRotation = false;
            _rb.isKinematic = false;
            _rb.WakeUp();

        }

        _rb.AddForce(force,ForceMode.VelocityChange);

    }
    public void DestroyEnemy(Vector3 force)
    {
        _rb.freezeRotation = false;
        _rb.isKinematic = false;
        _rb.WakeUp();
        _rb.AddForce(force, ForceMode.VelocityChange);

    }
    private void DestroyIt()
    {   
        gameObject.SetActive(false);
    }
    public void InvokeSetActive() => Invoke(nameof(DestroyIt),3f);
    //public void EnablePhysicsrock(Vector3 force, BulletU bullet)
    //{
    //    health -= bullet.Stats.GetDamage();
    //    if (health <= 0)
    //    {
    //        _rb.freezeRotation = false;
    //        _rb.isKinematic = false;
    //        _rb.WakeUp();

    //    }

    //    _rb.AddForce(force);
    //}
}
