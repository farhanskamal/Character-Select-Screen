using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "ScriptableObject/Character Data")]
public class CharacterData: ScriptableObject
{
    public string characterName;
    public Sprite characterFullBody;
    public Sprite characterPFP;
    public bool faceRight;
}