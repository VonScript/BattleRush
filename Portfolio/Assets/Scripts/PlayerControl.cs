using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UnityAtoms.BaseAtoms;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10.0f;
    public float rush = 30.0f;
    public float strafe = 20.0f;

    public bool strafed_left = false;
    public bool strafed_right = false;

    private Animator _animator;
    private float _position = 0f;
    private float _distance = 2.0f;
    private Vector3 _target = Vector3.zero;

    private PlayerAtoms _atoms;
    private bool _airborne = false;

    private void Awake() {
        _animator = GetComponent<Animator>();
    }

    private void Start(){
        _atoms = GetComponentInChildren<PlayerAtoms>();
    }

    private void FixedUpdate(){
        if (!_atoms.rush_activated.Value) {
            EndRush();
            transform.position += Vector3.forward * speed * Time.deltaTime;
        } else {
            transform.position += Vector3.forward * rush * Time.deltaTime;
        }
        

        if (strafed_left) {
            if (transform.position.x != -_distance) {
                _target.x = _position - _distance;
                _target.y = 0;
                _target.z = transform.position.z;

                transform.position = Vector3.MoveTowards(transform.position, _target, strafe * Time.deltaTime);
                if (transform.position.x <= (_position + -_distance)) strafed_left = false;
            } else strafed_left = false;
        }

        if (strafed_right) {
            if (transform.position.x != _distance) {
                _target.x = _position + _distance;
                _target.y = 0;
                _target.z = transform.position.z;

                transform.position = Vector3.MoveTowards(transform.position, _target, strafe * Time.deltaTime);
                if (transform.position.x >= (_position + _distance)) strafed_right = false;
            } else strafed_right = false;
        }

    }

    private void Update(){
        if (Input.GetButtonDown("Jump") && !_airborne) {
            if (!_atoms.rush_activated.Value && !_atoms.ult_activated.Value) {
                _animator.SetTrigger("Jump");
                _airborne = true;
            }
        }

        if (Input.GetButtonDown("Horizontal") && !_atoms.has_jumped.Value) {
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

        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            if (!_atoms.rush_activated.Value) {
                _animator.SetTrigger("Start_Rush");
            }
        }

    }
    public void Airborne() => _atoms.has_jumped.Value = true;

    public void StopJumping() { _atoms.has_jumped.Value = false; _airborne = false; }

    public void StartRush() { _animator.SetBool("Idle", false);  _animator.SetBool("Rush", true); }

    private void EndRush() { _atoms.rush_activated.Value = false; _animator.SetBool("Rush", false); _animator.SetBool("Idle", true); }

    public void StartUlt() { _animator.SetBool("Idle", false); _animator.SetBool("Rush", true); }
}
