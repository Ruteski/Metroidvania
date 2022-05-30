using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _barrierAnimator;
    //[SerializeField] private LayerMask layer;

    private void Start() {
        _animator = GetComponent<Animator>();   
    }

    private void OnPressed() {
        _animator.SetBool("isPressed", true);
        _barrierAnimator.SetBool("Down", true);
    }

    private void OnReleased() {
        _animator.SetBool("isPressed", false);
        _barrierAnimator.SetBool("Down", false);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Stone") || collision.gameObject.CompareTag("Player")) {
            OnPressed();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Stone") || collision.gameObject.CompareTag("Player")) {
            OnReleased();
        }
    }



    //private void OnCollision() {
    //    //objeto que estou colidindo 
    //    Collider2D hit = Physics2D.OverlapCircle(transform.position, 1, layer);// layer da colisao()
    //    // cria um colider de circulo na posicao do point

    //    if (hit != null) {
    //        OnPressed();
    //        hit = null;
    //    } else {
    //        OnReleased();
    //    }

    //}

    ////esse metodo aparece 100% do tempo - OnDrawGizmos
    //private void OnDrawGizmos() {
    //    //Debug.DrawRay(transform.position, _direction * 1, Color.red);
    //    Gizmos.DrawWireSphere(transform.position, 1);
    //}
}
