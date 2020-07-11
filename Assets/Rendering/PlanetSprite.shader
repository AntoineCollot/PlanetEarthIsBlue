// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "Custom/Sprite/Planet"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
        [HideInInspector] _RendererColor ("RendererColor", Color) = (1,1,1,1)
        [HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
        [PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
        [PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
		
		//Circle
		_CircleRadius ("Radius",Range(0,1)) = 0.75
		_CircleFade("Circle Fade",Range(0,1)) = 0.01
		
		//Noise
		_NoiseFreq ("Noise Frequency", Float) = 1
		_NoiseAspect ("Noise Aspect", Range(0.3, 3)) = 1
		
		//Draw
		_ThresholdValue ("Threshold Value", Range(0,1)) =0.5
		_ThresholdWidth ("Threshold Width", Range(0,1)) =0.3
		_Color1("Main Color", Color) = (1,1,1,1)
		_Color2("Secondary Color", Color) = (1,1,1,1)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        Pass
        {
        CGPROGRAM
            #pragma vertex SpriteVertNoise
            #pragma fragment SpriteFragNoise
            #pragma target 2.0
            #pragma multi_compile_instancing
            #pragma multi_compile_local _ PIXELSNAP_ON
            #pragma multi_compile _ ETC1_EXTERNAL_ALPHA
            #include "UnitySprites.cginc"
            #include "noiseSimplex.cginc"
			
			struct v2fNoise
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				float2 srcPos : TEXCOORD1;
				UNITY_VERTEX_OUTPUT_STEREO
			};
			
			//Noise
			half _NoiseFreq;
			half _NoiseAspect;
			
			v2fNoise SpriteVertNoise(appdata_t IN)
			{
				v2fNoise OUT;

				UNITY_SETUP_INSTANCE_ID (IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);

				OUT.vertex = UnityFlipSprite(IN.vertex, _Flip);
				OUT.vertex = UnityObjectToClipPos(OUT.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color * _RendererColor;

				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif
				
				OUT.srcPos = mul(unity_ObjectToWorld, IN.vertex).xyz;
				OUT.srcPos *= _NoiseFreq;
				OUT.srcPos.x *= _NoiseAspect;
				OUT.srcPos.y /= _NoiseAspect;

				return OUT;
			}
			
			//Circle
			half _CircleRadius;
			half _CircleFade;
			
			//Draw
			fixed4 _Color1;
			fixed4 _Color2;
			half _ThresholdValue;
			half _ThresholdWidth;
			
			fixed4 SpriteFragNoise(v2fNoise IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture (IN.texcoord) * IN.color;
				
				//circle
				half effectiveRadius = _CircleRadius;
				half dist = length(IN.texcoord-float2(0.5,0.5));
				c.a *= smoothstep(dist, dist+_CircleFade+0.00001,effectiveRadius*.5);
				
				//Draw
				float ns = snoise(IN.srcPos) / 2 + 0.5f;
				fixed4 col = _Color2;
				half width = _ThresholdWidth * 0.5;
				half lowerThreshold = _ThresholdValue - width;
				half upperThreshold = _ThresholdValue + width;
				//Take lower color if ns < lower threshold
				col = step(lowerThreshold ,ns) * col + step(ns,lowerThreshold) * _Color1;
				//Take upper color if ns > upper threshold
				col = step(ns,upperThreshold) * col + step(upperThreshold,ns) * _Color1;
				
				half greyscale = c.r + c.g+c.b;
				c.rgb *= c.a;
				return c * col;
			}
			
        ENDCG
        }
    }
}
