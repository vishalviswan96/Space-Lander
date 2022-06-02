using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    /// <summary>
    /// This Script is responsible for handeling the inputs. The evenTriggers call the PlayerController and execute the movements
    /// according the the given UI button pressed.
    /// 
    /// </summary>

    private PlayerController player_Controller;

    [SerializeField] private float required_HoldTime;
    [SerializeField] private Image fill_Image;

    private bool pointer_Down;
    private float pointer_Down_Timer;
   
    private bool pointer_Check;
    public bool button_Active;

    private void Start()
    {
        player_Controller = FindObjectOfType<PlayerController>();
        pointer_Down_Timer = required_HoldTime;
        pointer_Check = true;
        button_Active = true;
    }

    void Update()
    {
        if (pointer_Check)
        {
            if (pointer_Down)
            {
                pointer_Down_Timer -= Time.deltaTime;
                if (pointer_Down_Timer <= 0)
                {

                    Debug.Log(pointer_Down_Timer);
                    pointer_Check = false;
                    pointer_Down_Timer = 0f;
                    DeactivateButton();
                }

                fill_Image.fillAmount = pointer_Down_Timer / required_HoldTime;
            }
        }
        
    }

    public void OnRightDown()
    {
        if (!button_Active)
            return;

        player_Controller.HorizontalInput(1f);
        pointer_Down = true;
    }
    public void OnRightUp()
    {
        if (!button_Active)
            return;
        player_Controller.HorizontalInput(0f);
        pointer_Down = false;
    }
    public void OnLeftDown()
    {
        if (!button_Active)
            return;
        player_Controller.HorizontalInput(-1f);
        pointer_Down = true;
    }
    public void OnLeftUp()
    {
        if (!button_Active)
            return;
        player_Controller.HorizontalInput(0f);
        pointer_Down = false;
    }
  
    private void DeactivateButton()
    {
        Debug.Log("Inter");
        player_Controller.HorizontalInput(0f);
        button_Active = false;
    }

    public void ResetButton()
    {
        pointer_Down = false;
        button_Active = true;
        pointer_Down_Timer = required_HoldTime;
        fill_Image.fillAmount = pointer_Down_Timer / required_HoldTime;
    }

}
