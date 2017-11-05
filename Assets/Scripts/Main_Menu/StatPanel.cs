using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StatPanel : MonoBehaviour
{
    public Toggle blueToggle, orangeToggle, greenToggle, redToggle;
    public Text nameText, lvlText, classText, xpText, hpText, maxHpText, acText, profText, speedText,
        strModText, dexModText, conModText, intModText, wisModText, chaModText,
        strScoreText, dexScoreText, conScoreText, intScoreText, wisScoreText, chaScoreText,
        strThrowModText, dexThrowModText, conThrowModText, intThrowModText, wisThrowModText, chaThrowModText;

    private Dictionary<PlayerCharacter, Character> characterDictionary;
    Toggle[] toggles;

    void Awake()
    {
        characterDictionary = FindObjectOfType<CharacterDatabase>().CharacterDictionary;
        toggles = new Toggle[] { blueToggle, orangeToggle, greenToggle, redToggle };
    }

    void OnEnable()
    {
        PopulateCharacterStats(FindObjectOfType<GameManager>().currentCharacter);
        toggles[(int)FindObjectOfType<GameManager>().currentCharacter].isOn = true;     // This is matching the integer value of the PlayerCharacter enum to the index position of the toggles... less than ideal.
    }

    public void PopulateCharacterStats(PlayerCharacter playerCharacter)
    {
        nameText.text = characterDictionary[playerCharacter].Name;
        lvlText.text = "Lvl " + characterDictionary[playerCharacter].GetLvl().ToString();
        classText.text = characterDictionary[playerCharacter].Class;
        xpText.text = characterDictionary[playerCharacter].Xp.ToString() + " XP";
        hpText.text = characterDictionary[playerCharacter].CurrentHP.ToString();
        maxHpText.text = "/" + characterDictionary[playerCharacter].BaseHp.ToString();
        acText.text = characterDictionary[playerCharacter].GetAc().ToString();
        profText.text = "+" + characterDictionary[playerCharacter].GetProfBonus().ToString();
        speedText.text = characterDictionary[playerCharacter].Speed.ToString() + "ft";

        strModText.text = DisplayAbilityScoreModifier(characterDictionary[playerCharacter], AbilityType.Str);
        dexModText.text = DisplayAbilityScoreModifier(characterDictionary[playerCharacter], AbilityType.Dex);
        conModText.text = DisplayAbilityScoreModifier(characterDictionary[playerCharacter], AbilityType.Con);
        intModText.text = DisplayAbilityScoreModifier(characterDictionary[playerCharacter], AbilityType.Int);
        wisModText.text = DisplayAbilityScoreModifier(characterDictionary[playerCharacter], AbilityType.Wis);
        chaModText.text = DisplayAbilityScoreModifier(characterDictionary[playerCharacter], AbilityType.Cha);

        strScoreText.text = characterDictionary[playerCharacter].AbilityScoreDictionary[AbilityType.Str].ToString();
        dexScoreText.text = characterDictionary[playerCharacter].AbilityScoreDictionary[AbilityType.Dex].ToString();
        conScoreText.text = characterDictionary[playerCharacter].AbilityScoreDictionary[AbilityType.Con].ToString();
        intScoreText.text = characterDictionary[playerCharacter].AbilityScoreDictionary[AbilityType.Int].ToString();
        wisScoreText.text = characterDictionary[playerCharacter].AbilityScoreDictionary[AbilityType.Wis].ToString();
        chaScoreText.text = characterDictionary[playerCharacter].AbilityScoreDictionary[AbilityType.Cha].ToString();

        strThrowModText.text = DisplayThrowModifier(characterDictionary[playerCharacter], AbilityType.Str);
        dexThrowModText.text = DisplayThrowModifier(characterDictionary[playerCharacter], AbilityType.Dex);
        conThrowModText.text = DisplayThrowModifier(characterDictionary[playerCharacter], AbilityType.Con);
        intThrowModText.text = DisplayThrowModifier(characterDictionary[playerCharacter], AbilityType.Int);
        wisThrowModText.text = DisplayThrowModifier(characterDictionary[playerCharacter], AbilityType.Wis);
        chaThrowModText.text = DisplayThrowModifier(characterDictionary[playerCharacter], AbilityType.Cha);
    }

    private string DisplayAbilityScoreModifier(Character character, AbilityType ability)
    {
        string result = character.GetAbilityScoreModifier(ability).ToString();
        if (character.GetAbilityScoreModifier(ability) > 0) result = "+" + result;
        return result;
    }

    private string DisplayThrowModifier(Character character, AbilityType ability)
    {
        string result = character.GetThrowMod(ability).ToString();
        if (character.GetThrowMod(ability) > 0) result = "+" + result;
        return result;
    }

    // Forced to use index here due to limitations in Inspector.
    public void SwitchCharacter(int index)
    {
        switch (index)
        {
            case 0:
                PopulateCharacterStats(PlayerCharacter.Crystal);
                break;
            case 1:
                PopulateCharacterStats(PlayerCharacter.Teddy);
                break;
            case 2:
                PopulateCharacterStats(PlayerCharacter.Hunter);
                break;
            case 3:
                PopulateCharacterStats(PlayerCharacter.Damien);
                break;
            default:
                Debug.LogWarning("Trying to switch to a PlayerCharacter in CharacterPanel that is out of range.");
                break;
        }
    }
}
