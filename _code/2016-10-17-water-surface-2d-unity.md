---
title: 2D Water surface with reflection
tags: unity
shortdesc: A simple shader + example of a water surface with reflection in 2D
image: static/content/water2d-unity/cover.png
---

I am not good with shaders. But for a 2D prototype I wanted to have a nice **water effect**.
Something with **reflection**, **deformation** of what is underwater and **coloration**.

The result:

[  ![Shader in action][url_img_gif]  ][url_img_gif]

I made an easy-to-use unity package if you'd like to use it too:
- [Source](https://github.com/valryon/water2d-unity/)
- [Download package](https://github.com/valryon/water2d-unity/releases/download/1.0/water2d_surface.unitypackage)

It has been made with Unity 5.4.2f1 and has been tested on PC and mobile. But I don't guarantee crazy performances or compatibility everywhere.

# How it works

**1/ Transparent bumped colored shader**

- a **GrabPass**, so it can display everything that has been drawn before
- a Color to apply to the grabbed texture
- bump mapping
- an additional sinusoïdal deformation with the `magnitude` parameter

So it's a simple transparent shader that applies a color and a light deformation, yep.

**2/ Reflection**

The reflection is just a simple trick. For **every object** we want to reflect in the water, we create a new **mirroded** sprite!

[  ![Step 1][url_img_s1]  ][url_img_s1]

The `WaterReflectableScript` will do it automagically. Simply add it to a sprite that should be reflected and, **on runtime**, a mirroded sprite will appear.

[  ![Step 2][url_img_s2]  ][url_img_s2]

And that's it!

[  ![Step 3][url_img_s3]  ][url_img_s3]

# The shader

As I say, I am not skilled in shader creation.

Please feel free to contribute and to correct all my mistakes, I'm sure this could be optimized.

```glsl
Shader "Custom/Water2D Surface" {
Properties {
  _Color("Color", Color) = (1,1,1,1)
	_MainTex ("Normalmap", 2D) = "bump" {}
  _Magnitude("Magnitude", Range(0,1)) = 0.05
}

Category {

	Tags { "Queue"="Transparent" }

	SubShader {

		// This pass grabs the screen behind the object into a texture.
		// We can access the result in the next pass as _BackgroundTex
		GrabPass { "_BackgroundTex" }

		// Main pass: Take the texture grabbed above and use the bumpmap to perturb it
		// on to the screen
		Pass {
			Name "BASE"

      CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #include "UnityCG.cginc"

      struct vertexInput {
	      float4 vertex : POSITION;
	      float2 texcoord: TEXCOORD0;
      };

      struct vertexOutput {
	      float4 vertex : SV_POSITION;
	      float4 uvgrab : TEXCOORD0;
	      float2 uvmain : TEXCOORD2;
	      UNITY_FOG_COORDS(3)
      };

      fixed4 _Color;
      sampler2D _MainTex;
      float4 _MainTex_ST;
      float _BumpAmt;
      float4 _BumpMap_ST;
      float  _Magnitude;

      vertexOutput vert (vertexInput v)
      {
	      vertexOutput o;
	      o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
	      #if UNITY_UV_STARTS_AT_TOP
	      float scale = -1.0;
	      #else
	      float scale = 1.0;
	      #endif
	      o.uvgrab.xy = (float2(o.vertex.x, o.vertex.y*scale) + o.vertex.w) * 0.5;
	      o.uvgrab.zw = o.vertex.zw;
	      o.uvmain = TRANSFORM_TEX( v.texcoord, _MainTex );
	      UNITY_TRANSFER_FOG(o,o.vertex);
	      return o;
      }

      sampler2D _BackgroundTex;
      float4 _GrabTexture_TexelSize;
      sampler2D _BumpMap;

      half4 frag (vertexOutput i) : SV_Target
      {
	      // Calculate perturbed coordinates
        half4 bump = tex2D(_MainTex, i.uvmain);

        half2 distortion = UnpackNormal(bump).rg;
        i.uvgrab.xy += distortion * _Magnitude;

        // Get pixel in GrabTexture, rendered in previous pass
	      half4 col = tex2Dproj(_BackgroundTex, UNITY_PROJ_COORD(i.uvgrab));

        // Apply color
	      col *= _Color;

	      return col;
      }
      ENDCG
		}
	}
}

}

```

# Credits

Thanks to [Branden](http://brandenstrochinsky.blogspot.fr/2016/06/water-effect.html) for the height map and inspiration. Check out his article for a similar solution with more details on the shaders.


[url_img_gif]: {{site.url}}/static/content/water2d-unity/water2D_3.gif

[url_package]: {{site.url}}/static/content/water2d-unity/water2d_surface.unitypackage

[url_img_s1]: {{site.url}}/static/content/water2d-unity/s2.png
[url_img_s2]: {{site.url}}/static/content/water2d-unity/s1.png
[url_img_s3]: {{site.url}}/static/content/water2d-unity/s3.png
