using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEditor;

namespace UnityEngine.EventSystems
{
    public enum ColliderEventTriggerType
    {
        CollisionEnter,
        CollisionExit,
        CollisionStay,

        MouseDown,
        MouseDrag,
        MouseEnter,
        MouseExit,
        MouseUp,
        MouseUpAsButton,

        TriggerEnter,
        TriggerExit,
        TriggerStay,
    }
    
    #if UNITY_EDITOR
    [CustomEditor(typeof(ColliderEventTrigger), true)]
    public class ColliderEventTriggerEditor : Editor
    {
        private SerializedProperty m_DelegatesProperty;
        private GUIContent m_IconToolbarMinus;
        private GUIContent m_EventIDName;
        private GUIContent[] m_EventTypes;
        private GUIContent m_AddButonContent;

        protected virtual void OnEnable()
        {
            m_DelegatesProperty = serializedObject.FindProperty("m_Delegates");
            m_AddButonContent = new GUIContent("Add New Event Type");
            m_EventIDName = new GUIContent("");
            m_IconToolbarMinus = new GUIContent(EditorGUIUtility.IconContent("Toolbar Minus"));
            m_IconToolbarMinus.tooltip = "Remove all events in this list.";
            string[] names = Enum.GetNames(typeof(ColliderEventTriggerType));
            m_EventTypes = new GUIContent[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                m_EventTypes[i] = new GUIContent(names[i]);
            }
        }

        public override void OnInspectorGUI()
        {
            ColliderEventTrigger script = (ColliderEventTrigger)target;
            string noColliderErrorMessage = "A collider component must be attached to this gameObject!";

            if (!script.gameObject.GetComponent<Collider>())
            {
                EditorGUILayout.HelpBox(noColliderErrorMessage, MessageType.Warning);
                return;
            }

            serializedObject.Update();
            int num = -1;
            EditorGUILayout.Space();
            Vector2 vector = GUIStyle.none.CalcSize(m_IconToolbarMinus);
            for (int i = 0; i < m_DelegatesProperty.arraySize; i++)
            {
                SerializedProperty arrayElementAtIndex = m_DelegatesProperty.GetArrayElementAtIndex(i);
                SerializedProperty serializedProperty = arrayElementAtIndex.FindPropertyRelative("eventID");
                SerializedProperty property;

                bool missingComponent = false;
                string errorMessage = "Unknown error";

                switch (serializedProperty.enumValueIndex)
                {
                    case (int)ColliderEventTriggerType.CollisionEnter:
                    case (int)ColliderEventTriggerType.CollisionExit:
                    case (int)ColliderEventTriggerType.CollisionStay:
                        property = arrayElementAtIndex.FindPropertyRelative("collisionCallback");
                        if (!script.gameObject.GetComponent<Collider>())
                        {
                            missingComponent = true;
                        }
                        else if (script.gameObject.GetComponent<Collider>().isTrigger)
                        {
                            errorMessage = "Attached collider must not be a trigger!";
                            missingComponent = true;
                        }
                        break;
                    case (int)ColliderEventTriggerType.TriggerEnter:
                    case (int)ColliderEventTriggerType.TriggerExit:
                    case (int)ColliderEventTriggerType.TriggerStay:
                        property = arrayElementAtIndex.FindPropertyRelative("colliderCallback");
                        if (!script.gameObject.GetComponent<Collider>())
                        {
                            missingComponent = true;
                        }
                        else if (!script.gameObject.GetComponent<Collider>().isTrigger)
                        {
                            errorMessage = "Attached collider must be a trigger!";
                            missingComponent = true;
                        }
                        break;
                    case (int)ColliderEventTriggerType.MouseDown:
                    case (int)ColliderEventTriggerType.MouseDrag:
                    case (int)ColliderEventTriggerType.MouseEnter:
                    case (int)ColliderEventTriggerType.MouseExit:
                    case (int)ColliderEventTriggerType.MouseUp:
                    case (int)ColliderEventTriggerType.MouseUpAsButton:
                        if (!script.gameObject.GetComponent<Collider>())
                        {
                            missingComponent = true;
                        }
                        property = arrayElementAtIndex.FindPropertyRelative("genericCallback");
                        break;
                    default:
                        property = arrayElementAtIndex.FindPropertyRelative("genericCallback");
                        break;
                }

                m_EventIDName.text = serializedProperty.enumDisplayNames[serializedProperty.enumValueIndex];
                if (!missingComponent)
                {
                    EditorGUILayout.PropertyField(property, m_EventIDName);
                }
                else
                {
                    EditorGUILayout.HelpBox(errorMessage, MessageType.Warning);
                }
                Rect lastRect = GUILayoutUtility.GetLastRect();
                Rect position = new Rect(lastRect.xMax - vector.x - 8f, lastRect.y + 1f, vector.x, vector.y);
                if (GUI.Button(position, m_IconToolbarMinus, GUIStyle.none))
                {
                    num = i;
                }
                EditorGUILayout.Space();
            }
            if (num > -1)
            {
                RemoveEntry(num);
            }
            Rect rect = GUILayoutUtility.GetRect(m_AddButonContent, GUI.skin.button);
            rect.x += (rect.width - 200f) / 2f;
            rect.width = 200f;
            if (GUI.Button(rect, m_AddButonContent))
            {
                ShowAddTriggermenu();
            }
            serializedObject.ApplyModifiedProperties();
        }

        private void RemoveEntry(int toBeRemovedEntry)
        {
            m_DelegatesProperty.DeleteArrayElementAtIndex(toBeRemovedEntry);
        }

