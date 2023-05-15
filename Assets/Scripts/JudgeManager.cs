using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
    [SerializeField]
    string _coinName = "";
    void Start()
    {

    }
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(_coinName))
        {
            GameManager.Instance.CoinAmount++;
            Debug.Log(GameManager.Instance.CoinAmount++);
            collision.gameObject.SetActive(false);
        }
    }
}
