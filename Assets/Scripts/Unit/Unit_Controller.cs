using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Controller : MonoBehaviour
{
    public Rigidbody2D rigidbody;
    public BoxCollider2D collider;
    public CircleCollider2D unit_base;
    public Animator animator;
    public Surface_Contol surface_Contol;

    public UIManager uiManager;

    public Level_Descriptor level;

    public float max_velocity = 3f;
    public float jump_force = 300f;

    public bool on_ground = true;

    public bool shouldHide = true;

    private Vector3 jump_vector;

    private float y_level;
    private float current_y_level;
    private float last_y_level;

    private bool check_landing = false;
    private bool same_y = false;
    private bool check_same_y = false;

    private float max_jump_offset = 0.01f;

    private bool is_hiding = false;

    private bool to_right = true;

    private bool locked_right = false; //USED FOR PREVENT PLAYER FROM MOVEMENT TO THE WALL WHEN HE IS AT AIR
    private bool locked_left = false; //USED FOR PREVENT PLAYER FROM MOVEMENT TO THE WALL WHEN HE IS AT AIR

    public bool canMove = true; //YOU CAN JUST FORBIDE UNIT TO GO

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
        unit_base = GetComponent<CircleCollider2D>();
        animator = GetComponent<Animator>();

        rigidbody.bodyType = RigidbodyType2D.Static;

        jump_vector = new Vector3(0, jump_force, 0);
    }


    void Update()
    {

        if (level.IsTherePlafromBelow(this))
            current_y_level = level.GetDistanceToPlatformBelow(gameObject);
        else
            check_same_y = true;

    }


    //World will check when to set on_ground true
    public void Jump()
    {
        if (on_ground)
        {
            //TEMP
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            Vector3 DEBUG_JUMP = new Vector3(0, jump_force, 0);

            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, 0);
            rigidbody.AddForce(DEBUG_JUMP);
            rigidbody.gravityScale = Level_Descriptor.gravity_scale;

            surface_Contol.Jump();

            StandUp();
            JumpAnimate();
            JumpForcedStart();
        }
        //JumpAnimationStop();
    }

    public void SetMoveVector(Vector3 move_vector)
    {
        if (!canMove)
        {
            if (on_ground)
            {
                if(rigidbody.bodyType != RigidbodyType2D.Static)
                    rigidbody.velocity = new Vector3(0, 0, 0);
                StopWalking();
                Hide();
            }
            return;
        }

        if (move_vector.x < 0 && locked_left || move_vector.x > 0 && locked_right)
        {
            return;
        }

        if(move_vector.x == 0)
        {
            locked_left = false;
            locked_right = false;
        }
        if (move_vector.sqrMagnitude == 0)
        {
            StopWalking();
            Hide();
        }
        else
        {
            StandUp();
            if(on_ground)
                StartWalking();
        }

        if (to_right && move_vector.x < 0 || !to_right && move_vector.x > 0)
            Flip();

        if (on_ground && !is_hiding)
        {
            Vector3 normalized_move_vector = move_vector.normalized;
            rigidbody.velocity = normalized_move_vector * max_velocity;
        }
        else if(!is_hiding)
        {
            float x_velocity = 0;
            if(move_vector.x != 0)
                x_velocity += move_vector.x / Mathf.Abs(move_vector.x) * max_velocity;
            rigidbody.velocity = new Vector3(x_velocity, rigidbody.velocity.y, 0);
        }
    }

    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("Surface") && !collider.IsTouching(collision))
        {
            Debug.Log("leave");

            if (on_ground)
            {
                RaycastHit2D ground = Physics2D.Raycast(transform.position - new Vector3(rigidbody.velocity.normalized.x, 0, 0), new Vector3(0, -1, 0), Mathf.Infinity, LayerMask.NameToLayer("Collidable"));
                if(ground.collider.gameObject.layer == LayerMask.NameToLayer("Collidable"))
                    y_level = ground.distance;
            }

            check_landing = true;
            on_ground = false;


            rigidbody.gravityScale = level.gravity_scale;
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Collidable"))
        {
            RaycastHit2D ground = Physics2D.Raycast(transform.position, new Vector3(0, -1, 0), 0.32f, LayerMask.NameToLayer("Collidable"));
            if (ground)
            {
                on_ground = true;
                check_landing = false;
                collider.isTrigger = false;
                same_y = false;
                rigidbody.gravityScale = 0;

                check_same_y = false;
            }
            else
            {
                RaycastHit2D ceiling = Physics2D.Raycast(transform.position, new Vector3(0, 1, 0), 0.32f, LayerMask.NameToLayer("Collidable"));
                if (ceiling != null)
                    return;
                //DEBUG! CLARIFY THE 1f VALUE AND OTHERS
                bool right = Physics2D.Raycast(transform.position, new Vector3(1, 0, 0), 1f, LayerMask.NameToLayer("Collidable"));
                BackFrameX(right);
                if (right) locked_right = true;
                else locked_left = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Collidable"))
        {
            //rigidbody.velocity = new Vector3(0, 0, 0);
        }
    }

    public void Hide()
    {
        if (on_ground && !is_hiding && shouldHide)
        {
            animator.SetBool("Hiding", true);
            is_hiding = true;
            rigidbody.bodyType = RigidbodyType2D.Static;
            collider.isTrigger = true;
        }
    }

    public void StandUp()
    {
        animator.SetBool("Hiding", false);
        is_hiding = false;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        collider.isTrigger = false;
    }

    public void StartWalking()
    {
        animator.SetBool("Move", true);
    }

    public void StopWalking()
    {
        animator.SetBool("Move", false);
    }

    public void JumpAnimate()
    {
        animator.SetBool("Jump", true);
    }

    public void JumpAnimationStop()
    {
        animator.SetBool("Jump", false);
    }

    public void JumpForcedStart()
    {
        animator.Play("Jump", 0);
    }

    public void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        if(uiManager != null)
            uiManager.Flip();
        to_right = !to_right;
    }

    public void StartFalling()
    {
        rigidbody.gravityScale = Level_Descriptor.gravity_scale;
        on_ground = false;
    }

    public void StopFalling()
    {
        rigidbody.gravityScale = 0;
        on_ground = true;
    }

    public void BackFrameX(bool right)
    {
        if(right)
            transform.position = new Vector3(transform.position.x - 0.05f, transform.position.y, 0);
        else
            transform.position = new Vector3(transform.position.x + 0.05f, transform.position.y, 0);
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }
}
