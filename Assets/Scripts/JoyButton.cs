using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
   public bool Pressed { get; private set; }

   private Button _button;

   public void SubscribeButtonClick(Action func) => _button.onClick.AddListener(() => func());

   public void UnSubscribeAllButtonClick() => _button.onClick.RemoveAllListeners();

   public void OnPointerDown(PointerEventData eventData)
   {
      Pressed = true;
   }

   public void OnPointerUp(PointerEventData eventData)
   {
      Pressed = false;
   }

   private void Awake()
   {
      _button = GetComponent<Button>();
   }
}
