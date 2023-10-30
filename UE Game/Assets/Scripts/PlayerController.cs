using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public int velocidade = 10;
    public int forcaPulo = 7;
    private Rigidbody _rb;
    public bool noChao;
    private Transform cameraPivot;
    private Animator _animator;

    private bool _isMoving;
    void Start()
    {
        TryGetComponent(out _animator);
        TryGetComponent(out _rb);
        cameraPivot = Camera.main.transform;
    }
    void OnCollisionEnter(Collision col) {
        noChao = true;
    }
    void Update() {
       Movimento();
       Pulo();
       AtualizarAnimator();
       VerificarReinicio();
    }

    void Movimento()
    {
        float h = Input.GetAxis("Horizontal"); //-1 esquerda,0 nada, 1 direita
        float v = Input.GetAxis("Vertical");// -1 pra tras, 0 nada, 1 pra frente
        
        Vector3 direcao = (cameraPivot.forward * v + cameraPivot.right * h) * velocidade;
        direcao = new Vector3(direcao.x, _rb.velocity.y, direcao.z);
        _rb.velocity = Vector3.Lerp(_rb.velocity, direcao, 5 * Time.deltaTime);

        if (noChao && _rb.velocity != Vector3.zero)
        {
            _isMoving = true;
        }
        else if(noChao && _rb.velocity == Vector3.zero)
        {
            _isMoving = false;
        }

        if (_isMoving)
        {

            float angle = InputToAngle(v, h);// + cameraPivot.eulerAngles.y;
            Debug.Log(angle);
            Vector3 cameraEuler = new Vector3(0,cameraPivot.eulerAngles.y, 0);
            Vector3 endEuler = Vector3.Lerp(transform.eulerAngles,cameraEuler, 5 * Time.deltaTime);
            transform.rotation = Quaternion.Euler(endEuler); //Quaternion.Lerp(transform.rotation ,Quaternion.Euler(cameraEuler), 5 * Time.deltaTime );
        }
    }
    void Pulo()
    {
        if (Input.GetKeyDown(KeyCode.Space) && noChao ) {
            _rb.AddForce(Vector3.up * forcaPulo, ForceMode.Impulse); //pulo
            noChao = false;
        }
    }

    void AtualizarAnimator()
    {
        _animator.SetBool("IsMoving",_isMoving);
    }

    void VerificarReinicio()
    {
        if(transform.position.y <= -10) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    float InputToAngle(float x, float y)
    {
        Vector2 vec = new Vector2(-x, y).normalized;
        return Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg - 90;
    }
}