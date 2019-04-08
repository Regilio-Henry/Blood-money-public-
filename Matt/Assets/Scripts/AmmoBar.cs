using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{

    public int TotalAmmo;
    public float RechargeRate;
    public Transform AmmoContainer;
    public GameObject AmmoSlot;
    List<GameObject> AmmoSlots = new List<GameObject>();
    float currentAmmo;
    float currentAmount;


    float progressbarvalue;


    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = TotalAmmo;
        for (int i = 0; i < TotalAmmo; i++)
        {
            var slot = Instantiate(AmmoSlot);

            slot.transform.SetParent(AmmoContainer);
            AmmoSlots.Add(slot);
        }
    }

    public void ChangeAmmo(float value)
    {
        currentAmmo += value;
        UpdateAmmo();

    }

    // Update is called once per frame


    public void UpdateAmmo()
    {
        if (currentAmmo >= 0) // if current ammo is more than 0
        {
            for (int i = 0; i < TotalAmmo; i++) // for each ammo OUT OF TOTAL
            {
                if(currentAmmo - 1 >= i) // if current ammo is bigger than i then fill that node
                {
                    AmmoContainer.GetChild(i).GetComponent<Image>().fillAmount = 1;
                }

                else if(i == TotalAmmo - 1)// else if theres only 1 empty then check how much it needs to be filled and fill it
                {                  
                    float ClampedFillAmount = Mathf.Clamp(currentAmmo - i, 0, 1);//get the amount that the node needs to be filled to and clamp it between 0 and 1 as it could go over.
                    AmmoContainer.GetChild(i).GetComponent<Image>().fillAmount = ClampedFillAmount;
                    break;//if its the last one and filled to the right amount break
                }

                else if (TotalAmmo - i >1) // if theres more than one empty
                {
                    AmmoContainer.GetChild(i).GetComponent<Image>().fillAmount = (currentAmmo - i); // fill thecurrent node to the right ammount

                    int AmountToEmpty = (TotalAmmo - i) - 1; // find out how many are empty
                    while (AmountToEmpty > 0) // while the amount to empty is more than 0, empty each one and decrease the amount to empty until it is 0, then break;
                    {
                        AmmoContainer.GetChild(i + AmountToEmpty).GetComponent<Image>().fillAmount = 0; 
                        AmountToEmpty = AmountToEmpty - 1;
                    }
                    break;
                }

            }
        }
        else // ammo = 0 so empty them all
        {
            for (int i = 0; i < TotalAmmo; i++)
            {
                AmmoContainer.GetChild(i).GetComponent<Image>().fillAmount = 0;
            }
        }
    }

    public float GetAmmo()
    {
        return currentAmmo;
    }

    public void RechargeAmmo()
    {
        if (currentAmmo < TotalAmmo)
        {
            ChangeAmmo(.1f * Time.deltaTime);

        }
    }
    public void Update()
    {
        RechargeAmmo();
    }
}
