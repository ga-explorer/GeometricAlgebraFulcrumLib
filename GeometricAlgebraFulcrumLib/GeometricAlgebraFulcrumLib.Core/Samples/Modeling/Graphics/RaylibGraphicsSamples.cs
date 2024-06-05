// [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] 
// [!!] Copyright ©️ Raylib-CsLo and Contributors. 
// [!!] This file is licensed to you under the MPL-2.0.
// [!!] See the LICENSE file in the project root for more info. 
// [!!] ------------------------------------------------- 
// [!!] The code and 100+ examples are here! https://github.com/NotNotTech/Raylib-CsLo 
// [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!]  [!!] [!!] [!!] [!!]

/*******************************************************************************************
*
*   raylib [core] example - Initialize 3d camera free
*
*   This example has been created using raylib 1.3 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2015 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

using System.Numerics;
using Raylib_CsLo;

namespace GeometricAlgebraFulcrumLib.Core.Samples.Modeling.Graphics;

public static unsafe class RaylibGraphicsSamples
{
    public static void Camera3dFreeExample()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        Raylib.InitWindow(screenWidth, screenHeight, "raylib [core] example - 3d camera free");

        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.position = new(10.0f, 10.0f, 10.0f); // Camera position
        camera.target = new(0.0f, 0.0f, 0.0f);      // Camera looking at point
        camera.up = new(0.0f, 1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.fovy = 45.0f;                                // Camera field-of-view Y
        camera.projection_ = CameraProjection.CAMERA_PERSPECTIVE;                   // Camera mode type

        Vector3 cubePosition = new(0.0f, 0.0f, 0.0f);

        Raylib.SetCameraMode(camera, CameraMode.CAMERA_FREE); // Set a free camera mode

        Raylib.SetCameraPanControl((int)MouseButton.MOUSE_BUTTON_RIGHT);

        Raylib.SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second
                                                   //--------------------------------------------------------------------------------------

        // Main game loop
        while (!Raylib.WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            Raylib.UpdateCamera(&camera);          // Update camera

            if (Raylib.IsKeyDown('Z')) camera.target = new(0.0f, 0.0f, 0.0f);
            if (Raylib.IsKeyDown('Z')) camera.target = new(0.0f, 0.0f, 0.0f);
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.RAYWHITE);

            Raylib.BeginMode3D(camera);

            Raylib.DrawCube(cubePosition, 2.0f, 2.0f, 2.0f, Raylib.RED);
            Raylib.DrawCubeWires(cubePosition, 2.0f, 2.0f, 2.0f, Raylib.MAROON);

            Raylib.DrawGrid(10, 1.0f);

            Raylib.EndMode3D();

            Raylib.DrawRectangle(10, 10, 320, 133, Raylib.Fade(Raylib.SKYBLUE, 0.5f));
            Raylib.DrawRectangleLines(10, 10, 320, 133, Raylib.BLUE);

            Raylib.DrawText("Free camera default controls:", 20, 20, 10, Raylib.BLACK);
            Raylib.DrawText("- Mouse Wheel to Zoom in-out", 40, 40, 10, Raylib.DARKGRAY);
            Raylib.DrawText("- Mouse Right Pressed to Pan", 40, 60, 10, Raylib.DARKGRAY);
            Raylib.DrawText("- Alt + Mouse Right Pressed to Rotate", 40, 80, 10, Raylib.DARKGRAY);
            Raylib.DrawText("- Alt + Ctrl + Mouse Right Pressed for Smooth Zoom", 40, 100, 10, Raylib.DARKGRAY);
            Raylib.DrawText("- Z to zoom to (0, 0, 0)", 40, 120, 10, Raylib.DARKGRAY);

            Raylib.EndDrawing();
            //----------------------------------------------------------------------------------
        }



        // De-Initialization
        //--------------------------------------------------------------------------------------
        Raylib.CloseWindow();        // Close window and OpenGL context
                                     //--------------------------------------------------------------------------------------
    }

    public static int GeometricShapesExample()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        Raylib.InitWindow(screenWidth, screenHeight, "raylib [models] example - geometric shapes");

        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.position = new(0.0f, 10.0f, 10.0f);
        camera.target = new(0.0f, 0.0f, 0.0f);
        camera.up = new(0.0f, 1.0f, 0.0f);
        camera.fovy = 45.0f;
        camera.projection_ = CameraProjection.CAMERA_PERSPECTIVE;

        Raylib.SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
                                               //--------------------------------------------------------------------------------------

        // Main game loop
        while (!Raylib.WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            // TODO: Update your variables here
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.RAYWHITE);

            Raylib.BeginMode3D(camera);

            Raylib.DrawCube(new(-4.0f, 0.0f, 2.0f), 2.0f, 5.0f, 2.0f, Raylib.RED);
            Raylib.DrawCubeWires(new(-4.0f, 0.0f, 2.0f), 2.0f, 5.0f, 2.0f, Raylib.GOLD);
            Raylib.DrawCubeWires(new(-4.0f, 0.0f, -2.0f), 3.0f, 6.0f, 2.0f, Raylib.MAROON);

            Raylib.DrawSphere(new(-1.0f, 0.0f, -2.0f), 1.0f, Raylib.GREEN);
            Raylib.DrawSphereWires(new(1.0f, 0.0f, 2.0f), 2.0f, 16, 16, Raylib.LIME);

            Raylib.DrawCylinder(new(4.0f, 0.0f, -2.0f), 1.0f, 2.0f, 3.0f, 4, Raylib.SKYBLUE);
            Raylib.DrawCylinderWires(new(4.0f, 0.0f, -2.0f), 1.0f, 2.0f, 3.0f, 4, Raylib.DARKBLUE);
            Raylib.DrawCylinderWires(new(4.5f, -1.0f, 2.0f), 1.0f, 1.0f, 2.0f, 6, Raylib.BROWN);

            Raylib.DrawCylinder(new(1.0f, 0.0f, -4.0f), 0.0f, 1.5f, 3.0f, 8, Raylib.GOLD);
            Raylib.DrawCylinderWires(new(1.0f, 0.0f, -4.0f), 0.0f, 1.5f, 3.0f, 8, Raylib.PINK);

            Raylib.DrawGrid(10, 1.0f);        // Draw a grid

            Raylib.EndMode3D();

            Raylib.DrawFPS(10, 10);

            Raylib.EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        Raylib.CloseWindow();        // Close window and OpenGL context
                                     //--------------------------------------------------------------------------------------

        return 0;
    }
}
