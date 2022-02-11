using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UIViews.Code.UserControlSystem.Views
{
    public enum Mode {
        OutlineAll,
        OutlineVisible,
        OutlineHidden,
        OutlineAndSilhouette,
        SilhouetteOnly
    }

    public struct ListVector3
    {
        public List<Vector3> data;
    }
    
    [DisallowMultipleComponent]
    public class OutlineView : MonoBehaviour {
        private static HashSet<Mesh> _registeredMeshes = new HashSet<Mesh>();
        private static readonly int ZTest = Shader.PropertyToID("_ZTest");
        private static readonly int Width = Shader.PropertyToID("_OutlineWidth");
        private static readonly int OutlineColor1 = Shader.PropertyToID("_OutlineColor");

        [SerializeField] private Mode _outlineMode;
        [SerializeField] private Color _outlineColor = Color.white;
        [SerializeField, Range(0f, 10f)] private float _outlineWidth = 2f;

        public Mode OutlineMode
        {
            get => _outlineMode;
            set => _outlineMode = value;
        }
        public Color OutlineColor
        {
            get => _outlineColor;
            set => _outlineColor = value;
        }
        public float OutlineWidth
        {
            get => _outlineWidth;
            set => _outlineWidth = value;
        }

        private Renderer[] _renderers;
        private Material _outlineMaskMaterial;
        private Material _outlineFillMaterial;
        
        private List<Mesh> _bakeKeys = new List<Mesh>();
        private List<ListVector3> _bakeValues = new List<ListVector3>();

        private bool needsUpdate;

        private void Awake() 
        {
            _outlineMaskMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineMask"));
            _outlineFillMaterial = Instantiate(Resources.Load<Material>(@"Materials/OutlineFill"));

            _outlineMaskMaterial.name = "OutlineMask (Instance)";
            _outlineFillMaterial.name = "OutlineFill (Instance)";
            
            needsUpdate = true;
        }

        private void OnEnable() 
        {
            _renderers = GetComponentsInChildren<Renderer>();
            LoadSmoothNormals();
            
            foreach (var render in _renderers) 
            {
                var materials = render.sharedMaterials.ToList();

                materials.Add(_outlineMaskMaterial);
                materials.Add(_outlineFillMaterial);

                render.materials = materials.ToArray();
            }
        }

        private void OnValidate() 
        {
            needsUpdate = true;

            if (_bakeKeys.Count == 0 && _bakeKeys.Count == _bakeValues.Count) 
                return;
            
            _bakeKeys.Clear();
            _bakeValues.Clear();
        }

        private void Update()
        {
            if (!needsUpdate) 
                return;
            
            needsUpdate = false;
            UpdateMaterialProperties();
        }

        private void OnDisable() {
            foreach (var render in _renderers) 
            {
                var materials = render.sharedMaterials.ToList();

                materials.Remove(_outlineMaskMaterial);
                materials.Remove(_outlineFillMaterial);

                render.materials = materials.ToArray();
            }
        }

        private void OnDestroy() 
        {
            Destroy(_outlineMaskMaterial);
            Destroy(_outlineFillMaterial);
        }

        private void LoadSmoothNormals() 
        {
            foreach (var meshFilter in GetComponentsInChildren<MeshFilter>()) {
                if (!_registeredMeshes.Add(meshFilter.sharedMesh))
                    continue;

                var index = _bakeKeys.IndexOf(meshFilter.sharedMesh);
                var smoothNormals = (index >= 0) ? _bakeValues[index].data : SmoothNormals(meshFilter.sharedMesh);

                meshFilter.sharedMesh.SetUVs(3, smoothNormals);
            }

            foreach (var skinnedMeshRenderer in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (_registeredMeshes.Add(skinnedMeshRenderer.sharedMesh)) 
                {
                    skinnedMeshRenderer.sharedMesh.uv4 = new Vector2[skinnedMeshRenderer.sharedMesh.vertexCount];
                }
            }
        }

        private List<Vector3> SmoothNormals(Mesh mesh) 
        {
            var groups = mesh.vertices
                .Select((vertex, index) => new KeyValuePair<Vector3, int>(vertex, index))
                .GroupBy(pair => pair.Key);
            var smoothNormals = new List<Vector3>(mesh.normals);

            foreach (var group in groups) 
            {
                if (group.Count() == 1)
                    continue;

                var smoothNormal = Vector3.zero;
                foreach (var pair in group) 
                {
                    smoothNormal += mesh.normals[pair.Value];
                }

                smoothNormal.Normalize();
                foreach (var pair in group) 
                {
                    smoothNormals[pair.Value] = smoothNormal;
                }
            }

            return smoothNormals;
        }

        private void UpdateMaterialProperties() 
        {
            _outlineFillMaterial.SetColor(OutlineColor1, _outlineColor);

            switch (_outlineMode) 
            {
                case Mode.OutlineAll:
                    _outlineMaskMaterial.SetFloat(ZTest, (float) UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat(ZTest, (float) UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat(Width, _outlineWidth);
                    break;

                case Mode.OutlineVisible:
                    _outlineMaskMaterial.SetFloat(ZTest, (float) UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat(ZTest, (float) UnityEngine.Rendering.CompareFunction.LessEqual);
                    _outlineFillMaterial.SetFloat(Width, _outlineWidth);
                    break;

                    case Mode.OutlineHidden:
                    _outlineMaskMaterial.SetFloat(ZTest, (float) UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat(ZTest, (float) UnityEngine.Rendering.CompareFunction.Greater);
                    _outlineFillMaterial.SetFloat(Width, _outlineWidth);
                    break;

                case Mode.OutlineAndSilhouette:
                    _outlineMaskMaterial.SetFloat(ZTest, (float) UnityEngine.Rendering.CompareFunction.LessEqual);
                    _outlineFillMaterial.SetFloat(ZTest, (float) UnityEngine.Rendering.CompareFunction.Always);
                    _outlineFillMaterial.SetFloat(Width, _outlineWidth);
                    break;

                case Mode.SilhouetteOnly:
                    _outlineMaskMaterial.SetFloat(ZTest, (float)UnityEngine.Rendering.CompareFunction.LessEqual);
                    _outlineFillMaterial.SetFloat(ZTest, (float)UnityEngine.Rendering.CompareFunction.Greater);
                    _outlineFillMaterial.SetFloat(Width, 0);
                    break;
            }
        }
    }
}
