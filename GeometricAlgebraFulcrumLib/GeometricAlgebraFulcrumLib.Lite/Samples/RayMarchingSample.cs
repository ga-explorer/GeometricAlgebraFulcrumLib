// [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] 
// [!!] Copyright ©️ Raylib-CsLo and Contributors. 
// [!!] This file is licensed to you under the MPL-2.0.
// [!!] See the LICENSE file in the project root for more info. 
// [!!] ------------------------------------------------- 
// [!!] The code and 100+ examples are here! https://github.com/NotNotTech/Raylib-CsLo 
// [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!]  [!!] [!!] [!!] [!!]

using System.Numerics;
using Raylib_CsLo;

namespace GeometricAlgebraFulcrumLib.Lite.Samples;


/*******************************************************************************************
*
*   raylib [shaders] example - Raymarching shapes generation
*
*   NOTE: This example requires raylib OpenGL 3.3 for shaders support and only #version 330
*         is currently supported. OpenGL ES 2.0 platforms are not supported at the moment.
*
*   This example has been created using raylib 2.0 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2018 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

public static unsafe class RayMarchingShapes
{


#if PLATFORM_DESKTOP
	const int GLSL_VERSION = 330;
#else   // PLATFORM_RPI, PLATFORM_ANDROID, PLATFORM_WEB -> Not supported at this moment
	const int GLSL_VERSION = 100;
#endif

	public static int RayMarchingShapesExample()
	{
		// Initialization
		//--------------------------------------------------------------------------------------
		var screenWidth = 800;
		var screenHeight = 450;

		Raylib.SetConfigFlags(ConfigFlags.FLAG_WINDOW_RESIZABLE);
		Raylib.InitWindow(screenWidth, screenHeight, "raylib [shaders] example - raymarching shapes");

		Camera3D camera = new()
        {
            position = new Vector3(2.5f, 2.5f, 3.0f), // Camera position
            target = new Vector3(0.0f, 0.0f, 0.7f), // Camera looking at point
            up = new Vector3(0.0f, 1.0f, 0.0f), // Camera up vector (rotation towards target)
            fovy = 65.0f // Camera field-of-view Y
        };

        Raylib.SetCameraMode(camera, CameraMode.CAMERA_FREE);                 // Set camera mode

		// Load ray marching shader
		// NOTE: Defining 0 (NULL) for vertex shader forces usage of internal default vertex shader
		var shader = Raylib.LoadShader(null, Raylib.TextFormat("resources/shaders/glsl%i/raymarching.fs", GLSL_VERSION));

		// Get shader locations for required uniforms
		var viewEyeLoc = Raylib.GetShaderLocation(shader, "viewEye");
		var viewCenterLoc = Raylib.GetShaderLocation(shader, "viewCenter");
		var runTimeLoc = Raylib.GetShaderLocation(shader, "runTime");
		var resolutionLoc = Raylib.GetShaderLocation(shader, "resolution");

		Vector2 resolution = new((float)screenWidth, (float)screenHeight);
        Raylib.SetShaderValue(shader, resolutionLoc, resolution, ShaderUniformDataType.SHADER_UNIFORM_VEC2);

		var runTime = 0.0f;

		Raylib.SetTargetFPS(60);                       // Set our game to run at 60 frames-per-second
												//--------------------------------------------------------------------------------------

		// Main game loop
		while (!Raylib.WindowShouldClose())            // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------
            Raylib.UpdateCamera(&camera);              // Update camera

			Vector3 cameraPos = new(camera.position.X, camera.position.Y, camera.position.Z);
			Vector3 cameraTarget = new(camera.target.X, camera.target.Y, camera.target.Z);

			var deltaTime = Raylib.GetFrameTime();
			runTime += deltaTime;

			// Set shader required uniform values
            Raylib.SetShaderValue(shader, viewEyeLoc, cameraPos, ShaderUniformDataType.SHADER_UNIFORM_VEC3);
            Raylib.SetShaderValue(shader, viewCenterLoc, cameraTarget, ShaderUniformDataType.SHADER_UNIFORM_VEC3);
            Raylib.SetShaderValue(shader, runTimeLoc, &runTime, ShaderUniformDataType.SHADER_UNIFORM_FLOAT);

			// Check if screen is resized
			if (Raylib.IsWindowResized())
			{
				screenWidth = Raylib.GetScreenWidth();
				screenHeight = Raylib.GetScreenHeight();
				resolution = new((float)screenWidth, (float)screenHeight);
                Raylib.SetShaderValue(shader, resolutionLoc, resolution, ShaderUniformDataType.SHADER_UNIFORM_VEC2);
			}
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Raylib.RAYWHITE);

            // We only draw a white full-screen rectangle,
            // frame is generated in shader using raymarching
            Raylib.BeginShaderMode(shader);
            Raylib.DrawRectangle(0, 0, screenWidth, screenHeight, Raylib.WHITE);
            Raylib.EndShaderMode();

            Raylib.DrawText("(c) Raymarching shader by Iñigo Quilez. MIT License.", screenWidth - 280, screenHeight - 20, 10, Raylib.BLACK);

            Raylib.EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
        Raylib.UnloadShader(shader);           // Unload shader

        Raylib.CloseWindow();                  // Close window and OpenGL context
										//--------------------------------------------------------------------------------------

		return 0;
	}
}