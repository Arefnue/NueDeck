using System.Collections.Generic;
using NueDeck.Scripts.Card;
using NueDeck.Scripts.Managers;
using UnityEngine;

namespace NueDeck.Scripts.Controllers
{
    //Thanks to Cyan

    // handles hand movements
    public class HandController : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private bool cardUprightWhenSelected = true;
        [SerializeField] private bool cardTilt = true;
        [SerializeField] [Range(0, 5)] private float selectionSpacing = 1;

        private bool updateHierarchyOrder = false;

        [SerializeField] private Vector3 curveStart = new Vector3(2f, -0.7f, 0), curveEnd = new Vector3(-2f, -0.7f, 0);

        [SerializeField] private Vector2 handOffset = new Vector2(0, -0.3f), handSize = new Vector2(9, 1.7f);

        [Header("References")] 
        public Camera cam = null;

        [SerializeField] private Material inactiveCardMaterial = null;

        private Plane plane; // world XY plane, used for mouse position raycasts
        private Vector3 a, b, c; // Used for shaping hand into curve

        public List<CardBase> hand; // Cards currently in hand

        private int selected = -1; // Card index that is nearest to mouse
        private int dragged = -1; // Card index that is held by mouse (inside of hand)
        private CardBase heldCard; // Card that is held by mouse (when outside of hand)
        private Vector3 heldCardOffset;
        private Vector2 heldCardTilt;
        private Vector2 force;
        private Vector3 mouseWorldPos;
        private Vector2 prevMousePos;
        private Vector2 mousePosDelta;

        private Rect handBounds;
        private bool mouseInsideHand;

        private bool showDebugGizmos = true;

        #region Setup

        private void Start()
        {
            InitHand();
        }

        private void InitHand()
        {
            a = transform.TransformPoint(curveStart);
            b = transform.position;
            c = transform.TransformPoint(curveEnd);
            handBounds = new Rect((handOffset - handSize / 2), handSize);
            plane = new Plane(-Vector3.forward, transform.position);
            prevMousePos = Input.mousePosition;
        }
        

        #endregion

        private void Update()
        {
            // --------------------------------------------------------
            // HANDLE MOUSE & RAYCAST POSITION
            // --------------------------------------------------------

            var mousePos = HandleMouseInput(out var count, out var sqrDistance, out var mouseButton);

            // --------------------------------------------------------
            // HANDLE CARDS IN HAND
            // --------------------------------------------------------

            HandleCardsInHand(count, mouseButton, sqrDistance);

            // --------------------------------------------------------
            // HANDLE DRAGGED CARD
            // (Card held by mouse, inside hand)
            // --------------------------------------------------------

            HandleDraggedCardInsideHand(mouseButton, count);

            // --------------------------------------------------------
            // HANDLE HELD CARD
            // (Card held by mouse, outside of the hand)
            // --------------------------------------------------------

            HandleDraggedCardOutsideHand(mouseButton, mousePos);
        }

        #region Methods

        private Vector2 HandleMouseInput(out int count, out float sqrDistance, out bool mouseButton)
        {
            Vector2 mousePos = Input.mousePosition;

            // Allows mouse to go outside game window but keeps cards within window
            // If mouse doesn't need to go outside, could use "Cursor.lockState = CursorLockMode.Confined;" instead
            mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
            mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);

            // Mouse movement velocity
            if (cardTilt) TiltCard(mousePos);

            // Get world position from mouse
            GetMouseWorldPosition(mousePos);

            // Get distance to current selected card (for comparing against other cards later, to find closest)
            GetDistanceToCurrentSelectedCard(out count, out sqrDistance);

