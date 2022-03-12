using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetMouseButton(0))
        {
            SceneManager.LoadScene("Juego");
        }
        if(Input.GetKeyDown(KeyCode.I)){
            SceneManager.LoadScene("Inicio");
        }
    }
}
