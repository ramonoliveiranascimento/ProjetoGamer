using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personagem : MonoBehaviour
{
    public Rigidbody2D Jogador; /// variavel personagem jogador
    public float Velocidade; /// variavel global velocidade
    public float ForcaGravidadePulo; /// variavel para determinar forca do pulo dentro do unity
    public float ForcaDoPulo; /// variavel recebe funcao e forca do pulo dentro do unity
    bool NoChao = false; /// variavel recebe a funcao detecta chao

    public float tempo = 0; 

    public Animator Animacao_personagem; /// variavel de contato com ferramenta ANIMATOR

    public int peixe; /// variavel peixe global

    public Transform origempoder;
    public GameObject prefabpoder;

    public float volumepassos;
    public AudioSource passos;

    public AudioSource audio_pulo;



    // Start is called before the first frame update
    void Start()
    {
        Jogador = GetComponent<Rigidbody2D>(); /// procura o rigidbody no unity
        Animacao_personagem = GetComponent<Animator>(); /// procura o ANIMATOR no unity
        audio_pulo.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        Movimento();
        Pulo();
        GravidadePulo();
        Poder();
    }

    void Movimento()
    {
        // mover
        float hAxis = Input.GetAxis("Horizontal"); /// recebe aperto de tecla

        float xPox = hAxis * Velocidade + Time.deltaTime; /// 

        Jogador.velocity = new Vector2(xPox, Jogador.velocity.y); /// outra forma de movimentação

        /// transform.position += Vector3.right * hAxis * speed * Time.deltaTime; /// recebe movimentação 

        volumepassos = hAxis;

        /// ROTACIONAR IMAGENS DO PERSONAGEM
        if (hAxis < 0)
        {
            volumepassos = hAxis * -1;
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            Animacao_personagem.SetBool("parado", false); /// para animação do personagem parado
            Animacao_personagem.SetTrigger("andando"); /// ATIVA animação do personagem andando
        }
        else if (hAxis > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            Animacao_personagem.SetBool("parado", false); /// para animação do personagem parado
            Animacao_personagem.SetTrigger("andando"); /// ativa animação do personagem andando
        }
        else
        {
            Animacao_personagem.SetBool("andando",false); /// para animação do personagem andando
            Animacao_personagem.SetTrigger("parado"); /// ativa animação do personagem parado
        }

        passos.volume = volumepassos;

        //OUTRA FORMA DE mover
        /*if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(new Vector2 ou 3(0,0,0)); - ROTACIONA IMAGEM DO PERSONAGEM
            transform.Translate(Vector2.right * Velocidade * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(new Vector2 ou 3(0,180,0)); - ROTACIONA IMAGEM DO PERSONAGEM
            transform.Translate(Vector2.left * Velocidade * Time.deltaTime);
        }*/
    }
    void Poder()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Animacao_personagem.SetTrigger("tiro"); // chama animação do tiro
            Instantiate(prefabpoder, origempoder.position, origempoder.rotation);
        }
    }

    /// funcao para pular
    void Pulo()
    {
        if (Input.GetButton("Jump") && NoChao == true) /// recebe botao precionado 
        {
            float PosicaoY = ForcaDoPulo + Time.deltaTime; /// define a para pulo
            Jogador.velocity = new Vector2(Jogador.velocity.x, PosicaoY); /// recebe o resultado do pulo
            passos.Stop(); ///PARA O AUDIO PASSOS DURANTE O PULO
            audio_pulo.Play();
            Animacao_personagem.SetBool("parado", false); /// para animação do personagem parado
            Animacao_personagem.SetBool("andando", false); /// para animação do personagem andando
            Animacao_personagem.SetTrigger("pulando"); /// ativa a animção do personagem pulando
        }
    }

    // funcao determonar a forca aplicada na gravidade do pulo
    void GravidadePulo()
    {
        Jogador.AddForce(Vector2.down * ForcaGravidadePulo); /// AddForce adiciona forca
    }

    // DETECTA COLISÃO COM OS OBIJETOS QUE USAM TRIGGEER E LAYER.
    private void OnTriggerEnter2D(Collider2D collision) /// entrada
    {
        /// detecta colisao com os peixe
        if (collision.tag == "peixe")
        {
            peixe += 2;
            Destroy(collision.gameObject); /// destroi o objeto
        }

        /// detecta colisao com a caixa vermelha
        if (collision.tag == "caixavermelha")
        {
            peixe -= 1;
            Destroy(collision.gameObject); /// destroi o objeto

        }

        /// detecta colisao com o chao
        if (collision.gameObject.layer == 8)
        {
            NoChao = true;
            audio_pulo.Stop();
            passos.Play();
            Animacao_personagem.SetBool("pulando", false);
        }
    }

    // detecta saidas de colisao com objetos e layer
    private void OnTriggerExit2D(Collider2D collision) /// saida
    {
        if (collision.gameObject.layer == 8)
        {
            NoChao = false;
            passos.Stop();
        }

    }
}
