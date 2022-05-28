using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D _rb;
    [SerializeField] private float _speed = 5f;

    // Start is called before the first frame update
    void Start()    
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move(){
        float movement = Input.GetAxis("Horizontal");

        _rb.velocity = new Vector2(movement * _speed, _rb.velocity.y);

        if (movement > 0) {
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (movement < 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
        
}
