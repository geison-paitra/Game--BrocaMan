using UnityEngine;
using System.Collections;

public class ScriptBroca : MonoBehaviour
{

    public Rigidbody2D          rigidbody;                          //"Corpo" do personagem
    public Transform            checkGround;                        //Checa se está no chão
    private Animator            anim;                               //Controle de animação
    public LayerMask            whatIsGround;                       //Verifica o que é chão
    public float                moveSpeed;                          //Velocidade de movimento maxima 
    public float                jumpHeight;                         //Autura do pulo
    private CircleCollider2D    colider;                            //Colider do player
    private float               movimento;                          //Pega a diração do eixo do personagem
    private bool                isWalking = false;                  //Está caminhando?
    private bool                isOnTheFloor = true;                //Está pisando no chão?
    private bool                isJumping = false;                  //Está Pulando?
    private bool                running = false;                    //Está correndo?

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
        isJumping = Input.GetButtonDown("Jump") && isOnTheFloor && !running ? Jump() : false; // Verifica se apertou o botã de pulo

        if (Input.GetButton("Horizontal")) //Verifica se está apertando dir ou esq
        {
            movimento = Input.GetAxis("Horizontal");

            isWalking = isOnTheFloor ? true : false;

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

            if (Input.GetKey("left") || Input.GetKey("a"))
            {
                rigidbody.velocity = GetComponent<Rigidbody2D>().velocity = new Vector3(moveSpeed * movimento, 0, 0);
                FlipLeft();

            }
            if (Input.GetKey("right") || Input.GetKey("d"))
            {
                rigidbody.velocity = GetComponent<Rigidbody2D>().velocity = new Vector3(moveSpeed * movimento, 0, 0);
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
        rigidbody.AddForce(new Vector2(0, jumpHeight));
        return true;
    }

}
