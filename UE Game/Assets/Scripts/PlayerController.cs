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
    public int velocidadeOriginal = 10;
    public int velocidadeAtual;
    public int forcaPuloOriginal = 7;
    public int forcaPuloAtual = 7;
    private Rigidbody _rb;
    public bool noChao;
    private Transform cameraPivot;
    private Animator _animator;

    private float toleranciaMovimento = 0.1f;

    private bool _isMoving;
    void Start()
    {
        TryGetComponent(out _animator);
        TryGetComponent(out _rb);
        cameraPivot = Camera.main.transform;
        velocidadeAtual = velocidadeOriginal;
        forcaPuloAtual = forcaPuloOriginal;
    }
    void OnCollisionEnter(Collision col){
        
        if (col.gameObject.layer == LayerMask.NameToLayer("Cenario"))
        {
            noChao = true;
        }
        
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
        
        Vector3 direcao = (cameraPivot.forward * v + cameraPivot.right * h) * velocidadeAtual;
        direcao = new Vector3(direcao.x, _rb.velocity.y, direcao.z);
        _rb.velocity = Vector3.Lerp(_rb.velocity, direcao, 5 * Time.deltaTime);

        _isMoving = direcao.magnitude > toleranciaMovimento;

        if (noChao && _isMoving)
        {
            Vector3 cameraEuler = new Vector3(0,cameraPivot.eulerAngles.y, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation ,Quaternion.Euler(cameraEuler), 5 * Time.deltaTime );
        }
    }
    void Pulo()
    {
        if (Input.GetKeyDown(KeyCode.Space) && noChao ) {
            _rb.AddForce(Vector3.up * forcaPuloAtual, ForceMode.Impulse); //pulo
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

    public void SetVelocidade(int valor)
    {
        velocidadeAtual = valor;
    }
    
    public void ResetVelocidade()
    {
        velocidadeAtual = velocidadeOriginal;
    }
    public void SetForcaPulo(int valor)
    {
        forcaPuloAtual = valor;
    }
    
    public void ResetForcaPulo()
    {
        forcaPuloAtual = forcaPuloOriginal;
    }
    
    
}