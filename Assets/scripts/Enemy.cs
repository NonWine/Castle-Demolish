using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject canvas, _headRotation;
    [SerializeField] private float speedRotation;
    [SerializeField] private Image hpfill, WhiteFill;
    [SerializeField] private PersonBullet _bullet;
    [SerializeField] private MachineType _myType;
    [SerializeField] private Transform rotatedY;
    [SerializeField] private Transform rotatedX;
    [SerializeField] private Rigidbody[] _roots;
    [SerializeField] private Transform _bulletSpawnPosition;
    [SerializeField] private GameObject phisycsArbalet;
    [SerializeField] private GameObject animArbalet;
    [SerializeField] private GameObject strela;
    public float t;
    //[SerializeField] private Animator anim;

    private Coroutine _canvasCd;
    private Transform _target;
    private float _AmountPart = 0f, timer, SlowLerp = 3f;
    public float elapsedTime;
    private bool Noticed { get; set; }
    private bool isDeath;

    private float timerStrela;

    private void Start()
    {
        _target = Camera.main.transform;
        Noticed = true;
        canvas.SetActive(false);
    }

    private void Update()
    {
        
        if (!isDeath)
        {
            Vector3 dir = (_target.position + Vector3.down * 5) - _headRotation.transform.position;
            Quaternion rot = Quaternion.Slerp(_headRotation.transform.rotation, Quaternion.LookRotation(dir), speedRotation * Time.deltaTime);

            if (_myType == MachineType.Arbalet)
            {
                Quaternion y = rot;
                y.z = 0;
                y.x = 0;
                rotatedY.rotation = y;
            }
            if (_myType == MachineType.Canon)
            {
                _headRotation.transform.rotation = rot;
            }
        }

        if (Noticed)
        {
            elapsedTime += Time.fixedDeltaTime;
            if (elapsedTime >= _bullet.GetCooldown())
            {
                    ParticlePool.Instance.PlayCanonShoot(_headRotation.transform.GetChild(0).transform.position);
                PersonBullet Prefab = Instantiate(_bullet, _bulletSpawnPosition.position, Quaternion.identity);
                elapsedTime = 0;
          
                Prefab._endPos = _target.position;
               // Destroy(Prefab, 5f);
            }

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Bullet"))
        {
            if (_canvasCd != null)
                StopCoroutine(_canvasCd);
            _canvasCd = StartCoroutine(CanvasCD());
            Debug.Log(timer);
            BulletU bull = other.gameObject.GetComponent<BulletU>();
            StartCoroutine(ReducHp(bull.Stats.GetDamage()));
        }
    }

    private IEnumerator ReducHp(float count)
    {
        float speed = 0;
        _AmountPart = count / 1000f;
        hpfill.fillAmount -= _AmountPart;
        if (hpfill.fillAmount <= 0)
        {
            if(_myType == MachineType.Arbalet)
            {
                animArbalet.SetActive(false);
                phisycsArbalet.SetActive(true);
            }
            PeronsList.Instance.RemoveInList();
            canvas.SetActive(false);
           // Destroy(GetComponent<BoxCollider>());
            GetComponent<BoxCollider>().enabled = false;
            foreach (var item in _roots)
            {
                item.gameObject.GetComponent<Outline>().enabled = false;
                item.freezeRotation = false;
                item.isKinematic = false;
                item.WakeUp();
                item.AddForce(new Vector3(Random.Range(-2, 2), Random.Range(0, 5), Random.Range(-2, 2)), ForceMode.VelocityChange);
            }
         //   Destroy(GetComponent<Enemy>());
            GetComponent<Enemy>().enabled = false;
        }

        while (WhiteFill.fillAmount != hpfill.fillAmount)
        {
            speed += Time.deltaTime;
            WhiteFill.fillAmount = Mathf.Lerp(WhiteFill.fillAmount, hpfill.fillAmount, speed / SlowLerp);
            yield return null;
        }


    }

    private IEnumerator CanvasCD()
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(3f);
        canvas.SetActive(false);
    }
    private IEnumerator StrelaCd(int t)
    {
        yield return new WaitForSeconds(t);
        strela.SetActive(false);
    }
    public void SetNotice(bool flag) => Noticed = flag;

    public enum MachineType
    {
        Canon = 0,
        Arbalet = 1
    }
}