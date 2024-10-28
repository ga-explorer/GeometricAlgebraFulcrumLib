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

namespace GeometricAlgebraFulcrumLib.Modeling.Samples.Modeling.Graphics.Raylib;

public static unsafe class RaylibGraphicsSamples
{
    public static void Camera3dFreeExample()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        Raylib_CsLo.Raylib.InitWindow(screenWidth, screenHeight, "raylib [core] example - 3d camera free");

        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.position = new(10.0f, 10.0f, 10.0f); // Camera position
        camera.target = new(0.0f, 0.0f, 0.0f);      // Camera looking at point
        camera.up = new(0.0f, 1.0f, 0.0f);          // Camera up vector (rotation towards target)
        camera.fovy = 45.0f;                                // Camera field-of-view Y
        camera.projection_ = CameraProjection.CAMERA_PERSPECTIVE;                   // Camera mode type

        Vector3 cubePosition = new(0.0f, 0.0f, 0.0f);

        Raylib_CsLo.Raylib.SetCameraMode(camera, CameraMode.CAMERA_FREE); // Set a free camera mode

        Raylib_CsLo.Raylib.SetCameraPanControl((int)MouseButton.MOUSE_BUTTON_RIGHT);

        Raylib_CsLo.Raylib.SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second
                                                   //--------------------------------------------------------------------------------------

        // Main game loop
        while (!Raylib_CsLo.Raylib.WindowShouldClose())        // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            Raylib_CsLo.Raylib.UpdateCamera(&camera);          // Update camera

            if (Raylib_CsLo.Raylib.IsKeyDown('Z')) camera.target = new(0.0f, 0.0f, 0.0f);
            if (Raylib_CsLo.Raylib.IsKeyDown('Z')) camera.target = new(0.0f, 0.0f, 0.0f);
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            Raylib_CsLo.Raylib.BeginDrawing();
            Raylib_CsLo.Raylib.ClearBackground(Raylib_CsLo.Raylib.RAYWHITE);

            Raylib_CsLo.Raylib.BeginMode3D(camera);

            Raylib_CsLo.Raylib.DrawCube(cubePosition, 2.0f, 2.0f, 2.0f, Raylib_CsLo.Raylib.RED);
            Raylib_CsLo.Raylib.DrawCubeWires(cubePosition, 2.0f, 2.0f, 2.0f, Raylib_CsLo.Raylib.MAROON);

            Raylib_CsLo.Raylib.DrawGrid(10, 1.0f);

            Raylib_CsLo.Raylib.EndMode3D();

            Raylib_CsLo.Raylib.DrawRectangle(10, 10, 320, 133, Raylib_CsLo.Raylib.Fade(Raylib_CsLo.Raylib.SKYBLUE, 0.5f));
            Raylib_CsLo.Raylib.DrawRectangleLines(10, 10, 320, 133, Raylib_CsLo.Raylib.BLUE);

            Raylib_CsLo.Raylib.DrawText("Free camera default controls:", 20, 20, 10, Raylib_CsLo.Raylib.BLACK);
            Raylib_CsLo.Raylib.DrawText("- Mouse Wheel to Zoom in-out", 40, 40, 10, Raylib_CsLo.Raylib.DARKGRAY);
            Raylib_CsLo.Raylib.DrawText("- Mouse Right Pressed to Pan", 40, 60, 10, Raylib_CsLo.Raylib.DARKGRAY);
            Raylib_CsLo.Raylib.DrawText("- Alt + Mouse Right Pressed to Rotate", 40, 80, 10, Raylib_CsLo.Raylib.DARKGRAY);
            Raylib_CsLo.Raylib.DrawText("- Alt + Ctrl + Mouse Right Pressed for Smooth Zoom", 40, 100, 10, Raylib_CsLo.Raylib.DARKGRAY);
            Raylib_CsLo.Raylib.DrawText("- Z to zoom to (0, 0, 0)", 40, 120, 10, Raylib_CsLo.Raylib.DARKGRAY);

