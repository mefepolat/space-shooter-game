using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private Text _restartLevelText;

    private GameManager _gameManager;

    private bool _isGameOver = false;
    
    void Start()
    {
       
        _scoreText.text = "Score: " + 000;
        _gameOverText.gameObject.SetActive(false);
        _restartLevelText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if(_gameManager == null)
        {
            Debug.LogError("Game manager is null");
        }
    }

    private void Update()
    {
       
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
            GameOverSequence();
            
        }
    }

    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartLevelText.gameObject.SetActive(true);
        _isGameOver = true;
        StartCoroutine(FlickerGameOverTextRoutine());
    }


    IEnumerator FlickerGameOverTextRoutine()
    {
        while (_isGameOver)
        {

            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
