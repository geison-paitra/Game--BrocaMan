using UnityEngine;
using System.Collections;

public class ScriptBroca : MonoBehaviour
{

    public Rigidbody2D rigidbody;                          //"Corpo" do personagem
    public Transform checkGround;                        //Checa se está no chão
    private Animator anim;                               //Controle de animação
    public LayerMask whatIsGround;                       //Verifica o que é chão
    public float moveSpeed;                          //Velocidade de movimento maxima 
    public float jumpHeight;                         //Autura do pulo
    private CircleCollider2D colider;                            //Colider do player
    private float movimento;                          //Pega a diração do eixo do personagem
    private bool isWalking = false;                  //Está caminhando?
    private bool isOnTheFloor = true;                //Está pisando no chão?
    private bool isJumping = false;                  //Está Pulando?
    private bool running = false;                    //Está correndo?

    void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        //rigidbody = this.GetComponent<Rigidbody2D>(); //retorna o corpo rigido da instancia do personagem (feito manualmente)
        anim = GetComponent<Animator>();
        colider = GetComponent<CircleCollider2D>();
    }



    void FixedUpdate()
    {


        isOnTheFloor = Physics2D.OverlapCircle(checkGround.position, 0.3f, whatIsGround); // verifica se está tocando no chão

        if (Input.GetButtonDown("Jump") && isOnTheFloor && !running && !isJumping)// Verifica se apertou o botã de pulo
        {
            isJumping = true;
            Jump();
        }


        if (isOnTheFloor && isJumping) // se tocou o chão e estava na animação de jumping vola pra animação Idle
        {
            isJumping = false;
            anim.SetBool("isJumping", false);

        }


        if (Input.GetButton("Horizontal")) //Verifica se está apertando dir ou esq
        {
            movimento = Input.GetAxis("Horizontal"); //pegar a movimentação do eixo horizontal (x)

            isWalking = isOnTheFloor ? true : false; // Está andando? Está se estiver pisando no chão


            if (Input.GetKeyDown(KeyCode.LeftShift) && isOnTheFloor && !running) //Corrida
            {
                moveSpeed += 2;
                running = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) && running) //Parou de correr
            {
                running = false;
                moveSpeed -= 2;
            }

            if (Input.GetKey("left") || Input.GetKey("a")) //Esquerda
            {
                rigidbody.velocity = GetComponent<Rigidbody2D>().velocity = new Vector3(moveSpeed * movimento, 0, 0);
                if (isJumping) anim.SetBool("isJumping", false); //Clicou um direcional no ar? Sai da animação de pulo
                FlipLeft();

            }
            else if (Input.GetKey("right") || Input.GetKey("d")) // Direita
            {
                rigidbody.velocity = GetComponent<Rigidbody2D>().velocity = new Vector3(moveSpeed * movimento, 0, 0);
                if (isJumping) anim.SetBool("isJumping", false); //Clicou um direcional no ar? Sai da animação de pulo
                FlipRight();
            }

            anim.SetBool("isWalking", isWalking);
        }
        else if (Input.GetKey(KeyCode.LeftControl)) //ataque especial
        {
            float a = transform.localScale.x;
            a *= -1;
            transform.localScale = new Vector3(a, 1);
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }  

    }

    void FlipLeft()
    {
        transform.localScale = new Vector3(-1f, 1f, 1f);
    }

    void FlipRight()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private bool Jump()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isJumping", true);
        isOnTheFloor = false;
        rigidbody.AddForce(new Vector2(0, jumpHeight));
        return true;
    }

}
