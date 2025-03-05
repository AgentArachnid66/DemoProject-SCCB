using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct InteractContext
{
    private struct ContextLog
    {
        public string Message;
        public int Severity;

        public ContextLog(string msg, int v) : this()
        {
        }
    }

    public void InitialiseContext()
    {
        Messages = new List<ContextLog>();
    }

    public void AddMessage(string Msg)
    {
        Messages.Add(new ContextLog(Msg, 0));
        Debug.Log(Msg);
    }

    public void AddWarning(string warning)
    {
        Messages.Add(new ContextLog(warning, 1));
        Debug.LogWarning(warning);
    }

    public bool IsValidContext()
    {
        return Messages.Count > 0;
    }

    private List<ContextLog> Messages;
}

public interface IInteractableInterface
{
    bool IsInteractable(InteractionComponent interactionComponent);
    bool AttemptInteraction(InteractionComponent interactionComponent, ref InteractContext interactContext);

    void GetInteractContext(InteractionComponent interactionComponent, out InteractContext interactContext);
}
