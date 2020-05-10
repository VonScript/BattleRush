using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UnityAtoms.BaseAtoms;

public class PlayerControl : MonoBehaviour
{
    public float speed = 5.0f;
    public float rush = 10.0f;
    public float strafe = 20.0f;

    public BoolVariable rush_activated;
    public BoolVariable has_jumped;

    public bool strafed_left = false;
    public bool strafed_right = false;

    private Animator _animator;
    private float _position = 0f;
    private float _distance = 2.0f;
    private Vector3 _target = Vector3.zero;
    private Quaternion _rotation;

    private void Awake() {
        _rotation = GetComponentInChildren<Rigidbody>().rotation;
    }

    void Start(){
        _animator = GetComponent<Animator>();
    }

    void FixedUpdate(){
        if (!rush_activated.Value) {
            EndRush();
            transform.position += Vector3.forward * speed * Time.deltaTime;
        } else {
            transform.position += Vector3.forward * rush * Time.deltaTime;
        }
        

        if (strafed_left) {
            _target.x = _position - _distance;
            _target.y = 0;
            _target.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, _target, strafe * Time.deltaTime);
            if (transform.position.x <= (_position + -_distance)) strafed_left = false;
        }

        if (strafed_right) { 
            _target.x = _position + _distance;
            _target.y = 0;
            _target.z = transform.position.z;

            transform.position = Vector3.MoveTowards(transform.position, _target, strafe * Time.deltaTime);
            if (transform.position.x >= (_position + _distance)) strafed_right = false;
        }

    }

    private void Update(){
        if (Input.GetButtonDown("Jump") && !has_jumped.Value) {
            _animator.SetTrigger("Jump");
            has_jumped.Value = true;
        }

        if (Input.GetButtonDown("Horizontal") && !has_jumped.Value) {
            float left_right = Input.GetAxisRaw("Horizontal");

            if (left_right < 0) {
                //_animator.SetTrigger("Left");
                strafed_left = true;
                _position = Mathf.Round(transform.position.x);
            } else {
                //_animator.SetTrigger("Right");
                strafed_right = true;
                _position = Mathf.Round(transform.position.x);
            }
        }
    }

    public void StopJumping() { has_jumped.Value = false; GetComponentInChildren<Rigidbody>().MoveRotation(_rotation);  }

    public void StartRush() { _animator.SetBool("Idle", false);  _animator.SetBool("Rush", true); }

    private void EndRush() { rush_activated.Value = false; _animator.SetBool("Rush", false); _animator.SetBool("Idle", true); }
}
