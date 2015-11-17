using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public float enemySpeed = 0.3f;
    public float maxX;
    public float minX;
    public LayerMask whatIsGround;
    public Transform checkGround;
    private bool left = true;               //Está vindo para a esquerda?
    private bool automatic = false;
    private float moviment = 0.1f;
    private bool isOnTheFloor = true;
    private Animator anim;

    void Start()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
        anim = GetComponent<Animator>();
        Physics2D.IgnoreLayerCollision(10, 10);
        // rigidbody = this.GetComponent<Rigidbody2D>(); //retorna o corpo rigido da instancia do personagem
    }

    void Update()
    {

        isOnTheFloor = Physics2D.OverlapCircle(checkGround.position, 0.5f, whatIsGround); // verifica se está tocando no chão

        moviment = 10f;

        if (!isOnTheFloor)
        {
            anim.SetBool("IsOnTheFloor", false);
        }
        else
        {
            anim.SetBool("IsOnTheFloor", true);
            if (left)
            {
                rigidbody.velocity = new Vector3(-enemySpeed * moviment, 0, 0); //Está vindo para esquerda então a velocidade é negativa
            }
            else
            {
                rigidbody.velocity = new Vector3(enemySpeed * moviment, 0, 0); // senão a velocidade é positiva
            }

            if (rigidbody.transform.position.x <= minX)
            {
                left = FlipRight();
            }
            else if (rigidbody.transform.position.x >= maxX)
            {
                left = FlipLeft();
                
            }
        }

      
    }

    bool FlipLeft()
    {
        rigidbody.transform.localScale = new Vector3(1f, 1f, 1f);
        return true;
    }

    bool FlipRight()
    {
        rigidbody.transform.localScale = new Vector3(-1f, 1f, 1f);
        return false;
    }
}
