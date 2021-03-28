using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{
    [SerializeField] MenuButton[] buttonsOnMenu;
    private int currentButton = 0;
    private int lastButton = 0;
    void Start()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Move;
        entry.callback.AddListener((data) => { MoveArrows((AxisEventData)data); });
        trigger.triggers.Add(entry);

        entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Submit;
        entry.callback.AddListener((data) => { PressButton((BaseEventData)data); });
        trigger.triggers.Add(entry);

        ResetButtons();
        UpdateButton();
    }

    public void MoveArrows(AxisEventData data)
    {
        if (data.moveVector.y > 0)
        {
            UpButton();
            UpdateButton();
        }
        else if (data.moveVector.y < 0)
        {
            DownButton();
            UpdateButton();
        }
    }

    public void PressButton(BaseEventData data)
    {
        buttonsOnMenu[currentButton].Press();
    }

    private int UpButton()
    {
        lastButton = currentButton;
        if (--currentButton == -1) { currentButton = buttonsOnMenu.Length - 1; }
        return currentButton;
    }

    private int DownButton()
    {
        lastButton = currentButton;
        if (++currentButton == buttonsOnMenu.Length) { currentButton = 0; }
        return currentButton;
    }

    private void UpdateButton()
    {
        buttonsOnMenu[lastButton].Deactivate();
        buttonsOnMenu[currentButton].Activate();
    }

    private void ResetButtons()
    {
        for (int i = 0; i < buttonsOnMenu.Length; i++)
        {
            buttonsOnMenu[i].Deactivate();
        }
    }
}
