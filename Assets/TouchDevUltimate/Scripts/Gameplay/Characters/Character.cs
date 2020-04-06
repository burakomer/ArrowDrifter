using TouchDevUltimate.Gameplay.Core;
using UnityEngine;

namespace TouchDevUltimate.Gameplay.Character
{
    [RequireComponent(typeof(CharacterBrain), typeof(Health), typeof(Collider2D))]
    public class Character : MonoBehaviour
    {
        [Header("Properties")]
        public string characterName;
        public CharacterType characterType;
        public bool isAlive => health.isAlive;

        #region Components

        [HideInInspector] public new Collider2D collider;
        [HideInInspector] public Health health;
        [HideInInspector] public CharacterBrain brain;
        [HideInInspector] public CharacterModel model;
        
        #endregion

        protected virtual void Awake()
        {
            collider = GetComponent<Collider2D>();
            brain = GetComponent<CharacterBrain>();
            model = GetComponentInChildren<CharacterModel>();
            health = GetComponent<Health>();
            health.model = model.gameObject;
        }

        protected virtual void Start()
        {

        }

        #region Management Methods 

        public virtual void DisableCharacter(bool modelDisabled = false)
        {
            brain.active = false;
            collider.enabled = false;

            if (modelDisabled)
            {
                model.gameObject.SetActive(false);
            }
        }

        #endregion
    }
}
