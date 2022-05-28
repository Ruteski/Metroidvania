using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 10f;
    [SerializeField] private Animator _anim;
    [SerializeField] private Transform _point;
    [SerializeField] private float _pointRadius;

    private Rigidbody2D _rb;
    private bool _isJumping = false;
    private bool _doubleJump = false;
    private bool _isAttacking = false;

    // Start is called before the first frame update
    void Start()    
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Attack();
    }

    private void FixedUpdate() {
        Move();
    }

    private void Move(){
        float movement = Input.GetAxis("Horizontal");

        _rb.velocity = new Vector2(movement * _speed, _rb.velocity.y);

        if (movement > 0) {
            if (!_isJumping) {
                _anim.SetInteger("Transition", 1);
            }
            
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (movement < 0) {
            if (!_isJumping) {
                _anim.SetInteger("Transition", 1);
            }
            
            transform.eulerAngles = new Vector3(0, 180, 0);
        } 
        
        
        if (movement == 0 && !_isJumping && !_isAttacking) {
            _anim.SetInteger("Transition", 0);
        }
    }

    private void Jump() {
        if (Input.GetButtonDown("Jump")) {
            if (!_isJumping) {
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

                _anim.SetInteger("Transition", 2);
                _isJumping = true;
                _doubleJump = true;
            } else if (_doubleJump) {
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

                _anim.SetInteger("Transition", 2);
                _doubleJump = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6) {
            _isJumping = false;
            
        }
    }

    private void Attack() {
        if (Input.GetButtonDown("Fire1")) {
            _isAttacking = true;
            _anim.SetInteger("Transition", 3);

            //objeto que estou colidindo 
            Collider2D hit = Physics2D.OverlapCircle(_point.position, _pointRadius);
                                      // cria um colider de circulo na posicao do point

            if (hit != null) {
                print(hit.name);
            }

            StartCoroutine(OnAttack());
        }
    }

    IEnumerator OnAttack() {
        yield return new WaitForSeconds(0.333f);
        _isAttacking = false;

    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(_point.position, _pointRadius);
    }
}
