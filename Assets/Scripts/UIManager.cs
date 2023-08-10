using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Text _gameOverText;

    private bool isGameOver = false;
    
    void Start()
    {
       
        _scoreText.text = "Score: " + 000;
        _gameOverText.gameObject.SetActive(false);

    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        
        _livesImage.sprite = _liveSprites[currentLives];
        if(currentLives == 0)
        {
            _gameOverText.gameObject.SetActive(true);
            isGameOver = true;
            StartCoroutine(FlickerGameOverTextRoutine());
        }
    }



    IEnumerator FlickerGameOverTextRoutine()
    {
        while (isGameOver)
        {

            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
