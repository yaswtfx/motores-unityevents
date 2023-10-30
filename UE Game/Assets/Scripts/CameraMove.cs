using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraMove : MonoBehaviour
{
    public bool esconderMouse = true; //Controla se o cursor do mouse é exibido
    public float sensibilidadeX = 2.0f, sensibilidadeY = 0.1f; //Controla a sensibilidade do mouse
    private float mouseX = 0.0f, mouseY = 0.0f; //Variáveis que controlam a rotação do mouse
    private CinemachineFreeLook freeLookCam;

    void Start()
    {
        TryGetComponent(out freeLookCam);
        
        if (!esconderMouse)
        {
            return;
        }

        Cursor.visible = false; //Oculta o cursor do mouse
        Cursor.lockState = CursorLockMode.Locked; //Trava o cursor no centro
    }

    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * sensibilidadeX; // Incrementa o valor do eixo X e multiplica pela sensibilidade
        mouseY -= Input.GetAxis("Mouse Y") * sensibilidadeY; // Incrementa o valor do eixo Y e multiplica pela sensibilidade
        
        freeLookCam.m_XAxis.Value = mouseX;
        freeLookCam.m_YAxis.Value = mouseY;

    }
}