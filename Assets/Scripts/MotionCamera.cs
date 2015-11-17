using UnityEngine;
using System.Collections;

public class MotionCamera : MonoBehaviour
{

    public Transform player;                    // Este é o  personagem do jogo que a câmera irá acompanhar
    private Vector3 cameraPosition;             // Nova posição da camera
    public float minCamYPosition = -2.50f;      // Posição minima do x que a câmera irá atingr
    private bool alive = true;                  // Verifica se o player está vivo

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform; //usar para buscar player altomaticamente 
    }

    void Update()
    {
        if (minCamYPosition < transform.position.y)
        {
            cameraPosition = new Vector3(player.position.x, player.position.y + 0.6f, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, cameraPosition, Time.time);
        }
        else
        {
            alive = (player.position.y<(-100f)) ? DeadTrigger() : true;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) Application.LoadLevel("Menu");
        
    }

    private bool DeadTrigger()
    {
        Application.LoadLevel("Level 1");
        return true;
    }
}
