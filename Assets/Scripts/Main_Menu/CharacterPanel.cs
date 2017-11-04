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

    public void UpdateCharacterStats(PlayerCharacter character)
    {
        nameText.text = characters[character].Name;
        lvlText.text = "Lvl " + characters[character].GetLvl().ToString();
        classText.text = characters[character].Class;
        xpText.text = characters[character].Xp.ToString() + " XP";
        hpText.text = characters[character].CurrentHP.ToString();
        maxHpText.text = "/" + characters[character].BaseHp.ToString();
        acText.text = characters[character].GetAc().ToString();
        profText.text = "+" + characters[character].GetProfBonus().ToString();
        speedText.text = characters[character].Speed.ToString() + "ft";

        strModText.text = DisplayAbilityScoreModifier(characters[character], AbilityType.Str);
        dexModText.text = DisplayAbilityScoreModifier(characters[character], AbilityType.Dex);
        conModText.text = DisplayAbilityScoreModifier(characters[character], AbilityType.Con);
        intModText.text = DisplayAbilityScoreModifier(characters[character], AbilityType.Int);
        wisModText.text = DisplayAbilityScoreModifier(characters[character], AbilityType.Wis);
        chaModText.text = DisplayAbilityScoreModifier(characters[character], AbilityType.Cha);

        strScoreText.text = characters[character].AbilityScoreDictionary[AbilityType.Str].ToString();
        dexScoreText.text = characters[character].AbilityScoreDictionary[AbilityType.Dex].ToString();
        conScoreText.text = characters[character].AbilityScoreDictionary[AbilityType.Con].ToString();
        intScoreText.text = characters[character].AbilityScoreDictionary[AbilityType.Int].ToString();
        wisScoreText.text = characters[character].AbilityScoreDictionary[AbilityType.Wis].ToString();
        chaScoreText.text = characters[character].AbilityScoreDictionary[AbilityType.Cha].ToString();

        strThrowModText.text = DisplayThrowModifier(characters[character], AbilityType.Str);
        dexThrowModText.text = DisplayThrowModifier(characters[character], AbilityType.Dex);
        conThrowModText.text = DisplayThrowModifier(characters[character], AbilityType.Con);
        intThrowModText.text = DisplayThrowModifier(characters[character], AbilityType.Int);
        wisThrowModText.text = DisplayThrowModifier(characters[character], AbilityType.Wis);
        chaThrowModText.text = DisplayThrowModifier(characters[character], AbilityType.Cha);
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
