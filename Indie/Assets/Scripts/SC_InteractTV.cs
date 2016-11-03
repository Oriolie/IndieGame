﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SC_InteractTV : MonoBehaviour {

    public GameObject action;
    SC_TV TVScript;
    Text actionText;
    int currentAction;  // l'action sélectionnée par le joueur. 1 = allumer/éteindre ; 2 = pousser
    bool canInteract;   // à true lorsque le joueur peut interagir avec cet élément
    int player = 0;     // la variable qui va servir à savoir à quel joueur ce script appartient.
    bool isAlive = true;

	// Use this for initialization
	void Start () {
        actionText = action.GetComponent<Text>();
        currentAction = 1;
	}
	
    public void setPlayer()
    {
        player++;
    }

    public void setAliveStatus(bool status)
    {
        isAlive = status;
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetButtonDown("ActionJ"+player) && canInteract && isAlive)  // lorsqu'on appuie sur la touche action
        {
            switch (currentAction)      // un switch pour agir en fonction de l'action
            {
                case 1:
                    if (TVScript.isPlayingSound)    // si la TV est en train de faire du bruit
                    {
                        TVScript.Off();
                        actionText.text = "Allumer";
                    }
                    else
                    {
                        TVScript.On();
                        actionText.text = "Eteindre";
                    }
                    break;

                case 2:
                    TVScript.Push(new Vector2(TVScript.gameObject.transform.position.x - transform.position.x, TVScript.gameObject.transform.position.y - transform.position.y).normalized);
                    break;

                default:
                    break;
            }
            
        }

        if (Input.GetButtonDown("ChangeActionRightJ" + player) && canInteract && isAlive)
        {
            if (currentAction < 2)  // avec ici 2 actions possibles. Pour N actions, mettre "currentAction < N"
            {
                currentAction++;
            }
            else
            {
                currentAction = 1;  // retour à la première action
            }
            NewAction();
        }
        if (Input.GetButtonDown("ChangeActionLeftJ" + player) && canInteract && isAlive)
        {
            if (currentAction > 1)
            {
                currentAction--;
            }
            else
            {
                currentAction = 2;  // retour à la cernière action
            }
            NewAction();
        }

    }

    void NewAction ()
    {
        switch (currentAction)
        {
            case 1:
                if (TVScript.isPlayingSound)
                {
                    actionText.text = "Eteindre";
                }
                else
                {
                    actionText.text = "Allumer";
                }
                    break;

            case 2:
                actionText.text = "Pousser";
                break;

            default:
                break;
        }
    }

    public void CanIntertact(SC_TV script, bool isPlayingSound)
    {
        TVScript = script;
        canInteract = true;
        //currentAction = 1;

        /*if (isPlayingSound)
        {
            actionText.text = "Eteindre";
        }
        else
        {
            actionText.text = "Allumer";
        }*/
        NewAction();
        action.SetActive(true);
    }

    public void CanNotIntertact()
    {
        canInteract = false;
        action.SetActive(false);
    }
}
