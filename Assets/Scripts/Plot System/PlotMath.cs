using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotMath 
{
    public static Vector3 CalculateLandingPoint(GameObject jumpPoint)
    {
        List<Vector3> dots = new List<Vector3>();

        float time_step = 0.01f;

        float V_y = (Level_Descriptor.unit_jump_force / Level_Descriptor.unit_mass) * time_step * 0.93f;

        float simulationTime = 4f; //seconds

        RaycastHit2D collidable = Physics2D.Raycast(jumpPoint.transform.position, new Vector2(0, -1), Mathf.Infinity, 1 << LayerMask.NameToLayer("Collidable"));

        float jump_level = collidable.distance;

        Vector3 current = jumpPoint.transform.position;

        float t = 0;

        dots.Add(current);

        do
        {
            t += time_step;
            current = new Vector3(jumpPoint.transform.position.x + Level_Descriptor.unit_max_velocity * t * 0.41f, jumpPoint.transform.position.y + V_y * t - Level_Descriptor.gravity_scale * t * t, 0);
            dots.Add(current);
            collidable = Physics2D.Raycast(current, new Vector2(0, -1), Mathf.Infinity, 1 << LayerMask.NameToLayer("Collidable"));
        } while (collidable.distance > jump_level && t < simulationTime);

        float delta = collidable.distance - jump_level;

        dots[dots.Count - 1] += new Vector3(0, delta, 0);

        return dots[dots.Count - 1];
    }
}
