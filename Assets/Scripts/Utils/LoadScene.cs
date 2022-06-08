using Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : Singleton<LoadScene>
{
    // Start is called before the first frame update
    public void Load(int i)
    {
        SceneManager.LoadScene(i);
    }
    // Update is called once per frame
   
    public void Load(string g)
    {
        SceneManager.LoadScene(g);
    }
}
