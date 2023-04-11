using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance{
        get {
            if (_instance == null)
            {   
                Debug.LogError("GameManager is null");
            }
            return _instance;
        }
    }
    private void Awake(){
        _instance = this;
    }
    
    public int velocidade;
    public int pontos;
    public string nome;
    public string codigo;
    public int jogadorId;
    public int playerCheckpoint;
    public int quantidadePlayers;
    public bool jogando=false;
    public GameObject player;
    public GameObject[] OtherPlayers;
    public Transform[] checkpoints;
    public GameObject TelaEspera;
    public GameObject TelaEntrar;
    void Start()
    {
        pontos = 0;
    }

    public void Impacto(Collider chamou, Collider colidiu){
        //Debug.Log("Impacto "+ colidiu.gameObject.layer);
        //explosao
        if (chamou.gameObject.tag == "Explosion"){
            if (colidiu.gameObject.layer == 6){//se foi no player
                
            }
        }
    }
    
    public void Inicio(int valor){
        if (valor == jogadorId)
        {
            Instantiate(player, checkpoints[valor].position, Quaternion.identity);
        } else{
            Instantiate(OtherPlayers[valor], checkpoints[valor].position, Quaternion.identity);
        }
        //Instantiate(OtherPlayer1, checkpoint2);
        jogando = true;
    }
    public void Espera(){
        TelaEspera.SetActive(true);
        TelaEntrar.SetActive(false);
        if (playerCheckpoint == 0)
        {//dono do jogo
           // TelaEspera.GetComponent<InicioText>().ativarBotao();
        }else{
            //TelaEspera.GetComponent<InicioText>().ativarTexto();
        }
    }
    public void GameOver(){
        Destroy(player);
    }

}
