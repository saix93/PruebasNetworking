using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour {

    [SyncVar(hook = "OnNameChanged")]
    string playerName;

    [SyncVar(hook = "OnColorChanged")]
    Color playerColor;

    [SyncVar(hook = "OnRadioChanged")]
    float playerRadio = 0.5f;
    
    Vector2 _lastDirection;
    Rigidbody2D _rigidbody;

    float GetArea()
    {
        return Mathf.PI * playerRadio * playerRadio;
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
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
        if (!isLocalPlayer) { 
            this.name = playerName;
            this.GetComponentInChildren<Renderer>().material.color = playerColor;
        }
        OnRadioChanged(playerRadio);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        this.name = "Raúl T.";
        this.GetComponentInChildren<Renderer>().material.color = Color.yellow;

        CmdChangeName("Raúl T.");
        CmdChangeColor(Color.green);
    }

    private void Update()
    {      
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
        if (isServer)
        {
            Player enemy = collision.attachedRigidbody.GetComponent<Player>();
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

    void OnRadioChanged(float radio)
    {
        this.transform.localScale = 2 * radio * Vector3.one;
    }

    [Command]
    void CmdChangeName(string name)
    {
        this.gameObject.name = name;
        playerName = name;
    }

    [Command]
    void CmdChangeColor(Color color)
    {
        GetComponentInChildren<Renderer>().material.color = color;
        playerColor = color;
    }    


}
