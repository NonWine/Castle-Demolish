Shader "Hidden/LubLUT"
{
    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #define COLORS 32.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _LutTex;

            float4 _LutTex_TexelSize;

            float _Contribution;

            fixed4 frag (v2f i) : SV_Target
            {
                float maxColor = COLORS - 1.0;
                fixed4 col = tex2D(_MainTex, i.uv);
                float halfColX = 0.5 / _LutTex_TexelSize.z;
                float halfColY = 0.5 / _LutTex_TexelSize.w;
                float treshold  = maxColor/COLORS;

                float xOffset = halfColX + col.r * treshold / COLORS;
                float yOffset = halfColY + col.g * treshold;
                float ceil = floor(col.b * maxColor);

                float2 lutPos = float2(ceil / COLORS + xOffset, yOffset);
                fixed4 gradedCol = tex2D(_LutTex, lutPos);
                
                return lerp(col, gradedCol, _Contribution);
            }
            ENDCG
        }
    }
}
