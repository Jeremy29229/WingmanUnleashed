using UnityEngine;
using System.Collections;

public class NPCBouncer : NPCControlScript
{
    public BouncerType BouncerType = BouncerType.Angry;
    public BouncerVariant Variant = BouncerVariant.One;

    void Start()
    {
        Wander = false;
        base.StartSetup();
    }

    protected override void SetupRandomDialogue()
    {
        GameObject conversationInformation = gameObject.transform.FindChild("ConversationInformation").gameObject;
        base.SetupRandomDialogue();
        switch (BouncerType)
        {
            case BouncerType.Angry:
                randomDialogs[0].NPCDialog = "Let me guess, someone stole your mint.";
                randomDialogs[1].NPCDialog = "My cousin's out bouncing fight clubs and what do I get? Party duty.";
                randomDialogs[2].NPCDialog = "No loitering. Oh, wait, this is a party. Never mind.";
                randomDialogs[3].NPCDialog = "Hands to yourself.";
                randomDialogs[4].NPCDialog = "People keep calling us Tigger or something. I don't get the joke. It isn't funny.";
                break;
            case BouncerType.Happy:
                randomDialogs[0].NPCDialog = "They say The Shenheizzer broke the sound barrier... With his voice!";
                randomDialogs[1].NPCDialog = "I need to ask you to start partying harder. You're making people nervous.";
                randomDialogs[2].NPCDialog = "This party is under my protection. You have a good time, now.";
                randomDialogs[3].NPCDialog = "I don't know about you, but I am having a blast. A blast!";
                randomDialogs[4].NPCDialog = "These party guests are so polite. I love you guys.";
                break;
            case BouncerType.Stoic:
                randomDialogs[0].NPCDialog = "I used to be a partier like you... What, did you expect me to say more?";
                randomDialogs[1].NPCDialog = "I mostly deal with petty party crashers and drunken fisticuffs.";
                randomDialogs[2].NPCDialog = "Heard about you and your flowered words.";
                randomDialogs[3].NPCDialog = "Pibs think we need their laws. Pffft.";
                randomDialogs[4].NPCDialog = "Don't have money for drinks? You can always pay with your blood.";
                break;
        }
        conversationInformation.GetComponent<Interactable>().InteractableName = "Bouncer";
        conversationInformation.GetComponent<Interactable>().Action = "Talk to";
    }

    protected override void SetThemeOutfit(PartyTheme theme)
    {
        //Will add party theme specific stuff outfit logic if bouncers every have theme specific outfits
        //For now this method is pointless to have called after the first material setup
        Material mat;
        string matName = "";
        switch (BouncerType)
        {
            case BouncerType.Angry:
                matName = "Bouncer_Angry";
                break;
            case BouncerType.Happy:
                matName = "Bouncer_Happy";
                break;
            case BouncerType.Stoic:
                matName = "Bouncer_Stoic";
                break;
        }

        switch (Variant)
        {
            case BouncerVariant.One:
                matName += "1";
                break;
            case BouncerVariant.Two:
                matName += "2";
                break;
            case BouncerVariant.Three:
                matName += "3";
                break;
            case BouncerVariant.Four:
                matName += "4";
                break;
        }
        mat = (Material)Resources.Load(matName, typeof(Material));
        transform.FindChild("Mesh").GetComponent<Renderer>().material = mat;
    }

    void Update()
    {

    }
}
