using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inimigo : MonoBehaviour
{
    Rigidbody2D Urso;
    SpriteRenderer Imagem;
    Animator Animacao;

    float PosicaoInicical;
    float Destino;
    string estadoanimacao;
    bool DanoUrso = false;

    public float Velocidade;
    public float Vai;
    public float ForcaContato;


    
    

    // Start is called before the first frame update
    void Start()
    {
        Urso = GetComponent<Rigidbody2D>();
        Imagem = GetComponent<SpriteRenderer>();
        Animacao = GetComponent<Animator>();

        PosicaoInicical = transform.position.x;
        Destino = PosicaoInicical + Vai;
    }

    // Update is called once per frame
    void Update()
    {
        if(DanoUrso == false)
            Andar();   
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (DanoUrso == true && collision.gameObject.layer == 8)
        {
            Controleanimacao("urso_explode");
        }
    }

    void Andar()
    {
        Urso.MovePosition(transform.position + (Vector3.right * (Velocidade/100)));
        Controleanimacao("urso_andando");
        if(transform.position.x >= Destino && Imagem.flipX == true)
        {
            Velocidade *= -1;
            Imagem.flipX = false;

        }
        else if(transform.position.x <= PosicaoInicical && Imagem.flipX == false)
        {
            Velocidade *= -1;
            Imagem.flipX = true;
        }
    }

    public void Controleanimacao(string novoestadoanimacao)
    {
        if (novoestadoanimacao == estadoanimacao)
            return;

        Animacao.Play(novoestadoanimacao);

        estadoanimacao = novoestadoanimacao;
    }

    public void Dano(bool Direita)
    {
        DanoUrso = true;
        if (Direita)
        {
            Urso.AddForce(new Vector2(1, 1) * ForcaContato);
            Controleanimacao("urso_dano");
        }
        else
        {
            Urso.AddForce(new Vector2(-1, 1) * ForcaContato);
            Controleanimacao("urso_dano");
        }
    }

    public void Explodindo()
    {
        Destroy(gameObject);
    }
}
