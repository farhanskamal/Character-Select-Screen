using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    public string characterName;
    public Sprite characterFullBody;
    public Sprite characterPFP;
    public bool faceRight;

    public Image CharacterPFPImage;

    public void SetView(string characterName, Sprite characterFullBody, Sprite characterPFP, bool faceRight) {
        //Copy the Values 
        this.characterName = characterName;
        this.characterFullBody = characterFullBody;
        this.characterPFP = characterPFP;
        this.faceRight = faceRight;
        
        //Update the Button Image 
        CharacterPFPImage = transform.Find("CharacterPFP").GetComponent<Image>();
        CharacterPFPImage.sprite = this.characterPFP;
        
        // Give Button Action
        transform.GetComponent<Button>().onClick.AddListener(() => GameController.Instance.UpdateCharacterSelect(characterName, characterFullBody, faceRight));
    }
}
