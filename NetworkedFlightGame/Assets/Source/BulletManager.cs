using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    //send create / destroy events
    //
    public void newChild(GameObject go){
        Debug.Log(go.name);
    }
}
