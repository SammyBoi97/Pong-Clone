using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private static ScoreController _instance;
    public static ScoreController Instance { get { return _instance; } }

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


    public Sprite[] numberSprite = new Sprite[10];

    public GameObject player1Digit1;
    public GameObject player1Digit2;
    public GameObject player2Digit1;
    public GameObject player2Digit2;

    public int player1Score;
    public int player2Score;


    // Start is called before the first frame update
    void Start()
    {
        ResetScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetScore()
    {
        player1Digit1.SetActive(false);
        player2Digit1.SetActive(false);

        player1Digit2.GetComponent<SpriteRenderer>().sprite = numberSprite[0];
        player2Digit2.GetComponent<SpriteRenderer>().sprite = numberSprite[0];

        player1Score = 0;
        player2Score = 0;

    }

    public void UpdateScore(bool player1Won)
    {
        if (player1Won)
        {
            player1Score++;
            player1Digit1.SetActive(player1Score > 9);
            player1Digit2.GetComponent<SpriteRenderer>().sprite = numberSprite[player1Score % 10];
            
        }
        else
        {
            player2Score++;
            player2Digit1.SetActive(player2Score > 9);
            player2Digit2.GetComponent<SpriteRenderer>().sprite = numberSprite[player2Score % 10];
        }

        if (player1Score >= 11)
        {
            GameManager.Instance.EndGame(true);
        }
        else if (player2Score >= 11)
        {
            GameManager.Instance.EndGame(false);
        }
        else
        {
            GameManager.Instance.NewRound();
        }
    }
}
