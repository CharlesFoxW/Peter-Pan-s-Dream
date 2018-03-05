using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class loading : MonoBehaviour {

    private Image ScrollBarImage;
    private float i = 0.0f;

    // Use this for initialization
    void Start () {
        ScrollBarImage = GetComponent<Image>();
        ScrollBarImage.fillAmount = i;
    }
 
// Update is called once per frame
    void Update()
    {     
        if (i <= 1)
        {
            i += 0.002f;
            ScrollBarImage.fillAmount = i;
        }
    }
}
