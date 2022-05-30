using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttonlvl2Controller : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Animator _barrierAnimator;
    private string _nameButton;


    private void Start() {
        _animator = GetComponent<Animator>();
        _nameButton = gameObject.name;
    }

    private void OnPressed() {
        if (_nameButton == "Button1"){
            //_animator.SetBool("isPressed", true);
            _barrierAnimator.SetBool("Down", true); 
        } else {
            //_animator.SetBool("isPressed", true);
            _barrierAnimator.SetBool("Up", true);
        }
    }

    private void OnReleased() {
        if (_nameButton == "Button1") {
            //_animator.SetBool("isPressed", false);
            _barrierAnimator.SetBool("Down", false); 
        } else {
            //_animator.SetBool("isPressed", true);
            //_barrierAnimator.SetBool("Up", true);
        }
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
}
