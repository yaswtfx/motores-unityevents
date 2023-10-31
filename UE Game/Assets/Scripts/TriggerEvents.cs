using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvents : MonoBehaviour
{
    public List<string> tagsVerificadas;
    public UnityEvent onTriggerEnter, onTriggerExit;
    bool CheckTags(Collider other)
    {
        //se não tem nenhuma tag na lista nao passa na verificação
        if (tagsVerificadas == null || tagsVerificadas.Count == 0) return false;
        
        bool resultado = false;
        foreach (var t in tagsVerificadas)
        {
            resultado = resultado || other.tag == t;
        } 
        return resultado;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (CheckTags(other))
        {
            onTriggerEnter.Invoke();
        }
    }
   
    private void OnTriggerExit(Collider other)
    {
        if (CheckTags(other))
        {
            onTriggerExit.Invoke();
        }
    }
}
