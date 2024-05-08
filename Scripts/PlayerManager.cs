using System.Collections;
using UnityEngine;
using TMPro;
public class PlayerManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText, directionText, scoreText;
    Rigidbody2D rb;
    int speed = 10;
    public int score = 0;
    Vector3 initialPosition;
    Color[] colors = new Color[] { Color.red, Color.blue, Color.green };
    string[] directions = { "Up", "Down", "Left", "Right", "Blue", "Red", "Green" };
    string currentDirection;
    Color? randomColor;
    bool lost = false;
    bool firstTry = true;
    bool isCorrect;
    public GameObject topCorner, leftCorner, rightCorner, bottomCorner, lostPanel;
    private void Start()
    {
        isCorrect = true;
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        InvokeRepeating("generateTask", 2f, 2f);
        StartCoroutine(CountdownFiveSeconds());
        currentDirection = directions[Random.Range(0, directions.Length)];
        directionText.text = currentDirection;
    }
    void generateTask()
    {
        isCorrect = true;
        currentDirection = directions[Random.Range(0, directions.Length)];
        directionText.text = currentDirection;
        switch (currentDirection)
        {
            case "Red":
                randomColor = Color.red;
                break;
            case "Blue":
                randomColor = Color.blue;
                break;
            case "Green":
                randomColor = Color.green;
                break;
            default:
                break;
        }
        Debug.Log(currentDirection);
        Debug.Log(randomColor);

    }
    IEnumerator CountdownFiveSeconds()
    {
        for (float i = 2f; i > 0f; i--)
        {
            if (firstTry)
            {
                isCorrect = true;
            }
            if(!firstTry)
                isCorrect = true;
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
            if(i == 2f && isCorrect == false)
            {
                lost = true;
                firstTry = false;
            }
            if (i == 2f && isCorrect == true)
            {
                firstTry = false;
            }
        }
        if (!lost)
        {
            StartCoroutine(CountdownFiveSeconds());
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "corner")
        {
            rb.velocity = Vector2.zero;
            if (!lost)
            {
                transform.position = initialPosition;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (lost)
        {
            lostPanel.SetActive(true);
            Time.timeScale = 0;
        }
        Debug.Log(lost);
        if (Input.GetKeyDown("up") && (currentDirection == "Up" || topCorner.GetComponent<SpriteRenderer>().color == randomColor))
        {
            rb.velocity = Vector2.up * speed;
            isCorrect = true;
            score++;
            scoreText.text = score.ToString();
        }
        else if (Input.GetKeyDown("down") && (currentDirection == "Down" || bottomCorner.GetComponent<SpriteRenderer>().color == randomColor))
        {
            rb.velocity = Vector2.down * speed;
            isCorrect = true;
            score++;
            scoreText.text = score.ToString();
        }
        else if (Input.GetKeyDown("left") && (currentDirection == "Left" || leftCorner.GetComponent<SpriteRenderer>().color == randomColor))
        {
            rb.velocity = Vector2.left * speed;
            isCorrect = true;
            score++;
            scoreText.text = score.ToString();
        }
        else if (Input.GetKeyDown("right") && (currentDirection == "Right" || rightCorner.GetComponent<SpriteRenderer>().color == randomColor))
        {
            rb.velocity = Vector2.right * speed;
            isCorrect = true;
            score++;
            scoreText.text = score.ToString();
        }
        else if (Input.GetKeyDown("right") && (currentDirection != "Right" || rightCorner.GetComponent<SpriteRenderer>().color != randomColor))
        {
            isCorrect = false;
        }
        else if (Input.GetKeyDown("up") && (currentDirection != "Up" || topCorner.GetComponent<SpriteRenderer>().color != randomColor))
        {
            isCorrect = false;
        }
        else if (Input.GetKeyDown("down") && (currentDirection != "Down" || bottomCorner.GetComponent<SpriteRenderer>().color != randomColor))
        {
            isCorrect = false;
        }
        else if (Input.GetKeyDown("left") && (currentDirection != "Left" || leftCorner.GetComponent<SpriteRenderer>().color != randomColor))
        {
            isCorrect = false;
        }
    }
}