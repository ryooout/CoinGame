using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UiManager : MonoBehaviour
{
    [SerializeField]
    Text _CoinAmountText = default;
    [SerializeField]
    Text _gameOverText = default;
    [SerializeField]
    string _sceneNeme = default;
    void Start()
    {
        
    }
    void Update()
    {
        _CoinAmountText.text = $"Score:{GameManager.Instance.CoinAmount:0000000000}";
        if(GameManager.Instance.CoinAmount<0&&GameManager.Instance.GameEnd)
        {
            _gameOverText.gameObject.SetActive(true);
            _gameOverText.text = "GameOver";
        }
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
    public void BackToTitle()
    {
        GameManager.Instance.Save();
        SceneManager.LoadScene(_sceneNeme);
    }
}
