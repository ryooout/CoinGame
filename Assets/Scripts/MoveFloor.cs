using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [SerializeField, Tooltip("è∞ÇÃìÆÇ≠ïù")]
    private float _moveLength = 1.0F;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3
        (transform.position.x, transform.position.y, -Mathf.Sin(Time.time) * _moveLength);
    }
}
