using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 3f;

    public bool isPlayer1 = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gamePlaying)
        {
            if (isPlayer1)
            {
                transform.Translate(Vector3.up * Input.GetAxis("Player 1 Vertical") * speed * Time.deltaTime);
            }
            if (!isPlayer1)
            {
                transform.Translate(Vector3.up * Input.GetAxis("Player 2 Vertical") * speed * Time.deltaTime);
            }

            transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, -4f, 4f));

        }
    }
}
