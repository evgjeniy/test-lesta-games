using Ecs.Components.Events;
using Ecs.Utilities;
using TMPro;
using UnityEngine;
using Voody.UniLeo;

[RequireComponent(typeof(Animator))]
public class StartUIAnimator : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownText;
    
    public void ChangeCountDownText(string text) => countdownText.text = text;

    public void EnablePlayerInput() => WorldHandler.GetWorld().SendMessage<PlayerEnableInputEvent>();
}