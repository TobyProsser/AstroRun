using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChangeController : MonoBehaviour
{
    public RuntimeAnimatorController brushedController;
    public RuntimeAnimatorController fishController;
    private void Awake()
    {
        if (MenuController.skin == 0)
        {
            this.GetComponent<Animator>().runtimeAnimatorController = brushedController;
        }
        else if (MenuController.skin == 1)
        {
            print("secondSkin");
            this.GetComponent<Animator>().runtimeAnimatorController = fishController;
        }
    }

}
