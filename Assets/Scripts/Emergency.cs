#define Editor
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emergency : MonoBehaviour
{
    private GameObject flowerVase;
    // Start is called before the first frame update
    void Start()
    {
        flowerVase = GameObject.Find("FlowerVase");
        flowerVase.GetComponent<Rigidbody>().useGravity = false;
        flowerVase.SetActive(false);

#if Editor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            flowerVase.SetActive(true);
            flowerVase.GetComponent<Rigidbody>().velocity = Vector3.zero;
            flowerVase.GetComponent<Rigidbody>().useGravity = true;
        }


    }
}
