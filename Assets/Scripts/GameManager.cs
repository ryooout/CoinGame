
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField, Tooltip("�R�C���l����")]
    private int _coinAmount;
    public int CoinAmount { get => _coinAmount; set => _coinAmount = value; }
    [SerializeField, Tooltip("�R�C���̃I�u�W�F�N�g")]
    GameObject _coinObj = default;
    [SerializeField, Tooltip("������")]
    int _generateAmount;
    [SerializeField, Tooltip("�ŏ�������")]
    int _minGenerateAmountIncrese = 1;
    [SerializeField, Tooltip("�ő吶����")]
    int _maxGenerateAmountincrese = 10;
    [SerializeField]
    float _generateTime = 0;
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
                new Vector3(Random.Range(-3.7f, 3.7f), this.transform.position.y, Random.Range(-1.5f, 1.7f)), transform.rotation);
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
        }
    }
    void Generate()
    {
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
            _coinAmount++;
        }
    }
    public void Load()
    {
        //�f�o�b�O���Ɋy�Ȃ̂�dataPath�ɂ��Ă邯�ǁApersistentDataPath���K��
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
