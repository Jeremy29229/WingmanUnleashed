using UnityEngine;
using System.Collections;

public class NPCHips : NPCControlScript 
{
	public HipsCharacters CharacterType = HipsCharacters.BlondeGirl;

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
			case HipsCharacters.AgingWoman:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameFeminine();
                randomDialogs[0].NPCDialog = "Harumph.";
				randomDialogs[1].NPCDialog = "They say the drinks at this party are imported.";
				randomDialogs[2].NPCDialog = "My niece invited me here. She's a doctor you know.";
				randomDialogs[3].NPCDialog = "<i>Passive agressive cough</i>";
				randomDialogs[4].NPCDialog = "Did you know my son in law is a chef? Why they don't have him cater <i>every</i> party is beyond me.";
				break;
			case HipsCharacters.BlondeGirl:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameFeminine();
				randomDialogs[0].NPCDialog = "You see that guy? He's totes checking me out. NO don't look! Play it cool!";
				randomDialogs[1].NPCDialog = "O.M.G. My selfie in this outfit already has like 50 ups.";
				randomDialogs[2].NPCDialog = "#party #datmusictho #someweirdokeepstalkingtome #whoevenisthisguy";
				randomDialogs[3].NPCDialog = "<i>Whispers:</i>Did you hear the Shenheizzer's here? Like. I can't even. I can't. I can. Not.";
				randomDialogs[4].NPCDialog = "Sometimes I feel like the people at these parties know, like, two whole dances. It's super weird.";
				break;
			case HipsCharacters.DarkHairWoman:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameFeminine();
				randomDialogs[0].NPCDialog = "That economy huh?";
				randomDialogs[1].NPCDialog = "Do you always just walk up to people, nothing to say, and wait for them to talk to you?";
				randomDialogs[2].NPCDialog = "I'm pretty sure you're not supposed to be here. But then, I don't really <i>want</i> to be here. So whatever.";
				randomDialogs[3].NPCDialog = "The food at this party is so good. I'm thinking about stuffing some in my pockets for later.";
				randomDialogs[4].NPCDialog = "I'm so glad they created those bigger-on-the-inside-pockets. Where else would I put my purse?";
				break;
			case HipsCharacters.GingerWoman:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameFeminine();
				randomDialogs[0].NPCDialog = "Did you see that guy over there? In the glasses? So cute. So cute.";
				randomDialogs[1].NPCDialog = "The day they outlawed laser eye surgery was historic day.";
				randomDialogs[2].NPCDialog = "Can you believe? There was a guest using the <i>p word!</i>";
				randomDialogs[3].NPCDialog = "So I was reading this book and the characters were at a party like this and they all died. I hope we don't all die.";
				randomDialogs[4].NPCDialog = "Excuse me, I seem to have lost my phone number, can I please borrow yours? ... That was a joke. Ha.";
				break;
			case HipsCharacters.GothWoman:
                conversationInformation.GetComponent<Interactable>().InteractableName = NameGenerator.GenerateRandomFullNameFeminine();
				randomDialogs[0].NPCDialog = "<i>Self loathing intensifies</i>";
				randomDialogs[1].NPCDialog = "You ever hike up in the mountains and just howl alone to the party moon?";
				randomDialogs[2].NPCDialog = "Ever seen someone carry around their own dismembered leg?";
				randomDialogs[3].NPCDialog = "This party is alright. You know, if you're into dancing in groupings of random strangers.";
				randomDialogs[4].NPCDialog = "I know you. I could tell the bouncers about you right now. But I like rebels.";
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
            case HipsCharacters.AgingWoman:
                matName = "CharHip_AgingWoman" + themeName;
                break;
            case HipsCharacters.BlondeGirl:
                matName = "CharHip_BlondeGirl" + themeName;
                break;
            case HipsCharacters.DarkHairWoman:
                matName = "CharHip_GreenSweater" + themeName;
                break;
            case HipsCharacters.GingerWoman:
                matName = "CharHip_Turtleneck" + themeName;
                break;
            case HipsCharacters.GothWoman:
                matName = "CharHip_Goth" + themeName;
                break;
        }
        mat = (Material)Resources.Load(matName, typeof(Material));
        transform.FindChild("Mesh").GetComponent<Renderer>().material = mat;
    }

	void Update()
	{

	}
}
