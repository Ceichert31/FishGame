Shader "Unlit/TonalArtMaps"
{
    Properties
    {
        _TAM1 ("Texture", 2D) = "white" {}
        _TAM2 ("Texture", 2D) = "white" {}
        _LightColor ("Light Color", Color) = (1, 1, 1, 1)
        _LightPosition ("Light Position", Vector) = (1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
             Tags { "LightMode" = "ForwardBase" "PassFlags" = "OnlyDirectional" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : NORMAL;
                float4 color : COLOR;
            };

            sampler2D _TAM1;
            float4 _TAM1_ST;
            sampler2D _TAM2;
            fixed4 _LightColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToClipPos(v.normal);
                o.uv = TRANSFORM_TEX(v.uv, _TAM1);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            //Returns the average color of a pixel
            float PixelBrightness(float3 pixelColor)
            {
                return pixelColor.r + pixelColor.g + pixelColor.b / 3.0;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed lum = Luminance(i.color);

                float3 lightDirection = normalize(WorldSpaceLightDir(i.vertex));

                half diffuseIntesity = max(dot(i.normal, lightDirection), 0.0);

                fixed4 diffuseColor = _LightColor * tex2D(_TAM1, i.uv) * diffuseIntesity;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, diffuseColor);
                return diffuseColor;
            }
            ENDCG
        }
    }
}
