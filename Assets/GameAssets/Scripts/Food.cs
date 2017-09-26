using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Food : NetworkBehaviour {

    [SyncVar(hook = "OnRadioChanged")]
    public float playerRadio = 0.5f;

    public float GetArea()
    {
        return Mathf.PI * playerRadio * playerRadio;
    }

    public void OnRadioChanged(float radio)
    {
        this.transform.localScale = 2 * radio * Vector3.one;
    }

    protected virtual void Update()
    {
        if (isServer)
        {
            if (playerRadio < Mathf.Epsilon)
            {
                NetworkServer.Destroy(this.gameObject);
            }
            
        }
        
    }

}
