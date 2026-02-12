using UnityEngine;
using UnityEngine.EventSystems;

public class MobileInput : MonoBehaviour,
    IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform joystickBG;
    public RectTransform joystickHandle;
    public float joystickRadius = 60f;

    private PlayerMove player;
    private float lastDirection = 0f;

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

        float direction = pos.x / joystickRadius;

        if (player != null)
        {
            if (direction > 0.5f && lastDirection <= 0.5f)
            {
                player.MoveRight();
            }
            else if (direction < -0.5f && lastDirection >= -0.5f)
            {
                player.MoveLeft();
            }
        }

        lastDirection = direction;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        joystickHandle.anchoredPosition = Vector2.zero;
        lastDirection = 0f;
    }

    public void JumpButton()
    {
        if (player != null)
            player.Jump();
    }
}
