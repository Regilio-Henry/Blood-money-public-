using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class AbilityContainer : MonoBehaviour
{
    public int health;
    public int currentHealth;
    public Text description;
    public Text itemname;
    public Text challenge;
    public VideoPlayer vp_VideoPlayerRef;
    public Ability[] abilities;
    public GameObject abilityHolder;
    public GameObject cellPrefab;
    public GameObject cuePrefab;
    public GameObject cueHolder;
    public GameObject healthSlot;
    public GameObject healthHolder;
    public GameObject equipButton;
    public GameObject gameController;

    int currentIndex;
    public ToggleGroup toggleGroup;
    public List<Ability> selectedAbilites = new List<Ability>();


    private void Awake()
    {
        gameController = GameObject.Find("GameController");
    }

    // Start is called before the first frame update
    void Start()
    {
        //load ability holder from file
        //if (selectedAbilites != null)
        //{
        //    foreach(Ability ability in selectedAbilites)
        //    {
        //        ability.
        //    }
        //}

        //Set health total from game and update health
        health = gameController.GetComponent<playerStats>().healthTotal;
        currentHealth = health;
        updateHealth();
        

        //Populate the list of abilities
        for(int i = 0; i < abilities.Length; i++)
        {
            Ability ability = abilities[i];
            GameObject cell = Instantiate(cellPrefab, abilityHolder.transform);
            Toggle toggle = cell.GetComponent<Toggle>();
            toggle.group = toggleGroup;
            cell.transform.GetChild(0).GetComponent<Image>().sprite = ability.ItemSprite;
            cell.transform.GetComponent<cellIndex>().index = i;
            cell.transform.GetComponent<cellIndex>().locked = !gameController.transform.GetComponent<ChallengeBuilder>().challenges[ability.challengeIndex].ChallengeComplete;
            cell.transform.GetComponent<cellIndex>().LockCheck();

            if (i == 0)
            {
                toggle.isOn = true;
                PlayVideo(abilities[i].videoName);
            }
        }
    }


    //Sets the current selection for the ability
    public void selectAbility()
    {
        var currentAbility = abilities[currentIndex];
        var cell = abilityHolder.transform.GetChild(currentIndex);

        if(currentAbility.cost < currentHealth && !cell.GetComponent<cellIndex>().locked && !cell.GetComponent<cellIndex>().selected)
        {
            selectedAbilites.Add(currentAbility);
            currentHealth -= currentAbility.cost;
            updateHealth();
            cell.GetComponent<cellIndex>().selected = true;
            cell.GetComponent<cellIndex>().checkSelection();
        }
        else if (cell.GetComponent<cellIndex>().selected)
        {
            selectedAbilites.Remove(currentAbility);
            currentHealth += currentAbility.cost;
            updateHealth();
            cell.GetComponent<cellIndex>().selected = false;
            cell.GetComponent<cellIndex>().checkSelection();

        }
    }

    //Sets the currently selected ability
    public void getIndexFromChild(int i)
    {

        displayAbility(abilities[i]);
        currentIndex = i;
    }

    //Updates the health in the ui
    void updateHealth()
    {
        foreach (Transform child in healthHolder.transform)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < currentHealth; i++)
        {
            Instantiate(healthSlot, healthHolder.transform);
        }

        gameController.GetComponent<playerStats>().playerHealthTotal = currentHealth;

    }

    //Dispalys the current abilities description, name and video
    void displayAbility(Ability ability)
    {
        foreach (Transform child in cueHolder.transform)
        {
            Destroy(child.gameObject);
        }

        description.text = ability.itemDescription;
        itemname.text = ability.weaponName;
        challenge.text = gameController.GetComponent<ChallengeBuilder>().challenges[ability.challengeIndex].challengeName;
        PlayVideo(ability.videoName);

        for(int i = 0; i < ability.cost; i++)
        {
            Instantiate(cuePrefab, cueHolder.transform);
        }

    }

    //Plays a video
    public void PlayVideo(string videoName)
    {
       VideoClip clip = Resources.Load<VideoClip>(videoName) as VideoClip;
       vp_VideoPlayerRef.clip = clip;
       vp_VideoPlayerRef.Play();
    }


    // Update is called once per frame
    void Update()
    {
        if (abilityHolder.transform.GetChild(currentIndex).GetComponent<cellIndex>().selected)
        {
            equipButton.transform.GetChild(0).GetComponent<Text>().text = "UnEquip";
        }
        else
        {
            equipButton.transform.GetChild(0).GetComponent<Text>().text = "Equip";
        }
    }
}
