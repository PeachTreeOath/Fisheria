using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopExitManager : Singleton<ShopExitManager> {

    private bool[] finished;

    private void Start()
    {
        finished = new bool[5];
    }

    public void Finished(int playerNum)
    {
        bool go = true;
        for(int i = 1; i < 5; i++)
        {
            if(!finished[i])
            {
                go = false;
                break;
            }
        }

        if(go)
        {
            SceneTransitionManager.instance.GoToGame();
        }
    }
}
