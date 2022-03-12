using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocidad= 30.0f;
    [SerializeField] private string eje;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float v = Input.GetAxisRaw(eje);
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, v * velocidad);
        
    }
}
