using UnityEngine;

public class Clue : MonoBehaviour {


    public PortalManager portalManager;
    public TMPro.TextMeshPro clueText;

    private int blin;
    private string[] cluesRed;
    private string[] cluesBlue;
    private string[] cluesGreen;
    private string[] cluesYellow;



    void Start() {

        ClueTexts();
        blin = portalManager.p.Length;

    }




    public void SetupClues(int behaviour) {

        if (behaviour == 0) {
            // red good
            clueText.text = cluesRed[Random.Range(0, cluesRed.Length)];
            // blue bad
        } else if (behaviour == 1) {
            if (Random.Range(0, 2) == 0)
                clueText.text = cluesRed[Random.Range(0, cluesRed.Length)];
            else
                clueText.text = cluesGreen[Random.Range(0, cluesGreen.Length)];
            // red good
            // blue bad
            // green good
        } else if (behaviour == 2) {
            if (Random.Range(0, 2) == 0)
                clueText.text = cluesRed[Random.Range(0, cluesRed.Length)];
            else
                clueText.text = cluesYellow[Random.Range(0, cluesYellow.Length)];
            // red good
            // blue bad
            // green bad
            // yellow good
        } else {
            if (blin == 2) {
                clueText.text = cluesBlue[Random.Range(0, cluesBlue.Length)];
            } else {
                if (Random.Range(0,2) == 0)
                    clueText.text = cluesBlue[Random.Range(0, cluesBlue.Length)];
                else
                    clueText.text = cluesGreen[Random.Range(0, cluesGreen.Length)];
            }

            /*
             * else condition is
             * blue good
             * red bad
             * green good
             * yellow bad
            */
        }


    }















    private void ClueTexts() {

        cluesRed = new string[]{
            "Day 2: I've survived! Weird creatures follow me! Which way? I saw a RED wormhole not far...",
            "Day 7: My nose is bleeding all of sudden. Must hurry until my suit doesn't become soaked in RED",
            "Day 18: Must eat. Found some RED berries. Not sure if they're poisonous.",
            "Day 23: Monsters attacked at night. Maybe they were attracted by those RED indicators on my suit.",
            "Day 33: So lonely. I think I may go insane from the stress. I'm seeing RED.",
            "Day 64: I don't know who I am anymore? Do I exist? Does a rainbow have a RED color in it?",
            "Day 96: CODE RED! CODE RED! Got almost killed today.",
            "Day 111: Am I going mad? RED RUM! RED RUM!",
            "Day 145: Found one RED Skittle in a pocket. I'm ok now :D",
            "Day 317: Witnessed the birth of a huge RED star somewhere in the distance. Beautiful",
            "Day 10923: Spent the day by remembering the songs from that vintage band called \"RED Hot Chilly\".... How was it?",
            "Day ???: Ok. I just can't carry on anymore. This last RED wormhole will be the last I'm walking through."
        };

        cluesBlue = new string[] {
            "Day 2: My head still hurts after the crash. I wonder if it's safe here. Will investigate that BLUE portal.",
            "Day 7: Noticed a small BLUE creature today. It was playing in the grass.",
            "Day 18: Made friends with a little BLUE creature. Fed it a bit of my ration.",
            "Day 23: Despite all the dangers - I like it here. The sky above is so BLUE.",
            "Day 33: Starting to feel lonely. I wish that little BLUE critter followed me. :(",
            "Day 64: Lonely. I think I start understanding BLUES music.",
            "Day 96: Witnessed how two giant monsters fight today. They have BLUE blood. Bet it's toxic.",
            "Day 111: I think my skin is becoming BLUE-ish, because there's not much natural sunlight here.",
            "Day 145: Discovered a vein of some BLUE mineral today. It was sparkly.",
            "Day 317: I would kill for a bowl of BLUEberries. Literally.",
            "Day 10923: Decided to be super-creative today, so filled the log with funky colors, not the default BLUE.",
            "Day ???: I see a BLUE wormhole in front of me and my astronaut sense in tingling. What if?"
        };

        cluesGreen = new string[] {
            "Day 2: I think I should start to write a diary or log. Will do as I pass this GREEN wormhole",
            "Day 7: Whoa! It's so GREEN here! Feels like a jungle.",
            "Day 18: These GREEN slimes crawling around give me creeps. So ugly!",
            "Day 23: Ha, and they called Amazon jungles \"GREEN Hell\". Sheesh!",
            "Day 33: Ok. Now I kinda miss the GREEN scenery from before.",
            "Day 64: I saw a patch of GREEN between the fiery rocks. That must be the Wormhole.",
            "Day 96: Suddenly I had nostalgia kicking in: remembered that time when my parents presented me a GREEN sweater for Christmas.",
            "Day 111: According to my calculations it's actually Christmas back at home. Wonder if I can find a GREEN tree here somewhere?",
            "Day 145: Encountered a strange creature today. It spit with GREEN liquid that started to melt my glove. Washed it off quickly.",
            "Day 317: You know, they say: \"Grass is GREENer on the Other Side\". Well, I'm on the Otherside. It's not.",
            "Day 10923: Found my old laptop where I used to record logs. After all these years the indicator is still flashing GREEN. I'm doomed.",
            "Day ???: What's that? A GREEN Wormhole. But it gives a strange feeling. Not like the others. Wait, don't you think..."
        };

        cluesYellow = new string[] {
            "Day 2: I need to get out of here. Should I pass through this YELLOW wormhole?",
            "Day 7: It feels so hot here, is it because of the sun? Maybe I should go through this YELLOW wormhole.",
            "Day 18: The dark, the dark, I would like to see some lights. Oh why - oh why… Oh wow! that's a very fancy looking YELLOW candle light.",
            "Day 23: You know what they say, “This storm never last, but the YELLOW sun keeps shining”. I really have to get out of this place.",
            "Day 33: It takes two flints to make a bright burning YELLOW fire.",
            "Day 64: I-Is that a patch of yellow there between those rocks? That is certainly a wormhole",
            "Day 96: Encountered a very strange YELLOW substance today. It’s just lying down there on the ground. I wonder what it is.",
            "Day 111: Gods help me, this place is so hot. It feels like I’m in a desert… I wish I could have a nice juicy YELLOW banana to eat right now.",
            "Day 145: As I look on to the sky tonight, I saw a bright glowing YELLOW star in the night sky. How beautiful",
            "Day 317: Roses are red, violets are blue. Then what in the world are YELLOW supposed to be representing?",
            "Day 10923: A nice cold YELLOW lemon juice would seal the deal right about now. Haven’t had it in… 30 years? How long am I trapped here?",
            "Day ???: I saw a bright YELLOW wormhole today. Do you think…?"
        };
    }






}
