using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.UI.CanvasScaler;

public class Level_Descriptor : MonoBehaviour
{
    public float platform_height = 2;
    public float available_platform_height = 1.6f;
    public LayerMask collidable_layer;
    public static float gravity_scale = 3f;
    public static float unit_jump_force = 1000f;
    public static float unit_mass = 1f;
    public static float unit_max_velocity = 4f;

    public Unit_Controller player;

    public TilemapCollider2D surface;

    public CinemachineVirtualCamera camera;

    public Clock clock;

    [HideInInspector] public Vector3 check_base_vector;
    void Start()
    {
        check_base_vector = new Vector3(0, -1, 0);
    }

    void Update()
    {
        
    }

    public bool IsUnitOnSurface(Unit_Controller unit)
    {
        Vector3 base_center = unit.unit_base.bounds.center;
        float radius = unit.unit_base.radius;

        Vector3 top_bottom = new Vector3(0, radius, 0);
        Vector3 right_left = new Vector3(radius, 0, 0);

        Vector3 bottom = base_center - top_bottom;
        Vector3 top = base_center + top_bottom;
        Vector3 right = base_center + right_left;
        Vector3 left = base_center - right_left;

        if (!surface.OverlapPoint(bottom) || !surface.OverlapPoint(left) || !surface.OverlapPoint(top) || !surface.OverlapPoint(right))
            return false;

        return true;
    }

    public float GetDistanceToPlatformBelow(GameObject unit)
    {
        RaycastHit2D result = Physics2D.Raycast(unit.transform.position, check_base_vector, Mathf.Infinity, collidable_layer);
        return result.distance;
    }

    public static float DebugDistanceToPlatformBelow(Vector3 position)
    {
        RaycastHit2D result = Physics2D.Raycast(position, new Vector3(0, -1, 0), Mathf.Infinity, LayerMask.NameToLayer("Collidable"));
        return result.distance;
    }

    public (float, float) GetDistancesToPlatformBelow(Unit_Controller unit)
    {
        RaycastHit2D right = Physics2D.Raycast(unit.collider.bounds.max - new Vector3(0, unit.collider.bounds.size.y, 0), check_base_vector, Mathf.Infinity, collidable_layer);
        RaycastHit2D left = Physics2D.Raycast(unit.collider.bounds.min, check_base_vector, Mathf.Infinity, collidable_layer);
        return (right.distance, left.distance);
    }

    public bool IsTherePlafromBelow(Unit_Controller unit)
    {
        RaycastHit2D result = Physics2D.Raycast(unit.transform.position, check_base_vector, Mathf.Infinity, collidable_layer);
        if (result)
            return true;
        return false;
    }

    public bool UnitColliderInstersectsSurface(Unit_Controller unit)
    {
        return surface.bounds.Intersects(unit.collider.bounds);
    }
}
