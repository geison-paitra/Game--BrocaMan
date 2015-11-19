using UnityEngine;
using System.Collections;

public class LifeController : MonoBehaviour
{

    public float life;
    public Texture blood, bar;
    public Rigidbody2D rigidbody;                          //"Corpo" do personagem
    private float fullLife = 100;


    //teste
    private BoxCollider2D boxCollider;
    private float topAngle;
    private float sideAngle;

    void Start()
    {
        life = fullLife;

        //detectar lugar da colisão
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.size = Vector2.Scale(boxCollider.size, (Vector2)transform.localScale);
        topAngle = Mathf.Atan(boxCollider.size.x / boxCollider.size.y) * Mathf.Rad2Deg;
        sideAngle = 90.0f - topAngle;
    }

    void Update()
    {
        if (life >= fullLife)
        {
            life = fullLife;
        }
        else if (life <= 0)
        {
            life = 0;
        }

        if (life <=0)
        {
            Application.LoadLevel("Level 1");//temporariamente só rcarega o level
        }

    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(Screen.width / 30, Screen.height / 30, Screen.width / 5, Screen.height / 8), bar);
        GUI.DrawTexture(new Rect(Screen.width / 25, Screen.height / 16, Screen.width / 5.5f / fullLife * life, Screen.height / 15), blood);
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        //Colisões com o inimigo
        if (coll.gameObject.layer == 10)//Layer do Inimigo
        {
            

            Vector3 v = (Vector3)coll.contacts[0].point - transform.position;

            //if (Vector3.Angle(v, transform.up) <= topAngle)
            //{
            //    Debug.Log("Collision on top");
            //}


            //Uma uma pequena força para empurrar
            if (Vector3.Angle(v, transform.right) <= sideAngle) //só tira vida com colisão lateral
            {

                rigidbody.AddForce(new Vector2(-200f, 0));
                life -= 10;
            }
            else if (Vector3.Angle(v, -transform.right) <= sideAngle)
            {

                rigidbody.AddForce(new Vector2(200f, 0));
                life -= 10;
            }

            //else
            //{
            //    Debug.Log("Collision on Bottom");
            //}
        }
        if (coll.gameObject.layer == 11)//Coletou vida
        {
            life += 25;
            Destroy(coll.gameObject);
        }
    }
}
