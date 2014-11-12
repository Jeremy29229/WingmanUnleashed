using UnityEngine;
using System.Collections;
using System.Linq;

public class NPCBasic : NPCControlScript
{
    public BasicCharacters CharacterType = BasicCharacters.GingerMan;

	void Start () 
    {
        base.StartSetup();
	}

    protected override void SetupRandomDialogue()
    {
        GameObject conversationInformation = gameObject.transform.FindChild("ConversationInformation").gameObject;
        base.SetupRandomDialogue();

        switch (CharacterType)
        {
            case BasicCharacters.Bartender:
                conversationInformation.GetComponent<Interactable>().InteractableName = "Bartender";
                randomDialogs[0].NPCDialog = "I'm not judging your outfit. I'm just giving you constructive criticism. Silently.";
                randomDialogs[1].NPCDialog = "I'm just a bartender. I tend the bar. Or end the bart, it really depends.";
                randomDialogs[2].NPCDialog = "It took me three years to perfect the art of silently washing cups while feigning sympathy towards your problems.";
                randomDialogs[3].NPCDialog = "<i>Silently cleans a glass with an old cloth</i>";
                randomDialogs[4].NPCDialog = "I may look bored, but I'm actually racking up points on my people watching score.";
                break;
            case BasicCharacters.GingerMan:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameMasculine();
                randomDialogs[0].NPCDialog = "Mmhm.";
                randomDialogs[1].NPCDialog = "No. I'm not going to steal your soul. Not yet.";
                randomDialogs[2].NPCDialog = "It's a shame steel trees went extinct. Nice workout.";
                randomDialogs[3].NPCDialog = "What a party.";
                randomDialogs[4].NPCDialog = "Everyone's always too busy to get their own drinks. Lazy.";
                break;
            case BasicCharacters.Jock:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameMasculine();
                randomDialogs[0].NPCDialog = "Eeeeeey ballin' threads brah!!";
                randomDialogs[1].NPCDialog = "Didja catch the game last night?! Saawwweeeeeet!";
                randomDialogs[2].NPCDialog = "Look at all these <b>GIIIIRLS</b> duuuude!";
                randomDialogs[3].NPCDialog = "CHUG CHUG CHUG CHUG CHUG CHUG CHUG CHUG CHUG CHUG CHUG CHUG!!!!";
                randomDialogs[4].NPCDialog = "Breath check. One. Two. Awhh yeah I am HAWT.";
                break;
            case BasicCharacters.PlaidMan:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameMasculine();
                randomDialogs[0].NPCDialog = "<i>Adjusts mustache</i>";
                randomDialogs[1].NPCDialog = "I have thirty-five shirts. All of them plaid. All of them different colors.";
                randomDialogs[2].NPCDialog = "My plaid game is top.";
                randomDialogs[3].NPCDialog = "I've heard that the devil wears plaid.";
                randomDialogs[4].NPCDialog = "Don't get mad. Get plaid.";
                break;
            case BasicCharacters.PoliceMan:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameMasculine();
                randomDialogs[0].NPCDialog = "STOP RIGHT THERE CRIMINAL SCUM! Just kidding. I'm off duty right now.";
                randomDialogs[1].NPCDialog = "I can't really compete with these bouncers here. But my taser can.";
                randomDialogs[2].NPCDialog = "I've been on the force for over forty years now. It can be rough sometimes.";
                randomDialogs[3].NPCDialog = "It's good to take a break and have a drink with friends every once in a while.";
                randomDialogs[4].NPCDialog = "These are some good drinks, must have been expensive.";
                break;
            case BasicCharacters.Shenheizzer:
                conversationInformation.GetComponent<Interactable>().InteractableName = "The Shenheizzer";
                randomDialogs[0].NPCDialog = "Dr-dr-dr-dr-DROP the <b>BASS</b>";
                randomDialogs[1].NPCDialog = "Awwwwhaha yeah man these beats are HOT!";
                randomDialogs[2].NPCDialog = "The Shenheizzer doesn't like to brag.... but the Shenheizzer is absolutely KILLING IT RIGHT NOW.";
                randomDialogs[3].NPCDialog = "Why is the Shenheizzer THE Shenheizzer?! Why is the bass so PUMPIN'???";
                randomDialogs[4].NPCDialog = "The Shenheizzer's favorite season is spring. I LOVE those adorable baby birds.";
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
            case BasicCharacters.Bartender:
                matName = "CharBasic_Bartender" + themeName;
                break;
            case BasicCharacters.GingerMan:
                matName = "CharBasic_Ginger" + themeName;
                break;
            case BasicCharacters.Jock:
                matName = "CharBasic_Jock" + themeName;
                break;
            case BasicCharacters.PlaidMan:
                matName = "CharBasic_PlaidShirt" + themeName;
                break;
            case BasicCharacters.PoliceMan:
                matName = "CharBasic_PoliceMan" + themeName;
                break;
            case BasicCharacters.Shenheizzer:
                matName = "CharBasic_Schenheizzer" + themeName;
                break;
        }
        mat = (Material)Resources.Load(matName, typeof(Material));
        transform.FindChild("Mesh").GetComponent<Renderer>().material = mat;
    }
	
	void Update () 
    {
	    
	}
}
