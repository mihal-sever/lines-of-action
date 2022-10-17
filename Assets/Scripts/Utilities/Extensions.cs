using UnityEngine;

namespace Sever.Utilities
{
    public static class Extensions
    {
        public static void SetColor(this Renderer renderer, Color color)
        {
            var propertyBlock = new MaterialPropertyBlock();
            renderer.GetPropertyBlock(propertyBlock);
            propertyBlock.SetColor("_Color", color);
            renderer.SetPropertyBlock(propertyBlock);
        }
    }
}