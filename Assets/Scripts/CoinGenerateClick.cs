using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class CoinGenerateClick : MonoBehaviour
{
    [SerializeField]
    GameObject _coinObj = default;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            // nGUI上をクリックしているので処理をキャンセルする。
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (GameManager.Instance.CoinAmount > 0)
            {
                Instantiate(_coinObj,transform.position,transform.rotation);
                GameManager.Instance.CoinAmount--;
            }
            else if (GameManager.Instance.CoinAmount < 0)
            {
                GameManager.Instance.CoinAmount = 0;
            }
        }
    }
}
