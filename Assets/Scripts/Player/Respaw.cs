using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respaw : MonoBehaviour
{
    private Transform _player;

    public static Respaw instance;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;

        if (_player) {
            CheckPoint();
        }
    }

    public void CheckPoint() {
        Vector3 pos = transform.position;
        pos.z = 0f;
        _player.position = pos;
    }
}
