using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public List<CharacterData> characters = new List<CharacterData>();
    public GameObject gridParent; 
    public GameObject characterPrefab;
    
    public Image playerOneCharacterImage;
    public Image playerTwoCharacterImage;
    
    public TextMeshProUGUI playerOneName;
    public TextMeshProUGUI playerTwoName;
    public TextMeshProUGUI lockInStartButtonText;

    private float fadeDuration = 1.0f;
    public Image fadePanel; 

    [FormerlySerializedAs("startImage")] public Sprite startSprite;
    public string startName;
    public enum State {
        Player1Choice,
        Player2Choice,
        StartGame
    }
    
    public State currentState = State.Player1Choice;
    
    public static GameController Instance { get; private set; }
    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    
    private void Start() {
        playerOneCharacterImage.sprite = startSprite;
        playerTwoCharacterImage.sprite = startSprite;
        playerOneName.text = startName;
        playerTwoName.text = startName;
        
        SetImageAlpha(0f);
        fadePanel.raycastTarget = false;
        
        lockInStartButtonText.text = "Lock In Player 1";
        
        foreach (CharacterData character in characters) {
            GameObject obj = Instantiate(characterPrefab, gridParent.transform);
            obj.GetComponent<CharacterView>().SetView(character.characterName, character.characterFullBody, character.characterPFP, character.faceRight);
        }
    }

    public void UpdateCharacterSelect(string characterName, Sprite characterFullBody, bool faceRight) {
        switch (currentState) {
            case State.Player1Choice: {
                playerOneCharacterImage.sprite = characterFullBody;
                playerOneName.text = characterName;
                playerOneCharacterImage.transform.localScale = faceRight ? new Vector3(2f, 2, 2) : new Vector3(-2f, 2, 2);
                break;
            }
            case State.Player2Choice: {
                playerTwoCharacterImage.sprite = characterFullBody;
                playerTwoName.text = characterName;
                playerTwoCharacterImage.transform.localScale = faceRight ? new Vector3(-2f, 2, 2) : new Vector3(2f, 2, 2);
                break;
            }
        }
    }

    public void UpdateState() {
        switch (currentState) {
            case State.Player1Choice: {
                if (playerOneName.text == "???") {
                    break;
                }
                lockInStartButtonText.text = "Lock In Player 2";
                currentState = State.Player2Choice;
                break;
            }
            case State.Player2Choice: {
                if (playerTwoName.text == "???") {
                    break;
                }
                lockInStartButtonText.text = "Start";
                currentState = State.StartGame;
                StartCoroutine(FadeRoutine());
                break;
            }
        }
    }

    private IEnumerator FadeRoutine()
    {
        // Block clicks during fade
        fadePanel.raycastTarget = true;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float currentAlpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            SetImageAlpha(currentAlpha);
            yield return null;
        }

        SetImageAlpha(1f);

        yield return new WaitForSeconds(1f);
        // Reload the active scene
        int activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(activeSceneIndex);
    }

    private void SetImageAlpha(float alpha)
    {
        Color color = fadePanel.color;
        color.a = alpha;
        fadePanel.color = color;
    }
}
