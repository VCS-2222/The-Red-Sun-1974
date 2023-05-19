using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerLoseState : MonoBehaviour
{
    [Header("UI Contents")]
    public Canvas deathScreen;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI content;
    [SerializeField] Button buttonToSelect;

    string titleContext;
    string topicContext;

    private void Awake()
    {
        deathScreen.enabled = false;
    }

    public void EnableLoseState(string deathType)
    {
        deathScreen.enabled = true;
        Time.timeScale = 0.001f;
        buttonToSelect.Select();

        if (deathType == "Caught by Guard")
        {
            titleContext = "Felon caught! Now rotting!";
            topicContext = "Happy cheers and good news comrades! A felon who's escaped from the bronze mines in Area 12 has been caught! " +
                           "The brave Guard on patrol chased and tackled the monster, put him in handcuffs and now is waiting for the death penalty! " +
                           "In other news, local man caught smuggling ducks into district 2, for all the people that don't know, back in 1945 the Japanese used ducks infected with the Bubonic plague to " +
                           "poison our population and take over our golden fields and beautiful women! " +
                           "But it has been nearly 30 years since the events! Should we forgive them and start importing ducks into cities again? Or should we leave them outside of out concrete walls?";

            title.text = titleContext;
            content.text = topicContext;
        }

        if (deathType == "Shot by Guard")
        {
            titleContext = "Shocking brutal force applied by our Guards to stop felon";
            topicContext = "The felon that escaped Area 12 did not go far comrades! A guard was on duty and the monster threated their life, " +
                           "spouting horrible language and shacking a weapon at them. The Guard in question was new to the workforce and was absolutely terrified " +
                           "of the hardened criminal and accidentally fired uppon the convict. " +
                           "In other news, cars are starting to modernise and have slight design changes that do not make the leaders of this city happy, they say " +
                           "'Where is the modesty? Where is the equality in a man owning a gold car and another a chariot?'. Others strongly agree, but a group of people believe that the " +
                           "Automobile industry in the city and in the country as a whole should evolve with the rest of the world. They say 'It's beneficial to the economy!' and " +
                           "'So what if my neighbour has some adjustments on his Zana? It's insane that someone should care that much! " +
                           "If you yourself believe that our cars need to step up in the world please go to your nearest voting cabinet and write 'We need new cars' on the voting paper!";

            title.text = titleContext;
            content.text = topicContext;
        }

        if (deathType == "Hit by Car")
        {
            titleContext = "Guts! Guts everywhere!";
            topicContext = "An escaped convict from Area 12 had somehow managed to get out of the mines! The felon did try to escape some Guards that were chasing him but accidentally or not " +
                           "he ran on a road and got him by a car at full speed, the scene was horrible and multiple people has to have medical assistance after negative reactions from the gore! " +
                           "now the city may rest knowing that another monster is dead but was there not another way? If any escaped convicts are reading this, please, for the sake of Communist please go the nearest " +
                           "Police station near you and surrender peacefully! You can redeem yourself! We are all human! " +
                           "In other news, West Germany beat us in the World Cup of 1974! We placed second after the Netherlands and our Communist brother Poland! " +
                           "Don't let the TPFK soul die on the pitch after a small loss, we will win next cup!";

            title.text = titleContext;
            content.text = topicContext;
        }

        if (deathType == "Hit by Train")
        {
            titleContext = "A sad sight...";
            topicContext = "Today an escaped convict of the mines of Area 12 was seen by comrades walk in front of a train. " +
                           "This is presumed by the people as a suicide, as after the train stopped after the collision a diary was found " +
                           "on the felon. He had a sad life and with barely any hope as read from his diary, this is an absolute heart-break to " +
                           "the whole city. As even the worse criminals get a fair share of mental help from the government. Please if you feeel alone " +
                           "or feel hopeless and aimless in life, call us at 1-800-helpme and we will try our best to save you. In other news, " + 
                           "pro LGBT laws are getting rewriten and some outright banned? In Area 5 the Govener has taken down some " +
                           "laws to do with our fellow gay comrades. They speaking of, 'It doesn't make sense why we should have this' and " +
                           "'Well damn I believe it to just be somewhat unnatural, that's what my grandparents told us from the good late 1800s' " +
                           ", this being their reasoning. On the other hand most people believe that 'We need these laws in place in case the " +
                           "government tried something funny!' and 'We are human, all equals in this sociaty right? Then why are we baing punished for " +
                           "no crimes?', but what do you think? If you believe that are LGBT comrades should keep their protection laws or should we dim down some?";

            title.text = titleContext;
            content.text = topicContext;
        }

        if (deathType == "Void")
        {
            titleContext = "What!?";
            topicContext = "How'd you do that? I don't get it? Whether if you wanted to do that or not it's happened " +
                           "I'm sorry for that... Uhhhhhhhh, just go back to the main menu alright? Maybe retry?";

            title.text = titleContext;
            content.text = topicContext;
        }
    }
}
