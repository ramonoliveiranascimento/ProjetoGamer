using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ControladorSom : MonoBehaviour
{
    public AudioMixer meuaudio;
    public bool EstadoSom = true;
    public Image ImgSom;
    public Sprite ImgLigado;
    public Sprite ImgDesligado;
    void Start()
    {
        Ligar();
    }
    void Update()
    {
    }
    public void AlterarSom()
    {
        if (EstadoSom == true)
        {
            Desligar();
        }
        else
        {
            Ligar();
        }
    }
    public void Desligar()
    {
        EstadoSom = false;
        meuaudio.SetFloat("master", -80);
        ImgSom.sprite = ImgDesligado;

    }
    public void Ligar()
    {
        EstadoSom = true;
        meuaudio.SetFloat("master", 0);
        ImgSom.sprite = ImgLigado;
    }
}
