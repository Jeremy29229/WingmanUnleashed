using UnityEngine;
using System.Collections;

public class NPCLarge : NPCControlScript
{
    public LargeCharacters CharacterType = LargeCharacters.Gamer;

    void Start()
    {
        base.StartSetup();
    }

    protected override void SetupRandomDialogue()
    {
        GameObject conversationInformation = gameObject.transform.FindChild("ConversationInformation").gameObject;
        base.SetupRandomDialogue();
        
        switch (CharacterType)
        {
            case LargeCharacters.Gamer:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomGamerName();
                randomDialogs[0].NPCDialog = "I have like 30 followers on my Switch Stream.";
                randomDialogs[1].NPCDialog = "The latest Hat Man movie was insufferabley incorrect in regards to Cane's backstory. Typical mainstream media.";
                randomDialogs[2].NPCDialog = "Wanna see my level 9999 CyberWhale on YoY?";
                randomDialogs[3].NPCDialog = "Did you get invited to the exclusive beta test of - who am I kidding, of course you didn't.";
                randomDialogs[4].NPCDialog = "I hear there's this new 'wingman' game in development. Sounds like a game for filthy casuals.";
                break;
            case LargeCharacters.RichMan:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameMasculine();
                randomDialogs[0].NPCDialog = "<i>Pretentiously jolly laughter</i>";
                randomDialogs[1].NPCDialog = "I paid for a large portion of this party. Only the very best for my closest - wait, who are you?";
                randomDialogs[2].NPCDialog = "Some say that my greatest weakness is that I am just far too humble.";
                randomDialogs[3].NPCDialog = "I hope this party ends soon. I have five more to attend tonight!";
                randomDialogs[4].NPCDialog = "Sometimes I toss money off this rooftop and watch the people down below. It's like the ancient ducks with bread.";
                break;
            case LargeCharacters.RichWoman:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameFeminine();
                randomDialogs[0].NPCDialog = "Hmmmmmmmmmph.";
                randomDialogs[1].NPCDialog = "What do you want? Never mind, I don't care.";
                randomDialogs[2].NPCDialog = "I should think a party of this caliber would warrant more care in outfit planning. Evidently not.";
                randomDialogs[3].NPCDialog = "I presume you have a good reason for staring at me?";
                randomDialogs[4].NPCDialog = "<i>Pretends not to see you</i>";
                break;
        }
        conversationInformation.GetComponent<Interactable>().Action = "Talk to";
    }

    protected override void SetThemeOutfit(PartyTheme theme)
    {
        Material mat;
        string matName = "";
        string themeName = "_" + theme.ToString();

        themeName = "";//NOTE: comment this out when the separate party theme outfits actually exist
        switch (CharacterType)
        {
            case LargeCharacters.Gamer:
                matName = "CharLarge_Gamer" + themeName;
                break;
            case LargeCharacters.RichMan:
                matName = "CharLarge_RichMan" + themeName;
                break;
            case LargeCharacters.RichWoman:
                matName = "CharLarge_RichWoman" + themeName;
                break;
        }
        mat = (Material)Resources.Load(matName, typeof(Material));
        transform.FindChild("Mesh").GetComponent<Renderer>().material = mat;
    }

    void Update()
    {

    }
}
