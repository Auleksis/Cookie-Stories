using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Input_Manager : MonoBehaviour
{
    public Unit_Controller player;

    float h_input;
    float v_input;
    float jump_input;
    float hiding;

    Vector3 velocity_vector;

    void Start()
    {
        
    }

    void Update()
    {
        //FOR DEBUG ON COMPUTER
        h_input = Input.GetAxisRaw("Horizontal");
        v_input = Input.GetAxisRaw("Vertical");
        //jump_input = Input.GetAxisRaw("Jump");

        if (Input.GetKeyDown(KeyCode.F))
        {
            hiding = 1;
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            hiding = 0;
        }

       /* if (hiding != 0)
        {
            player.Hide();
        }
        else
        {
            player.StandUp();
        }*/

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump_input = 1;  
        }

        if (jump_input != 0)
        {
            velocity_vector = new Vector3(h_input, 0, 0);
            player.Jump();
        }
        else
        {
            velocity_vector = new Vector3(h_input, v_input, 0);
        }

        jump_input = 0;
    }

    private void FixedUpdate()
    {
        player.SetMoveVector(velocity_vector);
    }
}
