using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _maxVision = 5f;
    [SerializeField] private bool _isFront = false;
    [SerializeField] private Transform _point;
    [SerializeField] private bool _isRight = true;
    [SerializeField] private float _stopDistance;

    private Rigidbody2D _rb;
    private Vector2 _direction;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        if (_isRight) {//vira pra direita
            transform.eulerAngles = new Vector2(0, 0);
            _direction = Vector2.right;
        } else {//vira pra esquerda
            transform.eulerAngles = new Vector2(0, 180);
            _direction = Vector2.left;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        GetPlayer();
        OnMove();
    }

    private void OnMove() {
        if (_isFront){ 
            if (_isRight) {//vira pra direita
                transform.eulerAngles = new Vector2(0, 0);
                _direction = Vector2.right;
                _rb.velocity = new Vector2(_speed, _rb.velocity.y);
            } else {//vira pra esquerda
                transform.eulerAngles = new Vector2(0, 180);
                _direction = Vector2.left;
                _rb.velocity = new Vector2(-_speed, _rb.velocity.y);
            }
        }
    }

    private void GetPlayer() {               //origem,        direcaov     , distancia
        RaycastHit2D hit = Physics2D.Raycast(_point.position, _direction, _maxVision);

        if (hit.collider != null) {
            if (hit.transform.CompareTag("Player")) {
                _isFront = true;

                float distance = Vector2.Distance(transform.position, hit.transform.position);

                //distancia para atacar
                if (distance <= _stopDistance) {
                    _isFront = false;
                    _rb.velocity = Vector2.zero;

                    hit.transform.GetComponent<Player>().OnHit();
                }
            }
        }
    }

    //esse metodo aparece 100% do tempo - OnDrawGizmos
    private void OnDrawGizmos() {
        //Gizmos.DrawRay(_point.position, _direction * _maxVision);
        Debug.DrawRay(_point.position, _direction * _maxVision, Color.red);
    }

    //esse metodo s� aparece os gismos quando eu seleciono o objeto que tenha gismos para aparecer
    private void OnDrawGizmosSelected() {
        //Gizmos.DrawRay(_point.position, _direction * _maxVision);
        //Debug.DrawRay(_point.position, _direction * _maxVision, Color.red);
    }
}
