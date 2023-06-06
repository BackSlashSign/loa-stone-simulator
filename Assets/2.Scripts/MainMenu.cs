using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void StartButton()
    {
        //load Main(1) scene
        SceneManager.LoadScene(1);
    }
}
