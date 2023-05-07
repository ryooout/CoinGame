using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerateManager : MonoBehaviour
{
    [SerializeField, Tooltip("�R�C���̃I�u�W�F�N�g")]
    GameObject _coinObj = default;
    [SerializeField, Tooltip("���菰�̃I�u�W�F�N�g")]
    GameObject _judgeFloor = default;
    [SerializeField, Tooltip("������")]
    int _generateAmount;
    [SerializeField, Tooltip("�ŏ�������")]
    int _minGenerateAmountIncrese = 1;
    [SerializeField, Tooltip("�ő吶����")]
    int _maxGenerateAmountincrese = 10;
    [SerializeField]
    float _generateTime = 0;
    void Start()
    {
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
    void Update()
    {
    }
}
