using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using Utils.TagSelector;

namespace Utils.CameraBorder
{
    public class CameraBordersBuilder : MonoBehaviour
    {
        private Dictionary<SideType, GameObject> _borders = new Dictionary<SideType, GameObject>();
        public static event Action<Dictionary<SideType, GameObject>> OnBuild;
        private Vector3 _minPosition, _maxPosition;

        [SerializeField] private Camera _cam;

        [TagSelector] public string TopBorderTag = "";

        [TagSelector] public string ButtomBorderTag = "";

        [TagSelector] public string RightBorderTag = "";

        [TagSelector] public string LeftBorderTag = "";
        

        [HideInInspector] public SideType BordersPosition;

        private Bounds _cameraBounds;

        [SerializeField] private float _bordersThickness;

        private void Start()
        {
            GameObject bordersContainer = new GameObject("Borders Container");
            bordersContainer.transform.SetParent(transform);
            _cameraBounds = ComputeCameraBounds();
            _minPosition = _cameraBounds.min;
            _maxPosition = _cameraBounds.max;
            InitBorders();
        }

        private void InitBorders()
        {
            Dictionary<int, Action> actions = new Dictionary<int, Action>();

            float width = Mathf.Abs(_minPosition.x - _maxPosition.x);
            float height = Mathf.Abs(_minPosition.y - _maxPosition.y);
            float bordersThickness = _bordersThickness / 2;

            actions.Add((int) SideType.Top,
                () =>
                {
                    BuildBorder(SideType.Top, new Vector3(bordersThickness, _maxPosition.y + bordersThickness), width,
                        0, TopBorderTag);
                });
            actions.Add((int) SideType.Buttom,
                () =>
                {
                    BuildBorder(SideType.Buttom, new Vector3(-bordersThickness, _minPosition.y - bordersThickness),
                        width, 0, ButtomBorderTag);
                });

            actions.Add((int) SideType.Left,
                () =>
                {
                    BuildBorder(SideType.Left, new Vector3(_minPosition.x - bordersThickness, bordersThickness), 0,
                        height, LeftBorderTag);
                });
            actions.Add((int) SideType.Right,
                () =>
                {
                    BuildBorder(SideType.Right, new Vector3(_maxPosition.x + bordersThickness, -bordersThickness), 0,
                        height, RightBorderTag);
                });

            var bitArray = new BitArray(new[] {(byte) BordersPosition});
            for (int i = 0; i < Mathf.Sqrt((byte) SideType.All + 1); i++)
            {
                if (bitArray[i])
                {
                    actions[(int) Mathf.Pow(2, i)]();
                }
            }

            OnBuild?.Invoke(_borders);
        }

        private void BuildBorder(SideType sideType, Vector2 position, float width = 0, float height = 0,
            string tag = "Untagged")
        {
            var border = new GameObject($"{Enum.GetName(typeof(SideType), sideType)} Border", typeof(BoxCollider2D));
            border.transform.tag = tag;

            var boxCollider2D = border.GetComponent<BoxCollider2D>();

            border.transform.position = position;

            boxCollider2D.size = new Vector3(Mathf.Abs(width + _bordersThickness),
                Mathf.Abs(height + _bordersThickness));

            border.transform.SetParent(transform.GetChild(0));
            
            _borders.Add(sideType, border);
            CameraBorder.Init(border, sideType);
        }

        private Bounds ComputeCameraBounds()
        {
            var cameraWorldWidth = Vector3.Distance(_cam.ScreenToWorldPoint(new Vector3(0, 0, 0)),
                _cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)));
            
            var cameraWorldHeight = Vector3.Distance(_cam.ScreenToWorldPoint(new Vector3(0, 0, 0)),
                _cam.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)));

            return new Bounds(new Vector3(0, 0), new Vector3(cameraWorldWidth, cameraWorldHeight));
        }

    }

#if UNITY_EDITOR
    [CustomEditor(typeof(CameraBordersBuilder))]
    [CanEditMultipleObjects]
    public class MyScriptCustomizator : Editor
    {
        readonly List<SerializedProperty> serializedProperties = new List<SerializedProperty>();

        private CameraBordersBuilder _cameraBordersBuilder;

        void OnEnable()
        {
            var type = serializedObject.targetObject.GetType();
            _cameraBordersBuilder = (CameraBordersBuilder) serializedObject.targetObject;
            foreach (var field in type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public |
                                                 BindingFlags.Instance))
            {
                var serializedProperty = serializedObject.FindProperty(field.Name);

                if (serializedProperty != null)
                {
                    if (field.FieldType == typeof(SideType)) continue;
                    serializedProperties.Add(serializedObject.FindProperty(field.Name));
                }
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            foreach (var property in serializedProperties)
            {
                EditorGUILayout.PropertyField(property);
            }

            _cameraBordersBuilder.BordersPosition =
                (SideType) EditorGUILayout.EnumFlagsField(_cameraBordersBuilder.BordersPosition);

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}
