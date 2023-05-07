using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCoinGenerate : MonoBehaviour
{
    [SerializeField]
    float _moveLength;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Cos(Time.time) * _moveLength,
            transform.position.y, transform.position.z);
    }
}
