using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poderpersonagem : MonoBehaviour
{
    public Rigidbody2D Jogador; // variavel recebe e conica
    public float VelocidadePoder; // determina velocidade dentro do unity
    public float DistanciaPoder; // recebe quantidade de time
    bool Direita;


    // Start is called before the first frame update
    void Start()
    {
        Jogador = GetComponent<Rigidbody2D>(); // GetComponent procura o rig
        if (transform.rotation.eulerAngles.y == 0)
        {
            Jogador.velocity = new Vector2(VelocidadePoder, 0);
            Direita = true;
        }
        else
        {
            Jogador.velocity = new Vector2(-VelocidadePoder, 0);
            Direita = false;
        }

        Destroy(gameObject, DistanciaPoder); // Destroy os objetos e tempos etc..
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "urso")
        {
            inimigo urso = collision.gameObject.GetComponent<inimigo>();
            urso.Dano(Direita);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}