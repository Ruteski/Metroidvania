using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respaw : MonoBehaviour
{

    private Transform _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if (_player) {
            Vector3 pos = transform.position;
            pos.z = 0f;
            _player.position = pos; 
        }
    }

}
