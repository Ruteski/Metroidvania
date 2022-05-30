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
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Health _healthSystem;

    private Rigidbody2D _rb;

    private bool _isJumping = false;
    private bool _doubleJump = false;
    private bool _isAttacking = false;
    private float _recoveryCount = 0;
    private Audio _playerAudio;

    private static Player instance;

    private void Awake() {
        if (instance == null) {//checa se ja existe um outro player na cena
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (instance != this) {
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()    
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAudio = GetComponent<Audio>();
        _healthSystem = GetComponent<Health>();
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
            if (!_isJumping && !_isAttacking) {
                _anim.SetInteger("Transition", 1);
            }
            
            transform.eulerAngles = new Vector3(0, 0, 0);
        } else if (movement < 0) {
            if (!_isJumping && !_isAttacking) {
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
                _playerAudio.PlaySFX(_playerAudio.jumSound);
            } else if (_doubleJump) {
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

                _anim.SetInteger("Transition", 2);
                _doubleJump = false;
                _playerAudio.PlaySFX(_playerAudio.jumSound);

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
            _playerAudio.PlaySFX(_playerAudio.hitSound);
            _isAttacking = true;
            _anim.SetInteger("Transition", 3);

            //objeto que estou colidindo 
            Collider2D hit = Physics2D.OverlapCircle(_point.position, _pointRadius, _enemyLayer);//so vai dar dano se for um inimigo
                                      // cria um colider de circulo na posicao do point

            if (hit != null) {
                if (hit.GetComponent<Slime>()) {
                    hit.GetComponent<Slime>().OnHit();
                }else if (hit.GetComponent<Goblin>()) {
                    hit.GetComponent<Goblin>().OnHit();
                }
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

    public void OnHit() {
        _recoveryCount += Time.deltaTime;

        if (_recoveryCount > 2f) {
            _anim.SetTrigger("Hit");
            _healthSystem.health--;
            _recoveryCount = 0;
        }

        if (_healthSystem.health <= 0) {
            _speed = 0;
            _anim.SetTrigger("Death");
            Destroy(gameObject, 0.7f);

            GameManager.instance.ShowGameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 7) {
            OnHit();
        }

        if (collision.CompareTag("Coin")) {
            _playerAudio.PlaySFX(_playerAudio.coinSound);
            collision.GetComponent<Animator>().SetTrigger("Pickup");
            GameManager.instance.GetCoin();
            Destroy(collision.gameObject, 0.360f);
        }

        if (collision.CompareTag("Door")) {
            GameManager.instance.NextLvl();
        }

        if (collision.CompareTag("CheckPoint")) {
            Respaw.instance.CheckPoint();
        }
    }


}
