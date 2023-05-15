
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField, Tooltip("コイン獲得数")]
    private int _coinAmount = 100;
    public int CoinAmount { get => _coinAmount; set => _coinAmount = value; }
    bool _gameEnd = false;
    public bool GameEnd { get => _gameEnd; set => _gameEnd = value; }
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start()
    {
        Load();
    }
    
    // Update is called once per frame
    void Update()
    {
    }
    public void Load()
    {
        //デバッグ時に楽なのでdataPathにしてるけど、persistentDataPathが適切
        var save = LocalData.Load<SaveData>(Application.dataPath + "/save.json");
        if (save == null)
        {
            save = new SaveData();
        }

        _coinAmount = save.TotalAmount;
    }

    public void Save()
    {
        SaveData save = new SaveData();

        save.TotalAmount = _coinAmount;
        LocalData.Save(Application.dataPath + "/save.json", save);
        Debug.Log(save.TotalAmount);
    }
    public void ResetNum()
    {
        SaveData save = new SaveData();
        _coinAmount = 0;
        save.TotalAmount = _coinAmount;
        Debug.Log(_coinAmount);
        Debug.Log(save.TotalAmount);
    }
}
