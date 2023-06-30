
using UnityEngine;
using TMPro;
public class UIPeople : MonoBehaviour
{
    [SerializeField] private TMP_Text _Text;
    [SerializeField] private TMP_Text _AllBandits;
    // Start is called before the first frame update
    void Awake()
    {
       // PersonsList.OnPeopleChange += SetKilledPeople;
        
    }
    private void Start()
    {
        //_AllBandits.text = PersonsList.GetAmount().ToString();
    }
    private void SetKilledPeople()
    {
        int count = System.Convert.ToInt32(_Text.text) + 1;
        _Text.text = count.ToString();
    }
}
