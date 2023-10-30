using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private AudioSource _audioSource;
    public AudioMixer audioMixer;
    public AudioClip selectUI;

    private void Awake()
    {
        if (instance != null)
        {
            instance = this;
            Inicializacao();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Inicializacao()
    {
        TryGetComponent(out _audioSource);
    }

    public void SelectUI()
    {
        _audioSource.PlayOneShot(selectUI);
    }
    public void MutarAudio(bool mudo)
    {
        if (mudo)
        {
            //variavel exposta no audiomixer grupo master
            //o nome padrao da variavel era MyExposedParam e foi alterado pra Volume
            audioMixer.SetFloat("Volume", -80); //mudo
        }
        else
        {
            audioMixer.SetFloat("Volume", 0); //nao-mudo
        }
        
    }

}
