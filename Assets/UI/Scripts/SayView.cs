using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class SayView : MonoBehaviour
{
    public GameObject followedObject;

    private Label sayText;

    VisualElement root;

    public GameObject say;

    private void OnEnable()
    {
        //say = GameObject.Find("SayForm");
    }

    void Start()
    {
        
    }

    private void LateUpdate()
    {
        float y = followedObject.transform.position.y + 1.5f;
        Vector3 position = new Vector3(followedObject.transform.position.x, y, followedObject.transform.position.z);

        Vector2 canvasPos;
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(position);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponent<RectTransform>(), screenPoint, null, out canvasPos);

        say.transform.localPosition = canvasPos;
    }


}
