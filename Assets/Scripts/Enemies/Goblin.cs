using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _maxVision = 5f;
    [SerializeField] private bool _isFront = false;
    [SerializeField] private Transform _point;

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
        GetPlayer();
    }

    private void GetPlayer() {               //origem,        direcaov     , distancia
        RaycastHit2D hit = Physics2D.Raycast(_point.position, Vector2.right, _maxVision);

        if (hit.collider != null) {
            if (hit.transform.CompareTag("Player")) {
                print("estou vendo o player");
            }
        }
    }

    //esse metodo aparece 100% do tempo - OnDrawGizmos
    private void OnDrawGizmos() {
        //Gizmos.DrawRay(_point.position, Vector2.right * _maxVision);
        //Debug.DrawRay(_point.position, Vector2.right * _maxVision, Color.red);
    }

    //esse metodo só aparece os gismos quando eu seleciono o objeto que tenha gismos para aparecer
    private void OnDrawGizmosSelected() {
        //Gizmos.DrawRay(_point.position, Vector2.right * _maxVision);
        Debug.DrawRay(_point.position, Vector2.right * _maxVision, Color.red);
    }
}
