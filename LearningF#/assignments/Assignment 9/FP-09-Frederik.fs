module RayTracer

open System.Drawing
// #load "Vector.fs"
// #load "Point.fs"
open Point
open Vector

let discriminant a b c = b * b - 4.0 * a * c
let solve a b d = ((-b - (sqrt d))/(2.0 * a), (-b + sqrt d)/(2.0 * a))
let circleIntersection a b c r = 
    let d = discriminant a b (c - r * r)
    if d < 0. then None
    else Some(solve a b d)


let sendRays f (resX, resY) =
    for x in [-resX/2. .. resX/2.] do
        for y in [-resY/2. .. resY/2.] do
            f x y
    true
          

let saveImage a = 
    use image = System.Drawing.Bitmap(20, 20, Imaging.PixelFormat.Format32bppArgb)
    image.SetPixel (0, 0, Color.Blue)
    image.Save(@".\test.png", Imaging.ImageFormat.Png)

/// This function renders a sphere with the given radius and colour, centered at the origin,
/// and saves the rendered image into a file of the given name.
let renderSphere (radius : float)        // The radius of the sphere
                 (colour : Color)        // The colour of the sphere
                 (position : Point)      // The position of the camera
                 (lookat : Point)        // The point that the camera is looking at
                 (up : Vector)           // The up-vector for the camera
                 (zoom : float)          // Distance to the view plane
                 (width : float)         // Width of the view plane in units
                 (height : float)        // Height of the view plane in units
                 (resX : int)            // The horizontal resolution of the view plane  
                 (resY : int)            // The vertical resolution of the view plane
                 (fileName : string)     // Name of the file to save the rendered image
                 : unit
  = ()



[<EntryPoint>]
let main argv = 
    // red sphere, with the camera in front of it
    renderSphere 2.0 Color.Red (mkPoint 0.0 0.0 4.0) (mkPoint 0.0 0.0 0.0) (mkVector 0.0 1.0 0.0) 1.0 2.0 2.0 512 512 "front.png"

    // larger golden sphere, with the camera in front of it
    renderSphere 3.0 Color.Gold (mkPoint 0.0 0.0 4.0) (mkPoint 0.0 0.0 0.0) (mkVector 0.0 1.0 0.0) 1.0 2.0 2.0 512 512 "big.png"

    // green sphere, with the camera in front of it
    renderSphere 2.0 Color.Green (mkPoint 0.0 0.0 4.0) (mkPoint 0.0 2.0 0.0) (mkVector 0.0 1.0 0.0) 1.0 2.0 2.0 512 512 "up.png"

    // blue sphere, with the camera inside of it
    renderSphere 2.0 Color.Blue (mkPoint 0.0 0.0 0.0) (mkPoint 0.0 0.0 -4.0) (mkVector 0.0 1.0 0.0) 1.0 2.0 2.0 512 512 "inside.png"

    // yellow sphere, with the camera behind it (pointing in the other direction)
    renderSphere 2.0 Color.Yellow (mkPoint 0.0 0.0 -4.0) (mkPoint 0.0 0.0 -8.0) (mkVector 0.0 1.0 0.0) 1.0 2.0 2.0 512 512 "behind.png"

    0
