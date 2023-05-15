using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerateManager : MonoBehaviour
{
    [SerializeField, Tooltip("コインのオブジェクト")]
    GameObject _coinObj = default;
    [SerializeField, Tooltip("星のオブジェクト")]
    GameObject _starObj = default;
    [SerializeField, Tooltip("判定床のオブジェクト")]
    GameObject _judgeFloor = default;
    /// <summary>はじめにメダルを自動生成するオブジェクト</summary>
    GameObject _startGenerateMachine = default;
    [SerializeField, Tooltip("はじめにメダルを生成する個数")]
    int _firstMedalGenerateAmount;
    [SerializeField, Tooltip("生成個数")]
    int _generateAmount;
    [SerializeField, Tooltip("最小生成個数")]
    int _minGenerateAmountIncrese = 1;
    [SerializeField, Tooltip("最大生成個数")]
    int _maxGenerateAmountincrese = 1;
    [SerializeField]
    float _generateTime = 0;
    bool _isGenerate =  true;
    private void Awake()
    {
        _startGenerateMachine = GameObject.Find("GenerateObj1");
    }
    void Start()
    {
        for (int i = 0; i < _firstMedalGenerateAmount; i++)
        {
            Instantiate(_coinObj,
                new Vector3(Random.Range(-3.7f, 3.7f), 1.5f, Random.Range(-1.3f, 1.7f)), transform.rotation);
        }
        StartCoroutine(SpawnCoroutine());
    }
    IEnumerator SpawnCoroutine()
    {
        while (_isGenerate)
        {
            _generateTime = Random.Range(5, 10);
            yield return new WaitForSeconds(_generateTime);
            _startGenerateMachine.SetActive(false);
            _generateAmount = Random.Range(_minGenerateAmountIncrese, _maxGenerateAmountincrese);
            Generate();
        }
    }
    void Generate()
    {
        _judgeFloor.SetActive(true);
        for (int i = 0; i < _generateAmount; i++)
        {
            int rand = Random.Range(1, 10);
            if (rand > 8)
            {
                float x = Random.Range(-4, 4);
                Instantiate(_starObj, new Vector3(x, transform.position.y, transform.position.z), transform.rotation);
            }
            else
            {
                float x = Random.Range(-4, 4);
                Instantiate(_coinObj, new Vector3(x, transform.position.y, transform.position.z), transform.rotation);
            }
        }
    }
    void Update()
    {
        if (GameManager.Instance.CoinAmount < 0)
        {
            _isGenerate = false;
            GameManager.Instance.GameEnd = true;
        }
    }
}
