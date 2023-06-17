using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    public bool gamePlaying = false;

    public GameObject pongTitle;
    public GameObject instructions;
    public GameObject winnerText;

    public AnimationClip fadeIn, fadeOut;

    public GameObject player1, player2;

    // Start is called before the first frame update
    void Start()
    {
        NewGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        gamePlaying = false;
        instructions.SetActive(true);
        instructions.GetComponent<TMP_Text>().text = "PRESS SPACE TO START";
        winnerText.SetActive(false);
        pongTitle.SetActive(true);
        pongTitle.GetComponent<Animation>().Play(fadeIn.name);

        ScoreController.Instance.ResetScore();
    }

    public void EndGame(bool player1Won)
    {
        gamePlaying = false;
        instructions.SetActive(true);
        instructions.GetComponent<TMP_Text>().text = "PRESS SPACE FOR NEW GAME";
        winnerText.SetActive(true);
        if (player1Won)
        {
            GameManager.Instance.winnerText.GetComponent<TMP_Text>().text = "PLAYER 1 WINS!";
        }
        else
        {
            GameManager.Instance.winnerText.GetComponent<TMP_Text>().text = "PLAYER 2 WINS!";
        }
        pongTitle.SetActive(true);
        pongTitle.GetComponent<Animation>().Play(fadeIn.name);
    }

    public void NewRound()
    {
        gamePlaying = false;
        instructions.SetActive(true);
        winnerText.SetActive(false);
        pongTitle.SetActive(false);

        player1.transform.position = new Vector2(player1.transform.position.x, 0);
        player2.transform.position = new Vector2(player2.transform.position.x, 0);
    }


    public void StartGame()
    {
        gamePlaying = true;
        pongTitle.GetComponent<Animation>().Play(fadeOut.name);
        pongTitle.SetActive(false);
        instructions.SetActive(false);
        winnerText.SetActive(false);
    }
}
