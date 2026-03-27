Shader "Custom/DamageFlash"
{
     Properties
    {
        [PerRendererData]_MainTex("Sprite Texture", 2D) = "white" {}
        _Color("Tint", Color) = (1,1,1,1)
        _FlashColor("Flash Color", Color) = (1,0,0,0)
        _FlashAmount("Flash Amount", Range(0,1)) = 0
        _Alpha("Alpha", Range(0,1)) = 1

        [HideInInspector]_RendererColor("RendererColor", Color) = (1,1,1,1)
        [HideInInspector]_Flip("Flip", Vector) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "IgnoreProjector"="True"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            fixed4 _Color;
            fixed4 _FlashColor;
            float _FlashAmount;
            float _Alpha;

            struct appdata_t
            {
                float4 vertex   : POSITION;
                float4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex   : SV_POSITION;
                fixed4 color    : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            fixed4 _RendererColor;

            v2f vert(appdata_t IN)
            {
                v2f OUT;
                OUT.vertex = UnityObjectToClipPos(IN.vertex);
                OUT.texcoord = IN.texcoord;
                OUT.color = IN.color * _RendererColor;
                return OUT;
            }

            fixed4 frag(v2f IN) : SV_Target
{
            fixed4 tex = tex2D(_MainTex, IN.texcoord) * IN.color * _Color;
    
            // Applica il flash solo dove il pixel è visibile (alpha > 0)
            fixed4 finalColor = tex;
            finalColor.rgb = lerp(tex.rgb, _FlashColor.rgb, _FlashAmount * tex.a);
    
            finalColor.a *= _Alpha;
            return finalColor;
}
            ENDCG
        }
    }
}