using UnityEngine;
using TMPro;
public class UICoin : MonoBehaviour
{
    [SerializeField] private TMP_Text _Coin;
    // Start is called before the first frame update
    private void Awake()
    {
        //PersonsList.OnPeopleChange += PlusCoin;
    }

    private void PlusCoin()
    {
        
        int TotalCoin = System.Convert.ToInt32(_Coin.text) +1;
        if(TotalCoin == 100)
        {
            _Coin.characterWidthAdjustment += 100f;
        }
        if(TotalCoin == 1000)
        {
            _Coin.characterWidthAdjustment += 100f;
        }
        _Coin.text = TotalCoin.ToString();
    }
}
