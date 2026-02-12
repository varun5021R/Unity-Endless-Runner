using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform joystickBG;
    public RectTransform joystickHandle;
    public float joystickRadius = 60f;

    private Vector2 inputVector;
    private PlayerMove player;

    void Start()
    {
        player = FindFirstObjectByType<PlayerMove>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            joystickBG,
            eventData.position,
            eventData.pressEventCamera,
            out pos
        );

        pos = Vector2.ClampMagnitude(pos, joystickRadius);
        joystickHandle.anchoredPosition = pos;
        inputVector = pos / joystickRadius;

        if (player != null)
        {
            if (inputVector.x > 0.5f)
                player.SendMessage("HandleKeyboardInput", SendMessageOptions.DontRequireReceiver);

            if (inputVector.x < -0.5f)
                player.SendMessage("HandleKeyboardInput", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        inputVector = Vector2.zero;
        joystickHandle.anchoredPosition = Vector2.zero;
    }

    public void JumpButton()
    {
        if (player != null)
            player.Jump();
    }
}
