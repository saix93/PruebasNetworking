using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : Food {

    [SyncVar(hook = "OnNameChanged")]
    string playerName;

    [SyncVar(hook = "OnColorChanged")]
    Color playerColor;
        
    Vector2 _lastDirection;
    Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetPlayerName(string name)
    {
        this.playerName = name;
    }

    public void SetPlayerColor(Color color)
    {
        this.playerColor = color;
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
        playerRadio = Random.Range(3f, 4f);
        OnRadioChanged(playerRadio);
    }

    public override void OnStartClient()
    {        
        base.OnStartClient();        
        this.name = playerName;
        this.GetComponentInChildren<Renderer>().material.color = playerColor;        
        OnRadioChanged(playerRadio);
    }
    
    public override void OnNetworkDestroy()
    {
        base.OnNetworkDestroy();
        if (isLocalPlayer)
        {
            FindObjectOfType<GameCanvas>().ActivateLosePanel();
        }
    }

    protected override void Update()
    {
        base.Update();

        if(isLocalPlayer)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector2 direction = new Vector2(
                (horizontal > 0) ? 1 : (horizontal < 0) ? -1 : 0,
                (vertical > 0) ? 1 : (vertical < 0) ? -1 : 0);
            if(direction != _lastDirection)
            {
                _lastDirection = direction;
                _rigidbody.velocity = direction.normalized * 15;                
            }
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("OnTriggerStay with " + collision.name);
        if (isServer)
        {
            Food enemy = collision.attachedRigidbody.GetComponent<Food>();
            if (this.playerRadio > enemy.playerRadio)
            {
                float distance = Vector3.Distance(this.transform.position, enemy.transform.position);
                float sumRadios = this.playerRadio + enemy.playerRadio;
                float radioToEat = sumRadios - distance;
                radioToEat = Mathf.Clamp(radioToEat, 0, enemy.playerRadio);

                float currentEnemyArea = enemy.GetArea();
                enemy.playerRadio -= radioToEat;
                enemy.OnRadioChanged(enemy.playerRadio);

                float newEnemyArea = enemy.GetArea();
                float areaToAdd = currentEnemyArea - newEnemyArea;
                float myNewArea = GetArea() + areaToAdd;
                playerRadio = Mathf.Sqrt(myNewArea / Mathf.PI);
                OnRadioChanged(playerRadio);
            }
        }        
    }

    void OnNameChanged(string name)
    {
        this.name = name;
    }

    void OnColorChanged(Color color)
    {
        GetComponentInChildren<Renderer>().material.color = color;
    }

    public void Victory()
    {
        RpcVictory();
    }

    [ClientRpc]
    void RpcVictory()
    {
        if (isLocalPlayer)
        {
            FindObjectOfType<GameCanvas>().ActivateVictoryPanel();
            FindObjectOfType<GameCanvas>().SetVictoryMessage("¡Felicidades " + this.name + ", has ganado!");
        }
        else
        {
            FindObjectOfType<GameCanvas>().SetLoseMessage(this.name + " ha ganado");
        }
    }


}
