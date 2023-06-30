using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float _SpeedLeerp;
    [SerializeField] private float Radius;
    [SerializeField] private float _Force;
    [SerializeField] private float _Damage;
    [SerializeField] private int _CoolDown;
    private float _speed; 
    private Vector3 _startPos;
    public Vector3 _endPos { get; set; }
    private bool Move = true;
    void Start()
    {
     
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      
        if (Move) 
        {
            _speed += Time.deltaTime;
            gameObject.transform.position = Vector3.Lerp(_startPos, _endPos, _speed * _SpeedLeerp);
            if (Vector3.Distance(_endPos,transform.position) < 0.1f) { _speed = 0; Move = false; Debug.Log(gameObject.name + "12321"); }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Bullet")
        {
            Explode();
            Move = false;
            Debug.Log(gameObject.name);
        }
      
    }
    public void Explode()
    {
        Collider[] OverLapColliders = Physics.OverlapSphere(transform.position, Radius);
        for (int i = 0; i < OverLapColliders.Length; i++)
        {
            Rigidbody RB = OverLapColliders[i].attachedRigidbody;
            if (RB)
            {
                RB.AddExplosionForce(_Force, transform.position, Radius);
            //    Destroy(OverLapColliders[i].gameObject,2f);
            }
        }
      
        Destroy(gameObject, 1.5f);
    }
  
    public int GetCoolDown() { return _CoolDown; }

    public float GetDamage() { return _Damage; }
}
