
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField, Tooltip("コイン獲得数")]
    private int _coinAmount;
    public int CoinAmount { get => _coinAmount; set => _coinAmount = value; }
    [SerializeField, Tooltip("コインのオブジェクト")]
    GameObject _coinObj = default;
    [SerializeField, Tooltip("判定床のオブジェクト")]
    GameObject _judgeFloor = default;
    [SerializeField, Tooltip("生成個数")]
    int _generateAmount;
    [SerializeField, Tooltip("最小生成個数")]
    int _minGenerateAmountIncrese = 1;
    [SerializeField, Tooltip("最大生成個数")]
    int _maxGenerateAmountincrese = 10;
    [SerializeField]
    float _generateTime = 0;
    bool _isGenerate;
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
        for (int i = 0; i < 200; i++)
        {
            Instantiate(_coinObj, 
                new Vector3(Random.Range(-3.7f, 3.7f), this.transform.position.y, Random.Range(-1.3f, 1.7f)), transform.rotation);
        }
        StartCoroutine(SpawnCoroutine());
    }
    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            _generateTime = Random.Range(5, 10);
            yield return new WaitForSeconds(_generateTime);
            _generateAmount = Random.Range(_minGenerateAmountIncrese, _maxGenerateAmountincrese);
            Generate();
            _isGenerate = true;
        }
    }
    void Generate()
    {
        _judgeFloor.SetActive(true);
        for (int i = 0; i < _generateAmount; i++)
        {
            float x = Random.Range(-4, 4);
            Instantiate(_coinObj, new Vector3(x, transform.position.y, transform.position.z), transform.rotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10.0f;
            if (_isGenerate)
            {
                Instantiate(_coinObj, Camera.main.ScreenToWorldPoint(mousePos), transform.rotation);
                _coinAmount--;
            }
            if(_coinAmount<=0)
            {
                _isGenerate = false;
            }
        }
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
        LocalData.Save<SaveData>(Application.dataPath + "/save.json", save);
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
