using UnityEngine;
using UnityEngine.Events;
using EnumLibrary;

namespace Interactables
{
    /// <summary>
    /// Chld class for interactable signs-object ingame tha shall be interacted by pressing the 'E'-Key when player is in appropriate trigger zone
    /// </summary>
    public class Interactable : InteractionController
    {
        //------------------------------ Events ------------------------------
        public static event UnityAction OnDoorKickIn;

        //------------------------------ Fields ------------------------------
        [Header("Needed References to GameObjects")]
        [Space(2)]
        [Tooltip("Reference to the Animator-Component of this Object")]
        [SerializeField] private Animator _animCtrl;
        [Tooltip("Reference to the Animator-Component of the Player-Object")]
        [SerializeField] private Animator _playerAnim;
        [Space(5)]

        [Header("Settings")]
        [Tooltip("Set which type of Interactable this Object is for appropriate Interaction-Logic")]
        [SerializeField] private Enum_Lib.EInteractableType _interactableType;


        //------------------------------ Methods ------------------------------

        //---------- Unity-Executed Methods ----------
        private new void Awake()
        {
            #region AutoReferencing

            if (_animCtrl == null)
            {
                _animCtrl = GetComponent<Animator>();
                Debug.Log($"<color=yellow>Caution! Reference for Animator 'Anim Ctrl' was not set in Inspector of '{this}'. Trying to set automatically.</color>");
            }

            if (_playerAnim == null)
            {
                _playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
                Debug.Log($"<color=yellow>Caution! Reference for Animator 'Player Anim' was not set in Inspector of '{this}'. Trying to set automatically.</color>");
            }

            base.Awake();

            #endregion
        }


        //---------- Custom Methods ----------
        /// <summary>
        /// activates Tresure-Open-Animation, disables User-Interaction-Feedback-Marker and starts coroutine for collecting-Item-logic 
        /// </summary>
        protected override void ReadInteractionInput()
        {

            // disable Interaction-Feedback-UI
            for (int i = 0; i < _interactionFeedbackUI.Length; i++)
                _interactionFeedbackUI[i].SetActive(false);

            switch (_interactableType)
            {
                case Enum_Lib.EInteractableType.Door:

                    PlayAnimation("...");           // todo: if Door-KickIn-ANimation for Player is implemented fill out the Name-sting; JM (09.Oct.2023)
                    
                    PlaySFX("...");                 // Play DoorKickIn Sound

                    gameObject.SetActive(false);    // todo: exchange this later to switching the GameObjects from intact door to broken door; JM (09.Oct.2023)

                    // todo: (!)start runtime baking of nw nav mesh so the new accured walkable space (where once the door was) is walkable for the AI; JM (09.Oct.2023)

                    break;

                default:
                    break;
            }
        }

        private void PlayAnimation(string animationName)
        {
            if (_animCtrl != null)
                _animCtrl.SetBool(animationName, true);
        }

        private void PlaySFX(string soundToBePlayed)
        {
            //todo: If AudioManager for Handling SFX is implemented following outcommented Code can be commented in again; JM (09.Oct.2023)
            //if (AudioManager.Instance != null)
            //    AudioManager.Instance.PlayEffectSound(soundToBePlayed);                     
        }
    }
}