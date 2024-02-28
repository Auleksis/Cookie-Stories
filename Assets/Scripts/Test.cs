using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocity = 1f;

    public RectTransform move_rect;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x + velocity * Time.deltaTime, transform.position.y, 0);
    }
}
