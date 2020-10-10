using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personagem : MonoBehaviour
{
    public float Velocidade; /// variavel global velocidade
    public int peixe; /// variavel peixe global
    public Rigidbody2D Jogador; /// variavel personagem jogador
    public float ForcaGravidadePulo; /// variavel para determinar forca do pulo dentro do unity
    bool NoChao = false; /// variavel recebe a funcao detecta chao
    public float ForcaDoPulo; /// variavel recebe funcao e forca do pulo dentro do unity

    public Transform origempoder;
    public GameObject prefabpoder;

    public float volumepassos;
    public AudioSource passos;

    public float volumepulo;
    public AudioSource pulo;

    // Start is called before the first frame update
    void Start()
    {
        Jogador = GetComponent<Rigidbody2D>(); /// procura o rigidbody no unity
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
        ///mover
        float hAxis = Input.GetAxis("Horizontal"); /// recebe aperto de tecla

        float xPox = hAxis * Velocidade + Time.deltaTime; /// 

        Jogador.velocity = new Vector2(xPox, Jogador.velocity.y); /// outra forma de movimentação

        ///transform.position += Vector3.right * hAxis * speed * Time.deltaTime; /// recebe movimentação 

        volumepassos = hAxis;

        ///ROTACIONAR IMAGENS DO PERSONAGEM
        if (hAxis < 0)
        {
            volumepassos = hAxis * -1;
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        }
        else if (hAxis > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        passos.volume = volumepassos;

        ///OUTRA FORMA DE mover
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
            Instantiate(prefabpoder, origempoder.position, origempoder.rotation);

        }
    }
    /// funcao para pular
    void Pulo()
    {
        if (Input.GetButton("Jump") && NoChao == true) // recebe botao precionado 
        {
            float PosicaoY = ForcaDoPulo + Time.deltaTime; // define a para pulo
            Jogador.velocity = new Vector2(Jogador.velocity.x, PosicaoY); // recebe o resultado do pulo
            volumepulo = PosicaoY;
            
        }
        pulo.volume = volumepulo;
    }

    /// funcao determonar a forca aplicada na gravidade do pulo
    void GravidadePulo()
    {
        Jogador.AddForce(Vector2.down * ForcaGravidadePulo); /// AddForce adiciona forca
    }

    /// DETECTA COLISÃO COM OBIJETOS DE FISICOS
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    /// DETECTA COLISÃO COM OS OBIJETOS QUE USAM TRIGGEER E LAYER.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /// detecta colisao com os peixe
        if (collision.tag == "peixe")
        {
            peixe += 2;
            Destroy(collision.gameObject);
        }

        /// detecta colisao com a caixa vermelha
        if (collision.tag == "caixavermelha")
        {
            peixe -= 1;
            Destroy(collision.gameObject);

        }

        /// detecta colisao com o chao
        if (collision.gameObject.layer == 8)
        {
            NoChao = true;
        }
    }

    /// detecta saidas de colisao com objetos e layer
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            NoChao = false;
        }

    }
}
