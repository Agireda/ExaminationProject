using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController player;

    public float speed;


    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 Move = Vector3.zero;
        Move.x = Input.GetAxis("Horizontal") * speed;
        Move.y = Input.GetAxis("Vertical") * speed;
        player.Move(Move * Time.deltaTime);
    }
}
