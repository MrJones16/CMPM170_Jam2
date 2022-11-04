using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToStartScene : MonoBehaviour
{
    private float upInput;
    // Update is called once per frame
    void Update()
    {
        upInput = Input.GetAxis("Up");
        if (upInput != 0)
        {
            SceneManager.LoadScene(0);
        }
        
    }
}
