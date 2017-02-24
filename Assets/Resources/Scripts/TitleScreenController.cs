using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour
{

    void Start()
    {
        AudioManager.instance.PlayMusic("prophet", 1);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < 5; i++)
        {
            if (Input.GetButtonDown("FireA" + i) || Input.GetButtonDown("FireB" + i))
            {
                SceneTransitionManager.instance.StartGame();
            }
        }
    }
}