            // Check if mouse is inside hand bounds
            CheckMouseInsideHandBounds(out mouseButton);
            return mousePos;
        }

        private void HandleCardsInHand(int count, bool mouseButton, float sqrDistance)
        {
            for (var i = 0; i < count; i++)
            {
                var card = hand[i];
                var cardTransform = card.transform;

                // Set to inactive material if not enough mana required to use card
                card.SetInactiveMaterialState(HandManager.instance.currentMana < card.myProfile.myManaCost, inactiveCardMaterial);

                var noCardHeld = heldCard == null; // Whether a card is "held" (outside of hand)
                var onSelectedCard = noCardHeld && selected == i;
                var onDraggedCard = noCardHeld && dragged == i;

                // Get Position along Curve (for card positioning)
                float selectOffset = 0;
                if (noCardHeld)
                    selectOffset = 0.02f *
                                   Mathf.Clamp01(1 - Mathf.Abs(Mathf.Abs(i - selected) - 1) / (float) count * 3) *
                                   Mathf.Sign(i - selected);

                var t = (i + 0.5f) / count + selectOffset * selectionSpacing;
                var p = GetCurvePoint(a, b, c, t);

                var d = (p - mouseWorldPos).sqrMagnitude;
                var mouseCloseToCard = d < 0.5f;
                var mouseHoveringOnSelected =
                    onSelectedCard && mouseCloseToCard && mouseInsideHand; //  && mouseInsideHand

                // Handle Card Position & Rotation
                //Vector3 cardUp = p - (transform.position + Vector3.down * 7);
                var cardUp = GetCurveNormal(a, b, c, t);
                var cardPos = p + (mouseHoveringOnSelected ? cardTransform.up * 0.3f : Vector3.zero);
                var cardForward = Vector3.forward;

                /* Card Tilt is disabled when in hand as they can clip through eachother :(
                if (cardTilt && onSelectedCard && mouseButton) {
                    cardForward -= new Vector3(heldCardOffset.x, heldCardOffset.y, 0);
                }*/

                // Sorting Order
                if (mouseHoveringOnSelected || onDraggedCard)
                {
                    // When selected bring card to front
                    if (cardUprightWhenSelected) cardUp = Vector3.up;
                    cardPos.z = transform.position.z - 0.2f;
                }
                else
                {
                    cardPos.z = transform.position.z + t * 0.5f;
                }

                // Rotation
                cardTransform.rotation = Quaternion.RotateTowards(cardTransform.rotation,
                    Quaternion.LookRotation(cardForward, cardUp), 80f * Time.deltaTime);

                // Handle Start Dragging
                if (mouseHoveringOnSelected)
                {
                    var mouseButtonDown = Input.GetMouseButtonDown(0);
                    if (mouseButtonDown)
                    {
                        dragged = i;
                        heldCardOffset = cardTransform.position - mouseWorldPos;
                        heldCardOffset.z = -0.1f;
                    }
                }

                // Handle Card Position
                if (onDraggedCard && mouseButton)
                {
                    // Held by mouse / dragging
                    cardPos = mouseWorldPos + heldCardOffset;
                    cardTransform.position = cardPos;
                }
                else
                {
                    cardPos = Vector3.MoveTowards(cardTransform.position, cardPos, 16f * Time.deltaTime);
                    cardTransform.position = cardPos;
                }

                // Get Selected Card
                if (HandManager.instance.canSelectCards)
                {
                    //float d = (p - mouseWorldPos).sqrMagnitude;
                    if (d < sqrDistance)
                    {
                        sqrDistance = d;
                        selected = i;
                    }
                }
                else
                {
                    selected = -1;
                    dragged = -1;
                }

                // Debug Gizmos
                if (showDebugGizmos)
                {
                    var c = new Color(0, 0, 0, 0.2f);
                    if (i == selected)
                    {
                        c = Color.red;
                        if (sqrDistance > 2) c = Color.blue;
                    }

                    Debug.DrawLine(p, mouseWorldPos, c);
                }
            }
        }

        private void HandleDraggedCardOutsideHand(bool mouseButton, Vector2 mousePos)
        {
            if (heldCard != null)
            {
                var cardTransform = heldCard.transform;
                var cardUp = Vector3.up;
                var cardPos = mouseWorldPos + heldCardOffset;
                var cardForward = Vector3.forward;
                if (cardTilt && mouseButton) cardForward -= new Vector3(heldCardTilt.x, heldCardTilt.y, 0);

                // Bring card to front
                cardPos.z = transform.position.z - 0.2f;

                // Handle Position & Rotation
                cardTransform.rotation = Quaternion.RotateTowards(cardTransform.rotation,
                    Quaternion.LookRotation(cardForward, cardUp), 80f * Time.deltaTime);
                cardTransform.position = cardPos;

                HandManager.instance.HighlightCardTarget(heldCard.myProfile.myTargets);

                //if (!canSelectCards || cardTransform.position.y <= transform.position.y + 0.5f) {
                if (!HandManager.instance.canSelectCards || mouseInsideHand)
                {
                    //  || sqrDistance <= 2
                    // Card has gone back into hand
                    AddCardToHand(heldCard, selected);
                    dragged = selected;
                    selected = -1;
                    heldCard = null;

                    HandManager.instance.DeactivateCardHighlights();

                    return;
                }

                PlayCard(mousePos);
            }
        }

        private void PlayCard(Vector2 mousePos)
        {
            // Use Card
            var mouseButtonUp = Input.GetMouseButtonUp(0);
            if (mouseButtonUp)
            {
                //Remove highlights
                HandManager.instance.DeactivateCardHighlights();
                //todo buraları toparla
                if (heldCard.myProfile.myTargets == CardSO.CardTargets.Enemy)
                {
                    RaycastHit hit;
                    var mainRay = LevelManager.instance.mainCam.ScreenPointToRay(mousePos);
                    if (Physics.Raycast(mainRay, out hit, 1000, LevelManager.instance.selectableLayer))
                    {
                        var enemy = hit.collider.gameObject.GetComponent<EnemyBase>();
                        if (enemy != null)
                        {
                            if (HandManager.instance.canUseCards && HandManager.instance.currentMana >= heldCard.myProfile.myManaCost)
                            {
                               
                                heldCard.UseCard(enemy);
                            }
                            else
                            {
                                // Cannot use card / Not enough mana! Return card to hand!
                                AddCardToHand(heldCard, selected);
                            }

                            heldCard = null;
                        }
                        else
                        {
                            AddCardToHand(heldCard, selected);
                            heldCard = null;
                        }
                    }
                    else
                    {
                        AddCardToHand(heldCard, selected);
                        heldCard = null;
                    }
                }
                else
                {
                    if (HandManager.instance.canUseCards && HandManager.instance.currentMana >= heldCard.myProfile.myManaCost)
                    {
                        
                        heldCard.UseCard();
                    }
                    else
                    {
                        // Cannot use card / Not enough mana! Return card to hand!
                        AddCardToHand(heldCard, selected);
                    }

                    heldCard = null;
                }
            }
        }
        
        private void HandleDraggedCardInsideHand(bool mouseButton, int count)
        {
            if (!mouseButton)
            {
                // Stop dragging
                heldCardOffset = Vector3.zero;
                dragged = -1;
            }

            if (dragged != -1)
            {
                var card = hand[dragged];
                if (mouseButton && !mouseInsideHand)
                {
                    //  && sqrDistance > 2.1f
                    //if (cardPos.y > transform.position.y + 0.5) {
                    // Card is outside of the hand, so is considered "held" ready to be used
                    // Remove from hand, so that cards in hand fill the hole that the card left
                    heldCard = card;
                    RemoveCardFromHand(dragged);
                    count--;
                    dragged = -1;
                }
            }

            if (heldCard == null && mouseButton && dragged != -1 && selected != -1 && dragged != selected)
            {
                // Move dragged card
                MoveCardToIndex(dragged, selected);
                dragged = selected;
            }
        }

        private void CheckMouseInsideHandBounds(out bool mouseButton)
        {
            var point = transform.InverseTransformPoint(mouseWorldPos);
            mouseInsideHand = handBounds.Contains(point);

            mouseButton = Input.GetMouseButton(0);
        }

        private void GetDistanceToCurrentSelectedCard(out int count, out float sqrDistance)
        {
            count = hand.Count;
            sqrDistance = 1000;
            if (selected >= 0 && selected < count)
            {
                var t = (selected + 0.5f) / count;
                var p = GetCurvePoint(a, b, c, t);
                sqrDistance = (p - mouseWorldPos).sqrMagnitude;
            }
        }

        private void GetMouseWorldPosition(Vector2 mousePos)
        {
            var ray = cam.ScreenPointToRay(mousePos);
            if (plane.Raycast(ray, out var enter)) mouseWorldPos = ray.GetPoint(enter);
        }

        private void TiltCard(Vector2 mousePos)
        {
            mousePosDelta = (mousePos - prevMousePos) * new Vector2(1600f / Screen.width, 900f / Screen.height) *
                            Time.deltaTime;
            prevMousePos = mousePos;

            var tiltStrength = 3f;
            var tiltDrag = 3f;
            var tiltSpeed = 50f;

            force += (mousePosDelta * tiltStrength - heldCardTilt) * Time.deltaTime;
            force *= 1 - tiltDrag * Time.deltaTime;
            heldCardTilt += force * (Time.deltaTime * tiltSpeed);
            // these calculations probably aren't correct, but hey, they work...? :P

            if (showDebugGizmos)
            {
                Debug.DrawRay(mouseWorldPos, mousePosDelta, Color.red);
                Debug.DrawRay(mouseWorldPos, heldCardTilt, Color.cyan);
            }
        }

        #endregion

        #region Cyan Methods

        /// <summary>
        /// Obtains a point along a curve based on 3 points. Equal to Lerp(Lerp(a, b, t), Lerp(b, c, t), t).
        /// </summary>
        public static Vector3 GetCurvePoint(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return (oneMinusT * oneMinusT * a) + (2f * oneMinusT * t * b) + (t * t * c);
        }

        /// <summary>
        /// Obtains the derivative of the curve (tangent)
        /// </summary>
        public static Vector3 GetCurveTangent(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            return 2f * (1f - t) * (b - a) + 2f * t * (c - b);
        }

        /// <summary>
        /// Obtains a direction perpendicular to the tangent of the curve
        /// </summary>
        public static Vector3 GetCurveNormal(Vector3 a, Vector3 b, Vector3 c, float t)
        {
            Vector3 tangent = GetCurveTangent(a, b, c, t);
            return Vector3.Cross(tangent, Vector3.forward);
        }

        /// <summary>
        /// Moves the card in hand from the currentIndex to the toIndex. If you want to move a card that isn't in hand, use AddCardToHand
        /// </summary>
        public void MoveCardToIndex(int currentIndex, int toIndex)
        {
            if (currentIndex == toIndex) return; // Same index, do nothing
            CardBase card = hand[currentIndex];
            hand.RemoveAt(currentIndex);
            hand.Insert(toIndex, card);

            if (updateHierarchyOrder)
            {
                card.transform.SetSiblingIndex(toIndex);
            }
        }

        /// <summary>
        /// Adds a card to the hand. Optional param to insert it at a given index.
        /// </summary>
        public void AddCardToHand(CardBase card, int index = -1)
        {
            if (index < 0)
            {
                // Add to end
                hand.Add(card);
                index = hand.Count - 1;
            }
            else
            {
                // Insert at index
                hand.Insert(index, card);
            }

            if (updateHierarchyOrder)
            {
                card.transform.SetParent(transform);
                card.transform.SetSiblingIndex(index);
            }
        }

        /// <summary>
        /// Remove the card at the specified index from the hand.
        /// </summary>
        public void RemoveCardFromHand(int index)
        {
            if (updateHierarchyOrder)
            {
                CardBase card = hand[index];
                card.transform.SetParent(transform.parent);
                card.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
            }

            hand.RemoveAt(index);
        }

        #endregion

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.matrix = transform.localToWorldMatrix;
            Gizmos.color = Color.blue;

            Gizmos.DrawSphere(curveStart, 0.03f);
            //Gizmos.DrawSphere(Vector3.zero, 0.03f);
            Gizmos.DrawSphere(curveEnd, 0.03f);

            Vector3 p1 = curveStart;
            for (int i = 0; i < 20; i++)
            {
                float t = (i + 1) / 20f;
                Vector3 p2 = GetCurvePoint(curveStart, Vector3.zero, curveEnd, t);
                Gizmos.DrawLine(p1, p2);
                p1 = p2;
            }

            if (mouseInsideHand)
            {
                Gizmos.color = Color.red;
            }

            Gizmos.DrawWireCube(handOffset, handSize);
        }
#endif
    }
}