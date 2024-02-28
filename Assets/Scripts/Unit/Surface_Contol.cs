using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface_Contol : MonoBehaviour
{
    public Unit_Controller unit;

    BoxCollider2D boxCollider;

    public bool on_surface = true;
    public bool on_ground = true;

    private bool check_landing = false; //this bool is used to check the fact the unit left the surface. If it is true, than the unit can land
    private bool is_jumping = false;

    private float y_level;
    private float jump_level;
    private float current_y_level_right;
    private float current_y_level_left;
    private (float, float) current_y_levels;
    private Level_Descriptor level;

    private float jump_delta_check = 0.01f;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        level = unit.level;
    }

    
    void Update()
    {
        if (level.IsTherePlafromBelow(unit))
        {
            current_y_levels = level.GetDistancesToPlatformBelow(unit);
            current_y_level_right = current_y_levels.Item1;
            current_y_level_left = current_y_levels.Item2;

            if (current_y_level_left == 0)
                current_y_level_left = Mathf.Infinity;
            if (current_y_level_right == 0)
                current_y_level_right = Mathf.Infinity;
        }

        float current_min_y_level = Mathf.Min(current_y_level_left, current_y_level_right);
        float current_max_y_level = Mathf.Max(current_y_level_left, current_y_level_right);


        if (on_surface && on_ground)
        {
            y_level = Mathf.Min(current_y_level_left, current_y_level_right);
        }

        boxCollider.isTrigger = true;

        float delta = is_jumping ? jump_level - current_min_y_level : y_level - current_min_y_level;

        if (delta > jump_delta_check / 2) check_landing = true;

        if (is_jumping && check_landing && current_min_y_level < jump_level && current_max_y_level > jump_level)
        {
            boxCollider.isTrigger = false;
            check_landing = false;
            on_surface = false;
            on_ground = false;
            unit.StartFalling();
        }

        if (!on_ground && check_landing && level.IsTherePlafromBelow(unit))
        {
            if(delta > 0)
            {
                is_jumping = false;
                on_ground = true;
                unit.StopFalling();
                unit.transform.position = new Vector3(unit.transform.position.x, unit.transform.position.y + delta, 0);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Surface") && collision.IsTouching(boxCollider))
        {
            on_surface = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Surface") && !collision.IsTouching(boxCollider))
        {
            on_surface = false;
            check_landing = true;
            on_ground = false;
            unit.StartFalling();
        }
    }

    public void Jump()
    {
        is_jumping = true;
        on_ground = false;
        check_landing = false;
        jump_level = Mathf.Min(current_y_level_left, current_y_level_right);
        unit.StartFalling();
    }

    public bool IsOnSurface()
    {
        return on_surface;
    }
}
