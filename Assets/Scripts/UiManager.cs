using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UiManager : MonoBehaviour
{
    [SerializeField]
    Text _CoinAmountText = default;
    void Start()
    {
        
    }
    void Update()
    {
        _CoinAmountText.text = $"Score:{GameManager.Instance.CoinAmount:0000000000}";
    }
    public void Load()
    {
        GameManager.Instance.Load();
    }
    public void Save()
    {
        GameManager.Instance.Save();
    }
    public void ResetNum()
    {
        GameManager.Instance.ResetNum();
    }
}
