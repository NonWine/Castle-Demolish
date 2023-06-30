using UnityEngine;

namespace LubLUT.Scripts
{
    [ExecuteAlways]
    public class LubLUT : MonoBehaviour
    {
        [SerializeField] private Texture2D lutTexture;

        [SerializeField, Range(0,1)] private float blend;

        private Material _lutMat;

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (_lutMat == null)
                _lutMat = new Material(Shader.Find("Hidden/LubLUT"));
            
            _lutMat.SetTexture("_LutTex", lutTexture);
            _lutMat.SetTexture("_MainTex", src);
            _lutMat.SetFloat("_Contribution", blend);
            
            Graphics.Blit(null, dest, _lutMat);
        }
    }
}