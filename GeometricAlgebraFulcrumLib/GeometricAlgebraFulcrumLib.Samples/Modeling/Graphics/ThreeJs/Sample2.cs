using System;
using System.Collections.Generic;
using System.Linq;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs;
using GeometricAlgebraFulcrumLib.Core.Modeling.Graphics.Rendering.ThreeJs.Objects;
using GeometricAlgebraFulcrumLib.Utilities.Text.Code.JavaScript;

namespace GeometricAlgebraFulcrumLib.Samples.Modeling.Graphics.ThreeJs;

public static class Sample2
{
    public static void Execute()
    {
        var composer = JavaScriptCodeComposer.DefaultComposer;

        // camera = new THREE.PerspectiveCamera( 45, window.innerWidth / window.innerHeight, 1, 2000 );
        // camera.position.y = 400;
        var camera = new JsPerspectiveCamera(
            45,
            "window.innerWidth / window.innerHeight",
            1,
            2000
        ).JsConst("camera");
        camera.Position.Y = 400;
        composer.EmptyLine();

        // scene = new THREE.Scene();
        var scene = new JsScene().JsConst("scene");
        composer.EmptyLine();

        // let object;
        composer.VarLet("object");
        composer.EmptyLine();

        // const ambientLight = new THREE.AmbientLight( 0xcccccc, 0.4 );
        // scene.add( ambientLight );
        var ambientLight = new JsAmbientLight(
            "0xcccccc",
            0.4
        ).JsConst("ambientLight");

        scene.Add(ambientLight);
        composer.EmptyLine();

        // const pointLight = new THREE.PointLight( 0xffffff, 0.8 );
        // camera.add( pointLight );
        // scene.add( camera );
        var pointLight = new JsPointLight(0xffffff, 0.8);
        camera.Add(pointLight);
        scene.Add(camera);
        composer.EmptyLine();

        // const map = new THREE.TextureLoader().load( 'textures/uv_grid_opengl.jpg' );
        // map.wrapS = map.wrapT = THREE.RepeatWrapping;
        // map.anisotropy = 16;
        var map = new JsTextureLoader().Load("'textures/uv_grid_opengl.jpg'").JsConst("map");
        map.WrapS = map.WrapT = ThreeJsConstants.RepeatWrapping;
        map.Anisotropy = 16;
        composer.EmptyLine();

        JsBufferGeometry jsGeometry = null;
        // const material = new THREE.MeshPhongMaterial( { map: map, side: THREE.DoubleSide } );
        var material = new JsMeshPhongMaterial(
            new Dictionary<string, JsType>()
            {
                {"map", map},
                {"side", ThreeJsConstants.DoubleSide}
            }
        ).JsConst("material");
        composer.EmptyLine().CodeLine("//").EmptyLine();

        // object = new THREE.Mesh( new THREE.SphereGeometry( 75, 20, 10 ), material );
        // object.position.set( - 300, 0, 200 );
        // scene.add( object );
        jsGeometry = new JsSphereGeometry(75, 20, 10);
        var jsObject = "object".JsSet(new JsMesh(jsGeometry, material));
        jsObject.Position.Set(-300, 0, 200);
        scene.Add(jsObject);
        composer.EmptyLine();

        // object = new THREE.Mesh( new THREE.IcosahedronGeometry( 75, 1 ), material );
        // object.position.set( - 100, 0, 200 );
        // scene.add( object );
        jsGeometry = new JsIcosahedronGeometry(75, 1);
        jsObject = "object".JsSet(new JsMesh(jsGeometry, material));
        jsObject.Position.Set(-100, 0, 200);
        scene.Add(jsObject);
        composer.EmptyLine();

        // object = new THREE.Mesh( new THREE.OctahedronGeometry( 75, 2 ), material );
        // object.position.set( 100, 0, 200 );
        // scene.add( object );
        jsGeometry = new JsOctahedronGeometry(75, 2);
        jsObject = "object".JsSet(new JsMesh(jsGeometry, material));
        jsObject.Position.Set(100, 0, 200);
        scene.Add(jsObject);
        composer.EmptyLine();

        // object = new THREE.Mesh( new THREE.TetrahedronGeometry( 75, 0 ), material );
        // object.position.set( 300, 0, 200 );
        // scene.add( object );
        jsGeometry = new JsTetrahedronGeometry(75, 0);
        jsObject = "object".JsSet(new JsMesh(jsGeometry, material));
        jsObject.Position.Set(300, 0, 200);
        scene.Add(jsObject);
        composer.EmptyLine().CodeLine("//").EmptyLine();

        // object = new THREE.Mesh( new THREE.PlaneGeometry( 100, 100, 4, 4 ), material );
        // object.position.set( - 300, 0, 0 );
        // scene.add( object );
        jsGeometry = new JsPlaneGeometry(100, 100, 4, 4);
        jsObject = "object".JsSet(new JsMesh(jsGeometry, material));
        jsObject.Position.Set(-300, 0, 0);
        scene.Add(jsObject);
        composer.EmptyLine();

        // object = new THREE.Mesh( new THREE.BoxGeometry( 100, 100, 100, 4, 4, 4 ), material );
        // object.position.set( - 100, 0, 0 );
        // scene.add( object );
        jsGeometry = new JsBoxGeometry(100, 100, 100, 4, 4, 4);
        jsObject = "object".JsSet(new JsMesh(jsGeometry, material));
        jsObject.Position.Set(-100, 0, 0);
        scene.Add(jsObject);
        composer.EmptyLine();

        // object = new THREE.Mesh( new THREE.CircleGeometry( 50, 20, 0, Math.PI * 2 ), material );
        // object.position.set( 100, 0, 0 );
        // scene.add( object );
        jsGeometry = new JsCircleGeometry(50, 20, 0, Math.PI * 2);
        jsObject = "object".JsSet(new JsMesh(jsGeometry, material));
        jsObject.Position.Set(100, 0, 0);
        scene.Add(jsObject);
        composer.EmptyLine();

        // object = new THREE.Mesh( new THREE.RingGeometry( 10, 50, 20, 5, 0, Math.PI * 2 ), material );
        // object.position.set( 300, 0, 0 );
        // scene.add( object );
        jsGeometry = new JsRingGeometry(10, 50, 20, 5, 0, Math.PI * 2);
        jsObject = "object".JsSet(new JsMesh(jsGeometry, material));
        jsObject.Position.Set(300, 0, 0);
        scene.Add(jsObject);
        composer.EmptyLine().CodeLine("//").EmptyLine();

        // object = new THREE.Mesh( new THREE.CylinderGeometry( 25, 75, 100, 40, 5 ), material );
        // object.position.set( - 300, 0, - 200 );
        // scene.add( object );
        jsGeometry = new JsCylinderGeometry(25, 75, 100, 40, 5);
        jsObject = "object".JsSet(new JsMesh(jsGeometry, material));
        jsObject.Position.Set(-300, 0, -200);
        scene.Add(jsObject);
        composer.EmptyLine();

        // const points = [];
        // 
        // for ( let i = 0; i < 50; i ++ ) {
        // 	points.push( new THREE.Vector2( Math.sin( i * 0.2 ) * Math.sin( i * 0.1 ) * 15 + 50, ( i - 5 ) * 2 ) );
        // 
        // }
        // object = new THREE.Mesh( new THREE.LatheGeometry( points, 20 ), material );
        // object.position.set( - 100, 0, - 200 );
        // scene.add( object );
        var pointsArray =
            Enumerable
                .Range(0, 50)
                .Select(i =>
                    new JsVector2(
                        Math.Sin(i * 0.2) * Math.Sin(i * 0.1) * 15 + 50,
                        (i - 5) * 2
                    )
                ).ToArray();

        jsGeometry = new JsLatheGeometry(pointsArray, 20);
        jsObject = "object".JsSet(new JsMesh(jsGeometry, material));
        jsObject.Position.Set(-100, 0, -200);
        scene.Add(jsObject);
        composer.EmptyLine();

        // object = new THREE.Mesh( new THREE.TorusGeometry( 50, 20, 20, 20 ), material );
        // object.position.set( 100, 0, - 200 );
        // scene.add( object );
        jsGeometry = new JsTorusGeometry(50, 20, 20, 20);
        jsObject = "object".JsSet(new JsMesh(jsGeometry, material));
        jsObject.Position.Set(100, 0, -200);
        scene.Add(jsObject);
        composer.EmptyLine();

        // object = new THREE.Mesh( new THREE.TorusKnotGeometry( 50, 10, 50, 20 ), material );
        // object.position.set( 300, 0, - 200 );
        // scene.add( object );
        jsGeometry = new JsTorusKnotGeometry(50, 10, 50, 20);
        jsObject = "object".JsSet(new JsMesh(jsGeometry, material));
        jsObject.Position.Set(300, 0, -200);
        scene.Add(jsObject);
        composer.EmptyLine().CodeLine("//").EmptyLine();

        // renderer = new THREE.WebGLRenderer( { antialias: true } );
        // renderer.setPixelRatio( window.devicePixelRatio );
        // renderer.setSize( window.innerWidth, window.innerHeight );
        // document.body.appendChild( renderer.domElement );
        // 
        // stats = new Stats();
        // document.body.appendChild( stats.dom );
        // 
        // //
        // 
        // window.addEventListener( 'resize', onWindowResize );
        composer.CodeLine(
            @"
renderer = new THREE.WebGLRenderer( { antialias: true } );
renderer.setPixelRatio( window.devicePixelRatio );
renderer.setSize( window.innerWidth, window.innerHeight );
document.body.appendChild( renderer.domElement );
stats = new Stats();
document.body.appendChild( stats.dom );

//

window.addEventListener( 'resize', onWindowResize );
".Trim()
        );

        Console.WriteLine(composer.GetJsCode());
    }
}