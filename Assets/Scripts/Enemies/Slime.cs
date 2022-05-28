using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    [SerializeField] private float _speed = 1.5f;
    [SerializeField] private Transform _point;
    [SerializeField] private LayerMask layer;// me permite selecionar uma layer no inpector
    [SerializeField] private float _pointRadius;

    private Rigidbody2D _rb;
    

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
        _rb.velocity = new Vector2(_speed *-1, _rb.velocity.y);
        OnCollision();
    }

    private void OnCollision() {
        //objeto que estou colidindo 
        Collider2D hit = Physics2D.OverlapCircle(_point.position, _pointRadius, layer);// layer da colisao()
        // cria um colider de circulo na posicao do point

        if (hit != null) {
            // so eh chamado quando o inimigo bate em um objeto que tenha a layer selecionada
            _speed = - _speed;

            transform.eulerAngles = transform.eulerAngles.y == 0 ? 
                transform.eulerAngles = new Vector3(0, 180, 0) : 
                transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(_point.position, _pointRadius);
    }
}
