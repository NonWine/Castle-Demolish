using UnityEngine;
public class Person : MonoBehaviour
{
    [SerializeField] private float _Health;
    [SerializeField] private PersonBullet BulletPrefab;
    [SerializeField] private SkinnedMeshRenderer _renrer;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody[] _roots;
    [SerializeField] private Rigidbody spine;
    [SerializeField] private bool isDanger = true;
    private Transform _target;
    private SkinnedMeshRenderer ColorChanger;
    private float CoolDown, elapsedTime;
    private Animator _AmimController;
    private ParticleSystem _ps;
    private Quaternion startRotate;

    public bool Noticed { get; set; }

    public bool Alive { get; private set; }

    private void Start()
    {
        elapsedTime += Random.Range(-1.1f, 0.1f);
        foreach (var item in _roots)
        {
            item.isKinematic = true;
        }
        Alive = true;
        _ps = GetComponentInChildren<ParticleSystem>();
        _AmimController = GetComponentInChildren<Animator>();
        ColorChanger = GetComponentInChildren<SkinnedMeshRenderer>();
        _target = Camera.main.transform;
        CoolDown = 1.5f;
        startRotate = transform.rotation;
        Invoke(nameof(StartCollider),1f);
    }

    private void Update()
    {
        if (Alive && isDanger)//Notice
        {
            Vector3 dir = _target.position - transform.position;
            Quaternion rot = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), speed * Time.deltaTime);
            rot.x = 0;
            rot.z = 0;
            transform.rotation = rot;
        }
        else
            ResetRotation();

        if ( Alive && isDanger) //Notice
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= BulletPrefab.GetCooldown())
            {
                _AmimController.SetTrigger("shoot");
                elapsedTime = 0;


                PersonBullet Prefab = Instantiate(BulletPrefab, transform.position, Quaternion.identity);
                Prefab._endPos = _target.position ;
                //Destroy(Prefab, 5f);
            }
        }

    }

    public void TakeDamage(Vector3 bullet, float force)
    {
        ColorChanger.material.color = new Color32(192,180,183,255);
        ColorChanger.material.SetFloat("_Metallic", 0.3f);
        gameObject.GetComponentInChildren<Outline>().enabled = false;
        if (Alive)
        {
            Alive = false;
            _AmimController.enabled = false;
            _ps.Play();
            foreach (var item in _roots)
            {
                item.isKinematic = false;
                item.WakeUp();
                
            }
            //spine.AddForce( Random.insideUnitSphere * force,ForceMode.VelocityChange);
            spine.AddForce( Random.insideUnitSphere * 10,ForceMode.VelocityChange);
            transform.parent.GetComponent<PeronsList>().RemoveInList();
        }
    }

    private void ResetRotation()
    {
        if(Alive && isDanger)
        if (transform.rotation.y > startRotate.y)
            transform.Rotate(Vector3.up * -speed);
    }
    private void StartCollider() => GetComponent<BoxCollider>().enabled = true;
}
