using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rig;
    public int speed = 1;
    public int Jump = 4;
    public bool chao, puloDuplo;
    public float velocidade = 30f;
    public Animator animator;
    private Rigidbody2D playerRigidbody2D;



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start de " + this.name);
        animator = GetComponent<Animator>();
        playerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mover();
        pular();
    }

    void mover()
    {
        float movimentoHorizontal = Input.GetAxisRaw("Horizontal");

        Vector2 moveDirection = new Vector2(movimentoHorizontal, 0).normalized;
        Vector2 newPosition = playerRigidbody2D.position + moveDirection * velocidade * Time.deltaTime;

        playerRigidbody2D.MovePosition(newPosition);

        if (movimentoHorizontal < 0)
        {
            animator.SetBool("Esquerda", true);
        }
        else
        {
            animator.SetBool("Esquerda", false);
        }

        if (movimentoHorizontal > 0)
        {
            animator.SetBool("Direita", true);
        }
        else
        {
            animator.SetBool("Direita", false);
        }

    }







    void pular()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (chao)
            {
                rig.AddForce(Vector2.up * Jump, ForceMode2D.Impulse);
                chao = false;
                puloDuplo = true;
            }
            else if (puloDuplo)
            {
                rig.AddForce(Vector2.up * Jump, ForceMode2D.Impulse);
                chao = false;
                puloDuplo = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("plataforma"))
        {
            chao = true;
            puloDuplo = false;
        }
    }
}