            Raylib_CsLo.Raylib.EndDrawing();
            //----------------------------------------------------------------------------------
        }



        // De-Initialization
        //--------------------------------------------------------------------------------------
        Raylib_CsLo.Raylib.CloseWindow();        // Close window and OpenGL context
                                     //--------------------------------------------------------------------------------------
    }

    public static int GeometricShapesExample()
    {
        // Initialization
        //--------------------------------------------------------------------------------------
        const int screenWidth = 800;
        const int screenHeight = 450;

        Raylib_CsLo.Raylib.InitWindow(screenWidth, screenHeight, "raylib [models] example - geometric shapes");

        // Define the camera to look into our 3d world
        Camera3D camera = new();
        camera.position = new(0.0f, 10.0f, 10.0f);
        camera.target = new(0.0f, 0.0f, 0.0f);
        camera.up = new(0.0f, 1.0f, 0.0f);
        camera.fovy = 45.0f;
        camera.projection_ = CameraProjection.CAMERA_PERSPECTIVE;

        Raylib_CsLo.Raylib.SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
                                               //--------------------------------------------------------------------------------------

        // Main game loop
        while (!Raylib_CsLo.Raylib.WindowShouldClose())    // Detect window close button or ESC key
        {
            // Update
            //----------------------------------------------------------------------------------
            // TODO: Update your variables here
            //----------------------------------------------------------------------------------

            // Draw
            //----------------------------------------------------------------------------------
            Raylib_CsLo.Raylib.BeginDrawing();
            Raylib_CsLo.Raylib.ClearBackground(Raylib_CsLo.Raylib.RAYWHITE);

            Raylib_CsLo.Raylib.BeginMode3D(camera);

            Raylib_CsLo.Raylib.DrawCube(new(-4.0f, 0.0f, 2.0f), 2.0f, 5.0f, 2.0f, Raylib_CsLo.Raylib.RED);
            Raylib_CsLo.Raylib.DrawCubeWires(new(-4.0f, 0.0f, 2.0f), 2.0f, 5.0f, 2.0f, Raylib_CsLo.Raylib.GOLD);
            Raylib_CsLo.Raylib.DrawCubeWires(new(-4.0f, 0.0f, -2.0f), 3.0f, 6.0f, 2.0f, Raylib_CsLo.Raylib.MAROON);

            Raylib_CsLo.Raylib.DrawSphere(new(-1.0f, 0.0f, -2.0f), 1.0f, Raylib_CsLo.Raylib.GREEN);
            Raylib_CsLo.Raylib.DrawSphereWires(new(1.0f, 0.0f, 2.0f), 2.0f, 16, 16, Raylib_CsLo.Raylib.LIME);

            Raylib_CsLo.Raylib.DrawCylinder(new(4.0f, 0.0f, -2.0f), 1.0f, 2.0f, 3.0f, 4, Raylib_CsLo.Raylib.SKYBLUE);
            Raylib_CsLo.Raylib.DrawCylinderWires(new(4.0f, 0.0f, -2.0f), 1.0f, 2.0f, 3.0f, 4, Raylib_CsLo.Raylib.DARKBLUE);
            Raylib_CsLo.Raylib.DrawCylinderWires(new(4.5f, -1.0f, 2.0f), 1.0f, 1.0f, 2.0f, 6, Raylib_CsLo.Raylib.BROWN);

            Raylib_CsLo.Raylib.DrawCylinder(new(1.0f, 0.0f, -4.0f), 0.0f, 1.5f, 3.0f, 8, Raylib_CsLo.Raylib.GOLD);
            Raylib_CsLo.Raylib.DrawCylinderWires(new(1.0f, 0.0f, -4.0f), 0.0f, 1.5f, 3.0f, 8, Raylib_CsLo.Raylib.PINK);

            Raylib_CsLo.Raylib.DrawGrid(10, 1.0f);        // Draw a grid

            Raylib_CsLo.Raylib.EndMode3D();

            Raylib_CsLo.Raylib.DrawFPS(10, 10);

            Raylib_CsLo.Raylib.EndDrawing();
            //----------------------------------------------------------------------------------
        }

        // De-Initialization
        //--------------------------------------------------------------------------------------
        Raylib_CsLo.Raylib.CloseWindow();        // Close window and OpenGL context
                                     //--------------------------------------------------------------------------------------

        return 0;
    }
}
