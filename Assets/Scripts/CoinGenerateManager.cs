using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerateManager : MonoBehaviour
{
    [SerializeField]
    GameObject _coinObj = default;
    [SerializeField]
    int _generateAmount = 10;
    [SerializeField]
    int _generateAmountincrese = 1;
    [SerializeField]
    float _generateTime = 0;
    void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }
    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            _generateTime = Random.Range(5, 10);
            yield return new WaitForSeconds(_generateTime);
            _generateAmount = Random.Range(1, _generateAmountincrese);
            Generate();
        }
    }
    void Generate()
    {
        for (int i = 0; i < _generateAmount; i++)
        {
            float x = Random.Range(-4, 4);
            Instantiate(_coinObj,new Vector3(x,transform.position.y,transform.position.z),transform.rotation);
        }
    }
}
