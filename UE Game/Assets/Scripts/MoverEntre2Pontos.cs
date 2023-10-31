using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEntre2Pontos : MonoBehaviour
{
    private Vector3 _origem;
    public Vector3 offset;
    public bool ligado;
    private bool _chegou;
    public bool iniciarNoOffset = false;
    public float velocidade = 5;
    private float _tolerancia = 0.01f;

    private void Start()
    {
        _origem = transform.position;

        if (iniciarNoOffset)
        {
            transform.position = _origem + offset;
        }

        _chegou = true;
    }

    private void Update()
    {
        if (_chegou) return;
        if (ligado)
        {
            if(iniciarNoOffset)
            {
                MoverPara(_origem);
            }
            else
            {
                MoverPara(_origem + offset);
            }
        }
        else
        {
            if(!iniciarNoOffset)
            {
                MoverPara(_origem);
            }
            else
            {
                MoverPara(_origem + offset);
            }
        }
         

    }

    public void MoverPara(Vector3 destino)
    {
        if (!_chegou)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino,velocidade* Time.deltaTime);
            if (Vector3.Distance(transform.position, destino) <= _tolerancia)
            {
                _chegou = true;
                transform.position = destino;
            }
        }
    }
    
    public void LigarDesligar(bool valor)
    {
        _chegou = false;
        ligado = valor;
    }

    public void InverterLigar()
    {
        _chegou = false;
        ligado = !ligado;
    }
}
