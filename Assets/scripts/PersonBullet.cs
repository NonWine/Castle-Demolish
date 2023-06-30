using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class PersonBullet : MonoBehaviour
{
    [SerializeField] private float _SpeedLerp , t, _DamageToPlayer;
    [SerializeField] private float _cooldown, _radius;
    [SerializeField] private int _damageToCastle;
    private Rigidbody myBody;
    private Vector3 _startPos;
    public Vector3 _endPos { get; set; }
    void Start()
    {
        //StartCoroutine(GiveDamage());
        myBody = GetComponent<Rigidbody>();
        _startPos = transform.position;
        transform.LookAt(_endPos, Vector3.forward);
        myBody.velocity = (_endPos - _startPos).normalized * _SpeedLerp;

    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _endPos) < 1f)
        {
            if(!GameManager.Instance.GetFinish())
                TotalHealht.AmountDestroyed(_DamageToPlayer);
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Castle"))
        {
            StopAllCoroutines();
            Explode(new Vector3(Random.Range(-2, 2), Random.Range(5, 10), Random.Range(-2, 2)));
            gameObject.SetActive(false);
        }
    }
    private IEnumerator GiveDamage()
    {
        yield return new WaitForSeconds(t);
        if(!GameManager.Instance.GetFinish())
        TotalHealht.AmountDestroyed(_DamageToPlayer);
        gameObject.SetActive(false);
    }
    public float GetCooldown()
    {
        return _cooldown;
    }
    public void Explode(Vector3 force)
    {
        // if(_isDestroyBody)
        //    Destroy(myBody);
        Debug.Log("asdas");
        Collider[] OverLapColliders = Physics.OverlapSphere(transform.position, _radius);
        for (int i = 0; i < OverLapColliders.Length; i++)
        {
            Rigidbody rb = OverLapColliders[i].attachedRigidbody;
            if (rb && OverLapColliders[i].CompareTag("Castle") || OverLapColliders[i].CompareTag("PartsBullet"))
            {
                SupportGravity obj = OverLapColliders[i].GetComponent<SupportGravity>();
                obj.EnablePhysics(force, _damageToCastle);
                obj.InvokeSetActive();
        //        Destroy(OverLapColliders[i].gameObject, 3f);
            }
        }
        // Destroy(gameObject, 2f);
    }
}

