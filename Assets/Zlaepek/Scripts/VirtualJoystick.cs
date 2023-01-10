using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// ref: https://gist.github.com/cwmagnus/ffeb9b0beb452e00ccbdaebc35a73928
/// Virtual joystick for mobile joystick control
/// </summary>
public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image backPanel;
    [SerializeField] private Image knob;
    [SerializeField] private GameObject positionFocus0LeftUp;
    [SerializeField] private GameObject positionFocus3LeftDown;
    [SerializeField] private GameObject positionFocus1RightUp;
    [SerializeField] private GameObject positionFocus2RightDown;

    public Vector3 InputDirection { get; set; }

    /// <summary>
    /// Drag the knob of the joystick.
    /// </summary>
    /// <param name="pointerEventData">Data from the touch.</param>
    public virtual void OnDrag(PointerEventData pointerEventData)
    {
        Vector2 position = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (backPanel.rectTransform,
                pointerEventData.position,
                pointerEventData.pressEventCamera,
                out position))
        {
            // Get the touch position
            position.x = (position.x / backPanel.rectTransform.sizeDelta.x);
            position.y = (position.y / backPanel.rectTransform.sizeDelta.y);

            // Calculate the move position
            float x = (backPanel.rectTransform.pivot.x == 1) ?
                position.x * 2 + 1 : position.x * 2 - 1;
            float y = (backPanel.rectTransform.pivot.y == 1) ?
                position.y * 2 + 1 : position.y * 2 - 1;

            // Get the input position
            InputDirection = new Vector3(x, 0, y);
            InputDirection = (InputDirection.magnitude > 1) ?
                InputDirection.normalized : InputDirection;

            // Move the knob
            knob.rectTransform.anchoredPosition =
                new Vector3(InputDirection.x * (backPanel.rectTransform.sizeDelta.x / 3),
                    InputDirection.z * (backPanel.rectTransform.sizeDelta.y / 3));

            // Lit position focus (direction)
            positionFocus(InputDirection.x * (backPanel.rectTransform.sizeDelta.x / 3), InputDirection.z * (backPanel.rectTransform.sizeDelta.y / 3));
        }
    }

    /// <summary>
    /// Lit position focus. (direction)
    /// </summary>
    /// <param name="x">x position</param>
    /// <param name="y">y position</param>
    private void positionFocus(float x, float y)
    {
        if (x > 0)
        {
            if (y > 0)
            {
                positionFocus0LeftUp.SetActive(false);
                positionFocus3LeftDown.SetActive(false);
                positionFocus1RightUp.SetActive(true);
                positionFocus2RightDown.SetActive(false);
            }
            if (y < 0)
            {
                positionFocus0LeftUp.SetActive(false);
                positionFocus3LeftDown.SetActive(false);
                positionFocus1RightUp.SetActive(false);
                positionFocus2RightDown.SetActive(true);
            }
        }
        if (x < 0)
        {
            if (y > 0)
            {
                positionFocus0LeftUp.SetActive(true);
                positionFocus3LeftDown.SetActive(false);
                positionFocus1RightUp.SetActive(false);
                positionFocus2RightDown.SetActive(false);
            }
            if (y < 0)
            {
                positionFocus0LeftUp.SetActive(false);
                positionFocus3LeftDown.SetActive(true);
                positionFocus1RightUp.SetActive(false);
                positionFocus2RightDown.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Click on the knob.
    /// </summary>
    /// <param name="pointerEventData">Data from the touch.</param>
    public virtual void OnPointerDown(PointerEventData pointerEventData)
    {
        OnDrag(pointerEventData);
    }

    /// <summary>
    /// Click off the knob.
    /// </summary>
    /// <param name="pointerEventData">Data from the touch.</param>
    public virtual void OnPointerUp(PointerEventData pointerEventData)
    {
        InputDirection = Vector3.zero;
        knob.rectTransform.anchoredPosition = Vector3.zero;
        positionFocus0LeftUp.SetActive(false);
        positionFocus3LeftDown.SetActive(false);
        positionFocus1RightUp.SetActive(false);
        positionFocus2RightDown.SetActive(false);
    }
}
