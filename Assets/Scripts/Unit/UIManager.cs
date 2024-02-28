using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject msgText;
    public GameObject background;
    public GameObject canvas_obj;

    public float msgShowTime = 2f;

    private TMP_Text txt;
    private Image img;
    private Canvas canvas;

    private bool somethingShown = false;

    void Start()
    {
        txt = msgText.GetComponent<TMP_Text>();
        img = background.GetComponent<Image>();
        canvas = canvas_obj.GetComponent<Canvas>();

        txt.enabled = false;
        img.enabled = false;
    }

    void Update()
    {
    }

    public void ShowMessage(string text)
    {
        if (!somethingShown)
        {
            somethingShown = true;
            txt.enabled = true;
            img.enabled = true;
            StartCoroutine(ShowMessageCoroutine(text));
        }
    }

    private IEnumerator ShowMessageCoroutine(string text)
    {
        string[] lines = text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            lines[i] = lines[i].Trim();

            if (lines[i].Equals("")) continue;
            
            txt.SetText(lines[i]);
            txt.pageToDisplay = 1;
            do
            {
                yield return new WaitForSeconds(msgShowTime);
                txt.pageToDisplay++;
            } while (txt.pageToDisplay <= txt.textInfo.pageCount);            
        }
  
        somethingShown = false;
        txt.enabled = false;
        img.enabled = false;
    }

    public void Flip()
    {
        canvas.transform.localScale = new Vector3(canvas.transform.localScale.x * -1, canvas.transform.localScale.y, canvas.transform.localScale.z);
    }

    public bool IsSomethingShown()
    {
        return somethingShown;
    }
}