        private void ShowAddTriggermenu()
        {
            GenericMenu genericMenu = new GenericMenu();
            for (int i = 0; i < m_EventTypes.Length; i++)
            {
                bool flag = true;
                for (int j = 0; j < m_DelegatesProperty.arraySize; j++)
                {
                    SerializedProperty arrayElementAtIndex = m_DelegatesProperty.GetArrayElementAtIndex(j);
                    SerializedProperty serializedProperty = arrayElementAtIndex.FindPropertyRelative("eventID");
                    if (serializedProperty.enumValueIndex == i)
                    {
                        flag = false;
                    }
                }
                if (flag)
                {
                    genericMenu.AddItem(m_EventTypes[i], false, OnAddNewSelected, i);
                }
                else
                {
                    genericMenu.AddDisabledItem(m_EventTypes[i]);
                }
            }
            genericMenu.ShowAsContext();
            Event.current.Use();
        }

        private void OnAddNewSelected(object index)
        {
            int enumValueIndex = (int)index;
            m_DelegatesProperty.arraySize++;
            SerializedProperty arrayElementAtIndex = m_DelegatesProperty.GetArrayElementAtIndex(m_DelegatesProperty.arraySize - 1);
            SerializedProperty serializedProperty = arrayElementAtIndex.FindPropertyRelative("eventID");
            serializedProperty.enumValueIndex = enumValueIndex;
            serializedObject.ApplyModifiedProperties();
        }
    }
    #endif

    public class ColliderEventTrigger : MonoBehaviour, IEventSystemHandler
    {
        [Serializable]
        public class EmptyTriggerEvent : UnityEvent<BaseEventData>
        {
        }
        [Serializable]
        public class ColliderTriggerEvent : UnityEvent<Collider>
        {
        }
        [Serializable]
        public class CollisionTriggerEvent : UnityEvent<Collision>
        {
        }

        [Serializable]
        public class Entry
        {
            public ColliderEventTriggerType eventID = ColliderEventTriggerType.MouseDown;

            public EmptyTriggerEvent genericCallback = new EmptyTriggerEvent();
            public ColliderTriggerEvent colliderCallback = new ColliderTriggerEvent();
            public CollisionTriggerEvent collisionCallback = new CollisionTriggerEvent();
        }

        [FormerlySerializedAs("delegates"), SerializeField]
        private List<Entry> m_Delegates;

        public List<Entry> triggers
        {
            get
            {
                if (m_Delegates == null)
                {
                    m_Delegates = new List<Entry>();
                }
                return m_Delegates;
            }
            set
            {
                m_Delegates = value;
            }
        }

        protected ColliderEventTrigger()
        {
        }


        private void Execute(ColliderEventTriggerType id, BaseEventData eventData)
        {
            for (int i = 0; i < triggers.Count; i++)
            {
                Entry entry = triggers[i];
                if (entry.eventID == id && entry.genericCallback != null)
                {
                    entry.genericCallback.Invoke(eventData);
                }
            }
        }
        private void Execute(ColliderEventTriggerType id, Collider eventCollider)
        {
            for (int i = 0; i < triggers.Count; i++)
            {
                Entry entry = triggers[i];
                if (entry.eventID == id && entry.colliderCallback != null)
                {
                    entry.colliderCallback.Invoke(eventCollider);
                }
            }
        }
        private void Execute(ColliderEventTriggerType id, Collision eventCollision)
        {
            for (int i = 0; i < triggers.Count; i++)
            {
                Entry entry = triggers[i];
                if (entry.eventID == id && entry.collisionCallback != null)
                {
                    entry.collisionCallback.Invoke(eventCollision);
                }
            }
        }

        public virtual void OnCollisionEnter(Collision eventCollision)
        {
            Execute(ColliderEventTriggerType.CollisionEnter, eventCollision);
        }
        public virtual void OnCollisionExit(Collision eventCollision)
        {
            Execute(ColliderEventTriggerType.CollisionExit, eventCollision);
        }
        public virtual void OnCollisionStay(Collision eventCollision)
        {
            Execute(ColliderEventTriggerType.CollisionStay, eventCollision);
        }

        public virtual void OnMouseDown()
        {
            Execute(ColliderEventTriggerType.MouseDown, new BaseEventData(EventSystem.current));
        }
        public virtual void OnMouseDrag()
        {
            Execute(ColliderEventTriggerType.MouseDrag, new BaseEventData(EventSystem.current));
        }
        public virtual void OnMouseEnter()
        {
            Execute(ColliderEventTriggerType.MouseEnter, new BaseEventData(EventSystem.current));
        }
        public virtual void OnMouseExit()
        {
            Execute(ColliderEventTriggerType.MouseExit, new BaseEventData(EventSystem.current));
        }
        public virtual void OnMouseUp()
        {
            Execute(ColliderEventTriggerType.MouseUp, new BaseEventData(EventSystem.current));
        }
        public virtual void OnMouseUpAsButton()
        {
            Execute(ColliderEventTriggerType.MouseUpAsButton, new BaseEventData(EventSystem.current));
        }

        #if UNITY_2017
        public virtual void OnParticleCollision()
        {
            //todo: Implement particle collision.
        }
        #endif

        public virtual void OnTriggerEnter(Collider eventCollider)
        {
            Execute(ColliderEventTriggerType.TriggerEnter, eventCollider);
        }
        public virtual void OnTriggerExit(Collider eventCollider)
        {
            Execute(ColliderEventTriggerType.TriggerExit, eventCollider);
        }
        public virtual void OnTriggerStay(Collider eventCollider)
        {
            Execute(ColliderEventTriggerType.TriggerStay, eventCollider);
        }
    }
}