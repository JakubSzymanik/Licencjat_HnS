using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{
    void Update()
    {
        if (Input.anyKeyDown)
            SceneManager.LoadScene(1);
    }
}
