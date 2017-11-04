using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CharacterPanel : MonoBehaviour
{
    public Toggle blueToggle, orangeToggle, greenToggle, redToggle;
    public Text nameText, lvlText, classText, xpText, hpText, maxHpText, acText, profText, speedText,
        strModText, dexModText, conModText, intModText, wisModText, chaModText,
        strScoreText, dexScoreText, conScoreText, intScoreText, wisScoreText, chaScoreText,
        strThrowModText, dexThrowModText, conThrowModText, intThrowModText, wisThrowModText, chaThrowModText;
    public Dictionary<PlayerCharacter, Character> characters = new Dictionary<PlayerCharacter, Character>();

    private Toggle[] toggles;

    void OnEnable()
    {
        characters = FindObjectOfType<CharacterDatabase>().CharacterDictionary;
        UpdateCharacterStats(FindObjectOfType<GameManager>().currentCharacter);
        toggles = new Toggle[] { blueToggle, orangeToggle, greenToggle, redToggle };
        toggles[(int)FindObjectOfType<GameManager>().currentCharacter].isOn = true;     // This is matching the integer value of the PlayerCharacter enum to the index position of the toggles... less than ideal.
    }

    public void SwitchCharacter(int index) // Forced to use index here due to rigging in editor.
    {
        switch (index)
        {
            case 0:
                UpdateCharacterStats(PlayerCharacter.Crystal);
                break;
            case 1:
                UpdateCharacterStats(PlayerCharacter.Teddy);
                break;
            case 2:
                UpdateCharacterStats(PlayerCharacter.Hunter);
                break;
            case 3:
                UpdateCharacterStats(PlayerCharacter.Damien);
                break;
            default:
                Debug.LogWarning("Trying to switch to a PlayerCharacter in CharacterPanel that is out of range.");
                break;
        }
    }

    public void UpdateCharacterStats(PlayerCharacter playerCharacter)
    {
        nameText.text = characters[playerCharacter].Name;
        lvlText.text = "Lvl " + characters[playerCharacter].GetLvl().ToString();
        classText.text = characters[playerCharacter].Class;
        xpText.text = characters[playerCharacter].Xp.ToString() + " XP";
        hpText.text = characters[playerCharacter].CurrentHP.ToString();
        maxHpText.text = "/" + characters[playerCharacter].BaseHp.ToString();
        acText.text = characters[playerCharacter].GetAc().ToString();
        profText.text = "+" + characters[playerCharacter].GetProfBonus().ToString();
        speedText.text = characters[playerCharacter].Speed.ToString() + "ft";

        strModText.text = DisplayAbilityScoreModifier(characters[playerCharacter], AbilityType.Str);
        dexModText.text = DisplayAbilityScoreModifier(characters[playerCharacter], AbilityType.Dex);
        conModText.text = DisplayAbilityScoreModifier(characters[playerCharacter], AbilityType.Con);
        intModText.text = DisplayAbilityScoreModifier(characters[playerCharacter], AbilityType.Int);
        wisModText.text = DisplayAbilityScoreModifier(characters[playerCharacter], AbilityType.Wis);
        chaModText.text = DisplayAbilityScoreModifier(characters[playerCharacter], AbilityType.Cha);

        strScoreText.text = characters[playerCharacter].AbilityScoreDictionary[AbilityType.Str].ToString();
        dexScoreText.text = characters[playerCharacter].AbilityScoreDictionary[AbilityType.Dex].ToString();
        conScoreText.text = characters[playerCharacter].AbilityScoreDictionary[AbilityType.Con].ToString();
        intScoreText.text = characters[playerCharacter].AbilityScoreDictionary[AbilityType.Int].ToString();
        wisScoreText.text = characters[playerCharacter].AbilityScoreDictionary[AbilityType.Wis].ToString();
        chaScoreText.text = characters[playerCharacter].AbilityScoreDictionary[AbilityType.Cha].ToString();

        strThrowModText.text = DisplayThrowModifier(characters[playerCharacter], AbilityType.Str);
        dexThrowModText.text = DisplayThrowModifier(characters[playerCharacter], AbilityType.Dex);
        conThrowModText.text = DisplayThrowModifier(characters[playerCharacter], AbilityType.Con);
        intThrowModText.text = DisplayThrowModifier(characters[playerCharacter], AbilityType.Int);
        wisThrowModText.text = DisplayThrowModifier(characters[playerCharacter], AbilityType.Wis);
        chaThrowModText.text = DisplayThrowModifier(characters[playerCharacter], AbilityType.Cha);
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
}
